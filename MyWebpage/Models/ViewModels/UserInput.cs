using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;

namespace MyWebpage.Models.ViewModels
{
    public class UserInput : IUserInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }

    public interface IUserInput
    {
        string UserName { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        AccessLevel AccessLevel { get; set; }
    }
}