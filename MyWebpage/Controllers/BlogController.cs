using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebpage.Abstract;
using MyWebpage.Models;

namespace MyWebpage.Controllers
{
    public class BlogController : Controller
    {
        private IBlogPosts _blogPosts;

        public BlogController(IBlogPosts blogPosts)
        {
            _blogPosts = blogPosts;
        }

        [HttpPost]
        public ActionResult RemoveBlogHelperResult(int id)
        {
            _blogPosts.RemoveById(id);

            return View("BlogManagerResult", _blogPosts);
        }

        [HttpPost]
        public ActionResult ModifyBlogHelpeResult(int id)
        {
            IBlogPost post = _blogPosts.BlogPosts.Single(x => x.Id == id);
            return View(post);
        }

        [HttpPost]
        public ActionResult FinishBlogModify(BlogPost model)
        {
            if (_blogPosts.BlogPosts.Exists(x => x.Id == model.Id))
            {
                _blogPosts.RemoveById(model.Id);
            }
            if (ModelState.IsValid && model.Content != null)
            {
                var temp = _blogPosts.BlogPosts;
                temp.Add(model);
                _blogPosts.BlogPosts = temp;
            }
            return View("BlogManagerResult", _blogPosts);
        }


        [HttpPost]
        public ActionResult BlogManagerResult()
        {
            return View(_blogPosts);
        }

        [HttpPost]
        public ActionResult AddBlogHelper(MyWebpage.Models.BlogPost post = null)
        {
            if (ModelState.IsValid && post.Content != null)
            {
                var temp = _blogPosts.BlogPosts;
                temp.Add(post);
                _blogPosts.BlogPosts = temp;
            }
            return View("Helper/AddBlogPostHelper", post);
        }

        public ActionResult GetAllPosrtPartialViewResult()
        {
            return PartialView("NewestPostsPartialViewResult", _blogPosts.BlogPosts);
        }

        public ActionResult NewestPostsPartialViewResult(int countOfPosts = 5)
        {
            _blogPosts.BlogPosts.Sort((a, b) => a.BlogDateTime.CompareTo(b.BlogDateTime));
            return PartialView(_blogPosts.BlogPosts.Take(countOfPosts));
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}