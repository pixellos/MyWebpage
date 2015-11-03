using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebpage.Abstract
{
    public enum AccessLevel
    {
        User,UberUser,Admin,
    }

    public interface IUser
    {
        [Key]
        int Id { get; set; }
        [Index("FirstName",1,IsUnique = true)]
        string UserName { get; set; }
        string Email { get; set; }
        int SHA256Password { get; set; }
        AccessLevel AccessLevel { get; set; }
    }

    public interface IUsers
    {
        bool IsPasswordOfUserVaild(string userName, string password);
        bool TryToChangePassword(string userName, string oldPassword, string newPassword);
        bool RegisterNewUser(string userName, string password, string email);
        AccessLevel GeAccessLevel(string userName);
    }
}
