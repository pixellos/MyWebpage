using System;
using System.Text.Encodings.Web;
using Omu.ValueInjecter;
using RoadToCode.Services.Blog;

namespace RoadToCode.Models.Blog
{
    public class CreatePostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public PostValue PostValue { get; set; }
        public DateTime DateTime { get; set; }
        public string Picture { get; set; }
        public string NormalizedTitle => NormalizationProvider.Normalize(this.Title);
        public CreatePostViewModel()
        {
            this.Id = string.Empty;
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
            result.Category = post.Category != null ? String.Join(";", post.Category) : string.Empty;
            return result;
        }

        public Post ToPost()
        {
            var result = new Post();
            result.InjectFrom(this);
            result.Category = this.Category.Split(';');
            return result;
        }
    }
}