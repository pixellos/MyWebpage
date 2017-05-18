using System;
using System.Text.Encodings.Web;
using Omu.ValueInjecter;
using RoadToCode.Services.Blog;

namespace RoadToCode.Models.Blog
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public PostValue PostValue { get; set; }
        public DateTime DateTime { get; set; }
        public string Picture { get; set; }

        public CreatePostViewModel()
        {
            this.Title = string.Empty;
            this.Content = string.Empty;
            this.Author = string.Empty;
            this.Category = string.Empty;
            this.DateTime = DateTime.Now;
        }

        public static CreatePostViewModel FromPost(Post post)
        {
            var result = new CreatePostViewModel();
            result.InjectFrom(post);
            return result;
        }

        public Post ToPost()
        {
            var result = new Post();
            result.InjectFrom(this);
            return result;
        }
    }
}