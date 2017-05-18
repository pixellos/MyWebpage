using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoadToCode.Services.Blog;
using Microsoft.AspNetCore.Authorization;
using RoadToCode.Models.Blog;
using System;
using RoadToCode.Models.Results;

namespace RoadToCode.Controllers
{
    [Route("[controller]/[Action]")]
    public class BlogController : Controller
    {
        private IPostProvider Posts { get; }
        public BlogController(IPostProvider postProvider)
        {
            this.Posts = postProvider;
        }


        [Route("{count:int:min(0)}")]
        [Authorize]
        public IActionResult Edit(int count)
        {
            var post = this.Posts.Skip(count).FirstOrDefault();
            if (post != null)
            {
                var postvm = CreatePostViewModel.FromPost(post);
                this.ViewData["ReturnAction"] = nameof(Edit);
                return this.View("Edit", postvm);
            }
            else
            {
                return this.NoContent();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(CreatePostViewModel cp)
        {
            var result = this.Posts.Edit(cp.ToPost());
            if (result is Succedeed)
            {
                return this.View("Succeeded");
            }
            else
            {
                return this.BadRequest();
            }
        }

        [Authorize]
        public IActionResult AllRaw()
        {
            return this.Json(this.Posts.ToArray());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Publish()
        {
            var model = new CreatePostViewModel();
            this.ViewData["ReturnAction"] = nameof(Publish);
            return View("Edit", model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Publish(CreatePostViewModel cp)
        {
            var result = this.Posts.Add(cp.ToPost());
            if (result is Succedeed)
            {
                return this.View("Succeeded");
            }
            else
            {
                return this.BadRequest();
            }
        }

        [HttpGet]
        [Route("{count:int:min(0)}/{partial:bool}")]
        [Route("{count:int:min(0)}")]
        public IActionResult Post(int count, bool partial = false)
        {
            var post = this.Posts.Skip(count).FirstOrDefault();
            if (post != null)
            {
                var postvm = CreatePostViewModel.FromPost(post);
                if (partial)
                {
                    return this.PartialView("PartialPost", postvm);
                }
                else
                {
                    return this.View(postvm);
                }
            }
            else
            {
                return this.NoContent();
            }
        }
    }
}