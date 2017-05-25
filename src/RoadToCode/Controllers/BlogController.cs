using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoadToCode.Services.Blog;
using Microsoft.AspNetCore.Authorization;
using RoadToCode.Models.Blog;
using System;
using Pixel.Results;

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

        [HttpGet]
        [Route("{title}")]
        public IActionResult Post(string title)
        {
            var result = this.Posts.NewestByTitle(title);
            if (result is Succeeded<Post> post)
            {
                var postVm = CreatePostViewModel.FromPost(post);
                return this.View("StandalonePost", postVm);
            }
            else if (result is IResult res)
            {
                return this.View("Error", res?.ToString() ?? string.Empty);
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
                    return this.PartialView(postvm);
                }
                else
                {
                    return this.View("StandalonePost", postvm);
                }
            }
            else
            {
                return this.NoContent();
            }
        }

        [Route("/[controller]/Post/{title}/Remove")]
        [Route("/[controller]/Remove/{title}")]
        public IActionResult Remove(string title)
        {
            var result = this.Posts.NewestByTitle(title);
            if (result is Succeeded<Post> post)
            {
                var postvm = CreatePostViewModel.FromPost(post);
                this.ViewData["ReturnAction"] = nameof(Remove);
                return this.View("RemoveAccept", postvm);
            }
            else if (result is IResult res)
            {
                return this.View("Error", res?.ToString() ?? string.Empty);
            }
            else
            {
                return this.BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Remove(Post post)
        {
            var result = this.Posts.Hide(post);
            if (result is ISucceeded removed)
            {
                return this.View("Succeeded");
            }
            else if (result is IResult res)
            {
                return this.View("Error", res?.ToString() ?? string.Empty);
            }
            else
            {
                return this.BadRequest();
            }
        }

        [Route("/[controller]/Post/{title}/Edit")]
        [Route("/[controller]/Edit/{title}")]
        [Authorize]
        public IActionResult Edit(string title)
        {
            var result = this.Posts.NewestByTitle(title);
            if (result is Succeeded<Post> post)
            {
                var postvm = CreatePostViewModel.FromPost(post);
                this.ViewData["ReturnAction"] = nameof(Edit);
                return this.View(postvm);
            }
            else if (result is IResult res)
            {
                return this.View("Error", res?.ToString() ?? string.Empty);
            }
            else
            {
                return this.BadRequest();
            }
        }

        [Route("{count:int:min(0)}/Edit")]
        [Authorize]
        public IActionResult Edit(int count)
        {
            var post = this.Posts.Skip(count).FirstOrDefault();
            if (post != null)
            {
                var postvm = CreatePostViewModel.FromPost(post);
                this.ViewData["ReturnAction"] = nameof(Edit);
                return this.View(postvm);
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
            if (result is ISucceeded)
            {
                return this.View("Succeeded");
            }
            else if (result is Error error)
            {
                ModelState.AddModelError(error.GetHashCode().ToString(), error.Message);
                return this.View(cp);
            }
            else
            {
                throw new InvalidOperationException();
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
            if (result is Succeeded)
            {
                return this.View("Succeeded");
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}