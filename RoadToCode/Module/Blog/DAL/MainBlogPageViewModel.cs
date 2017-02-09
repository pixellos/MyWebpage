using System.Collections.Generic;

namespace RoadToCode.Module.Blog.DAL
{
    public class MainBlogPageViewModel
    {
        public List<Post> HighlightedPosts { get; set; }
        public IEnumerable<Post> ShowedPosts { get; set; }
        public IEnumerable<Post> AllPosts { get; set; }
        public List<string> Categories { get; set; }
    }
}