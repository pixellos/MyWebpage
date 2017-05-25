using System;
using System.Collections.Generic;
using RoadToCode.Models;
using RoadToCode.Models.Blog;
using Pixel.DataSource;

namespace RoadToCode.Services.Blog
{
    public class Post : IDatabaseModel
    {
        public string Id { get; set; }
        public DateTime Added { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public PostValue PostValue { get; set; }
        public string[] Category { get; set; }
        public ModelState State { get; set; }
    }
}