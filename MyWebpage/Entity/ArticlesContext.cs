using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyWebpage.Abstract;
using MyWebpage.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyWebpage.Entity
{
    public class ArticlesContext :DbContext
    {

        public ArticlesContext() :base("Server = tcp:lv9kyjkui8.database.windows.net, 1433; Database=MyWebpageDataBase;User ID = SQLDatabase@lv9kyjkui8;Password=Test123456; Trusted_Connection=False;Encrypt=True;Connection Timeout = 30;")
        {

        }
        public DbSet<ArticleSheet> Articles { get; set; }
    }

    public class ArticleRepository : IArticles
    {
        public List<IArticleSheet> Articles
        {
            get
            {
                ArticlesContext connection = new ArticlesContext();
                var toReturn = connection.Articles.ToList<IArticleSheet>();
                connection.Dispose();
                return toReturn;
            }
            set
            {
                    if (value != null)
                   {
                        using (ArticlesContext connection = new ArticlesContext())
                        {
                          
                        foreach (var item in value)
                        {
                            connection.Articles.Add(item as ArticleSheet);
                        }
                        var info = new ArticleRepository().Articles;
                            connection.SaveChanges();
                        }
                   }
            }
        }
    }
}
