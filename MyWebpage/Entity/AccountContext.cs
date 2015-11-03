using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;

namespace MyWebpage.Entity
{
    public class AccountContext :DbContext
    {
        public AccountContext(string connectionString) :base(connectionString)
        {
        }
        public DbSet<User> Users { get; set; }
    }

    public class User : IUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int SHA256Password { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }

    public class UserRepository : IUsers
    {
        private string _connectionString;
        public UserRepository(string connectionString= MyWebpage.Constats.BlogPostsConnectionString)
        {
            _connectionString = connectionString;
            _context = new AccountContext(_connectionString);
        }

        protected AccountContext _context;
        
        protected IEnumerable<User> Users
        {
            
            get { return  _context.Users; }
            set
            {
                using (var connectionWithDataBase = _context)
                
                {
                    foreach (var user in value)
                    {
                       connectionWithDataBase.Users.AddOrUpdate(user);
                    }
                    connectionWithDataBase.SaveChanges();
                }
            }
        }

        private bool AddOrUpdateUser(User user)
        {
            try
            {
                _context.Users.AddOrUpdate(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetHashOfPassword(string password)
        {
            return password.GetHashCode();
        }

        private User GetUser(string userName)
        {
            return Users.SingleOrDefault(x => x.UserName == userName);
        }

        public bool IsPasswordOfUserVaild(string userName, string password)
        {
            var user = GetUser(userName);
            if (null != user)
            {
                return user.SHA256Password.Equals(
                    GetHashOfPassword(password));
            }
            return false;
        }

        public bool TryToChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (!IsPasswordOfUserVaild(userName, oldPassword)) return false;
            var user = GetUser(userName);
            user.SHA256Password = newPassword.GetHashCode();
            return AddOrUpdateUser(user);
        }

        public bool RegisterNewUser(string userName, string password, string email)
        {
           
            if (GetUser(userName)!=null) return false;
            return AddOrUpdateUser(
                new User()
                {
                    AccessLevel = AccessLevel.User,
                    Email = email,
                    SHA256Password = GetHashOfPassword(password),
                    UserName = userName
                });
        }

        public AccessLevel GeAccessLevel(string userName)
        {
            return  GetUser(userName)
                .AccessLevel;
        }
    }
}
