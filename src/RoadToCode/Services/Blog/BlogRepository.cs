using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using RoadToCode.Models;
using RoadToCode.Models.Blog;
using Pixel.Results;
using Pixel.DataSource;

namespace RoadToCode.Services.Blog
{
    public class BlogRepository : Repository<Post>, IPostProvider
    {
        public IEnumerable<string> Categories => this.SelectMany(x => x.Category).Distinct();
        public BlogRepository(string tableName) : base(tableName)
        {
        }

        public IResult<Post> NewestByTitle(string title)
        {
            var post = this.LastOrDefault(x => NormalizationProvider.Normalize(x.Title) == title);
            var result = post != null ? (IResult<Post>)new Succeeded<Post>(post) : new NotFound<Post>(post);
            return result;
        }

        public Post GetFeaturedPost()
        {
            var awardedPost = this.Collection.Find(x => x.PostValue == PostValue.Awarded);
            return awardedPost.OrderByDescending(x => x.Added).First();
        }

        public IEnumerable<Post> GetPostsFromCategory(string category)
        {
            return this.Collection.Find(x => x.Category.Contains(category));
        }
    }
}