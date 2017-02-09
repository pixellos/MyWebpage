using System;

namespace RoadToCode.Module.Blog.DAL
{
    public class Post : IDatabaseModel
   {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
        public PostValue PostValue { get; set; }
        public string Category { get; set; }
        public string GetLink => Title.GetLinkString();

        public static Post GetDefault() => new Post()
        {
            Category = "",
            Title = "",
            DateTime = DateTime.Now,
            ImageUrl = "",
            Id = 0,
            Content = "",
            PostValue = PostValue.None
        };
    }

}