using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebpage.Models;
using MyWebpage.Abstract;

namespace MyWebpage.Controllers
{
    public class CarouselController : Controller
    {
        IArticles _articleRepo;
        public CarouselController(IArticles articlesRepository)
        {
            _articleRepo = articlesRepository;
        }
        public PartialViewResult Carousel(string articlecece = "About")
        {
            return PartialView(_articleRepo);
        }
    }
}