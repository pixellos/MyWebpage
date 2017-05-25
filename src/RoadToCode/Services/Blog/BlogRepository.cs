using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using RoadToCode.Models;
using RoadToCode.Models.Blog;
using RoadToCode.Models.Results;

namespace RoadToCode.Services.Blog
{
    public class BlogRepository : Repository<Post>, IPostProvider
    {
        public BlogRepository(string tableName) : base(tableName)
        {
        }
        public IEnumerable<string> Categories => this.SelectMany(x => x.Category).Distinct();

        public Post GetFeaturedPost()
        {
            var awardedPost = this.Collection.Find(x => x.PostValue == PostValue.Awarded);
            return awardedPost.OrderBy(x => x.Added).First();
        }

        public IEnumerable<Post> GetPostsFromCategory(string category)
        {
            return this.Collection.Find(x => x.Category.Contains(category));
        }
    }
}