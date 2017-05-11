using System;
using RoadToCode.Module.Blog.DAL;

namespace RoadToCode.Module.Projects.DAL
{
    public class Project : IDatabaseModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string HtmlContent { get; set; }
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
    }
}