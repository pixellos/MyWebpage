using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWebpage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyWebpage.Entity.Tests
{
    [TestClass()]

    class UserRepositoryManagmentClass : UserRepository
    {
        public UserRepositoryManagmentClass(string connectionString) : base(connectionString)
        { }
        public void ResetDatabase()
        {
            List<User> UserList = Users.ToList();
            _context.Users.RemoveRange(UserList);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetListUsers()
        {
            return Users;
        }
    }
    public class UserRepositoryTests
    {
        const string LocalTestConnectionString = "localTEst";
        private UserRepository UserRepository;

        public UserRepositoryTests()
        {
            new UserRepositoryManagmentClass(LocalTestConnectionString).ResetDatabase();
            UserRepository = new UserRepository(LocalTestConnectionString);
        }

        [TestMethod()]
        public void IsDataBaseEmpty()
        {
            Assert.IsNull(new UserRepositoryManagmentClass(LocalTestConnectionString).GetListUsers());
        }


        [TestMethod()]
        public void IsPasswordOfUserVaildTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TryToChangePasswordTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RegisterNewUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GeAccessLevelTest()
        {
            Assert.Fail();
        }
    }
}