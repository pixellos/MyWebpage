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

        public Result Remove(Post post)
        {
            var collectionPosts = this.Collection.Find(x => x.Id.Equals(post.Id, StringComparison.OrdinalIgnoreCase));
            if (!collectionPosts.Any())
            {
                return new Error("There are no matching posts.");
            }
            else
            {
                using (var trans = base.Database.BeginTrans())
                {
                    foreach (var item in collectionPosts)
                    {
                        item.State = ModelState.Deleted;
                    }
                    trans.Commit();
                }
                return new Succedeed();
            }
        }

        public Result Edit(Post post)
        {
            var pervousPost = this.Collection.Find(x => x.Id.Equals(post.Id, StringComparison.OrdinalIgnoreCase));
            if (pervousPost.Any())
            {
                using (var trans = base.Database.BeginTrans())
                {
                    var transactionDate = DateTime.Now;
                    foreach (var item in pervousPost.Where(x => x.State == ModelState.Ok).ToArray())
                    {
                        item.State = ModelState.Updated;
                        item.Added = transactionDate;
                        this.Collection.Update(item);
                    }
                    post.State = ModelState.Ok;
                    var addResult = this.Add(post, transactionDate);
                    if (addResult is Error errorResult)
                    {
                        trans.Rollback();
                        return new Error<Error>(errorResult, "Error while adding");
                    }
                    else
                    {
                        trans.Commit();
                        return new Succedeed();
                    }
                }
            }
            else
            {
                return new Error();
            }
        }

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