using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using MyWebpage.Abstract;
using MyWebpage.Models;

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
        public ActionResult Login(LoginData model = null)
        {
            if (ModelState.IsValid)
            {
                if (_users.IsPasswordOfUserVaild(model.UserName,model.Password))
                {
                    return View("Index", null);
                }
            }
            return PartialView();
        }
    }
}