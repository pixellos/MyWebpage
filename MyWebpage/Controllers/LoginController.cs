using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MyWebpage.Abstract;
using MyWebpage.Entity;
using MyWebpage.Models;
using MyWebpage.Models.ViewModels;

namespace MyWebpage.Controllers
{
    public class LoginController : Controller
    {
        private IUsers _users;

        public LoginController(IUsers users)
        {
            _users = users;
        }

        public PartialViewResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddUserHelper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserInput model = null)
        {
            if (_users.RegisterNewUser(model.UserName, model.Password, model.Email))
            {
                return SuccessResult("This Account has been registered! " + model.UserName);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SuccessResult(string message = "Operation Successfull")
        {
            return Content(message,"text/html");
        }

        [HttpPost]
        public ActionResult ChangePasswordHelper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(UserInput model = null)
        {
            if (_users.TryToChangePassword(model.UserName, model.Password, model.Email))
            {
                
                return SuccessResult("Password has been successfully set for account " + model.UserName);
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public ActionResult Login(LoginData model = null)
        {
            if (ModelState.IsValid)
            {
                if (_users.IsPasswordOfUserVaild(model.UserName, model.Password))
                {
                    return View("Index", null);
                }
            }
            return PartialView();
        }
    }
}