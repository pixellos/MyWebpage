using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebpage.Abstract;
using MyWebpage.Models;

namespace MyWebpage
{
    public struct Constats
    {
        public const string BlogPostsConnectionString =
            @"Data Source=SQL5011.Smarterasp.net;Initial Catalog=DB_9D8EA4_towebpage;User Id=DB_9D8EA4_towebpage_admin;Password=DATA123456;";

        public static ArticlesModel ArticlesModel = new ArticlesModel()
        {
             Articles = new List<IArticleSheet>()
            {
                new ArticleSheet()
                {
                    Article = "AboutMeArticle",
                    Description = "Kim jestem, jak się możesz skontaktować",
                    PhotoShowAdress = "http://imagehosting.biz/images/2015/09/19/Doc.jpg",
                    Headline = "O mnie",
                    Id = 1,
                },
                new ArticleSheet()
                {
                    Article = "	ProjectsArticle",
                    Description = "Projekty zakończone i obecne",
                    PhotoShowAdress = "http://imagehosting.biz/images/2015/09/19/Code.jpg",
                    Headline = "Projekty",
                    Id = 2,
                },
                new ArticleSheet()
                {
                    Article = "	ProjectsArticle",
                    Description = "Często zadawane pytania",
                    PhotoShowAdress = "http://imagehosting.biz/images/2015/09/19/Untitled.gif",
                    Headline = "Najczęściej zadawane pytania",
                    Id = 3,
                }
            }
        };
    }
}