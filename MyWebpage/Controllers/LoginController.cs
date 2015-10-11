using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using MyWebpage.Models;

namespace MyWebpage.Controllers
{
   
    public class LoginController : Controller
    {
        public PartialViewResult Login()
        {
            return PartialView();
        }

        public ViewResult LoginAccepted()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginData model = null)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName=="admin" && model.Password=="admin")
                {
                    return View("Index", null);
                }
            }
            return PartialView();
        }
    }
}