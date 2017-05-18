using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using RoadToCode.Models;
using RoadToCode.Models.Blog;
using RoadToCode.Models.Results;

namespace RoadToCode.Services.Blog
{
    public class BlogRepository : Repository<Post, string>, IPostProvider
    {
        public BlogRepository(string tableName) : base(tableName, x => x.Title)
        {
        }

        public Result Edit(Post post)
        {
            var pervousPost = this.Collection.Find(x => x.Title.Equals(post.Title, StringComparison.OrdinalIgnoreCase));
            foreach (var item in pervousPost.Where(x => x.State == ModelState.Ok).ToArray())
            {
                item.State = ModelState.Updated;
                this.Collection.Update(item);
            }
            this.Add(post);
            return new Succedeed();
        }

        public IEnumerable<Post> Paged(int page, int postsPerPage)
        {
            if (page < 0)
            {
                throw new ArgumentException(nameof(page));
            }
            if (postsPerPage < 1)
            {
                throw new ArgumentException(nameof(postsPerPage));
            }
            return this.Collection.FindAll().Skip(postsPerPage * page).Take(page);
        }

        public Post GetFeaturedPost()
        {
            var awardedPost = this.Collection.Find(x => x.PostValue == PostValue.Awarded);
            return awardedPost.OrderBy(x => x.Updated.First()).First();
        }

        public IEnumerable<Post> GetPostsFromCategory(string category)
        {
            return this.Collection.Find(x => x.Category.Equals(category));
        }
    }
}