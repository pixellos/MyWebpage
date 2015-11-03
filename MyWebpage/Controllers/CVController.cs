using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebpage.Controllers
{
    public class CvController : Controller
    {
        // GET: CV
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}