using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebpage.Controllers
{
    public class WarringController : Controller
    {
        public ActionResult PageDoesntExistOrYouDontHaveAccess()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}