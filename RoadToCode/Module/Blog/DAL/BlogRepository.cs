using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;

namespace RoadToCode.Module.Blog.DAL
{
    public class BlogRepository : RepoBase<Post>
    {
        public BlogRepository() : base("Blog")
        {

        }

        public void Delete(string linkString)
        {
            using (var db = createConnection())
            {
                var collection = db.GetCollection<Post>(TableName);


                var id = collection.FindAll().First(x => x.GetLink.Equals(linkString)).Id;
                collection.Delete(x => x.Id.Equals(id));
                db.Commit();
            }
        }

        public MainBlogPageViewModel GetViewModel()
        {
            using (var db = createConnection())
            {
                var collection = GetCollection(db);
                
                MainBlogPageViewModel model = new MainBlogPageViewModel();
                
                var inMemoryDump = collection.FindAll().OrderBy(x=>x.DateTime).Reverse();
                model.ShowedPosts= inMemoryDump.Where(x => x.PostValue == PostValue.Awarded).ToList();
                model.HighlightedPosts= inMemoryDump.Where(x => x.PostValue == PostValue.Promoted).ToList();
                model.AllPosts = inMemoryDump.ToList();
                model.Categories = new HashSet<String>(inMemoryDump.Select(x => x.Category)).ToList();
                return model;
            }
        }

        public Post GetPostFromLink(string linkString)
        {
            using (var db = createConnection())
            {
                var collection = db.GetCollection<Post>(TableName);


                return collection.FindAll().First(x=>x.GetLink.Equals(linkString));
            }
        }

        public Post GetFeaturedPost()
        {
            using (var db = createConnection())
            {
                var collection = db.GetCollection<Post>(TableName);

                var awardedPost = collection.Find(x => x.PostValue == PostValue.Awarded);

                return awardedPost.OrderBy(x => x.DateTime).First();
            }
        }

        public List<Post> GetPostsFromCategory(string category)
        {
            using (var db = createConnection())
            {
                var collection = GetCollection(db);

                return collection.Find(x=>x.Category.Equals(category)).ToList();
            }
        }
    }
}