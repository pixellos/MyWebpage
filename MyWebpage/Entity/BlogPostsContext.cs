using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;
using MyWebpage.Models;

namespace MyWebpage.Entity
{
    public class BlogPostsContext : DbContext
    {
        public BlogPostsContext() : base(Constats.BlogPostsConnectionString)
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }  
    }

    public class BlogPostRepository : IBlogPosts
    {
        public  virtual List<IBlogPost> BlogPosts
        {
            get
            {
                var list = new List<IBlogPost>();
                foreach (var post in new BlogPostsContext().BlogPosts)
                    list.Add(post);
                return list;
            }

            set
            {
                using (var blogContext = new BlogPostsContext())
                {
                    if (!value.Equals(null))
                    {
                        foreach (var thing in value)
                        {
                            blogContext.BlogPosts.AddOrUpdate((BlogPost)thing);
                        }
                        blogContext.SaveChanges();
                    }
                }
            }
        }

        public void RemoveById(int id)
        {
            using (var blogContext = new BlogPostsContext())
            {
                blogContext.BlogPosts.Remove(
                    blogContext.BlogPosts.First(
                        x => x.Id == id));
                blogContext.SaveChanges();
            }
        }
    }
}
