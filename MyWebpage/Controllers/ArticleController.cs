using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebpage.Abstract;
using MyWebpage.Models;



namespace MyWebpage.Controllers
{
    public class ArticleController : Controller
    {
        IArticles _articlesRepository;
        public ArticleController(IArticles articles)
        {
            _articlesRepository = articles;
        }
        // GET: Article
        public ActionResult Index(string article = "AboutMeArticle")
        {
            var getArticleByParametr = _articlesRepository.Articles.FirstOrDefault(
                    x => x.Article == article
                    );
            if (getArticleByParametr == null)
            {
                getArticleByParametr = new ArticleSheet()
                {
                    Article = "NOPEArticle",
                    Id = 404,
                    Description = "Strona której szukasz nie istnieje",
                    Headline = "Uciekł :/"
                };
            }
            
            return View(getArticleByParametr);
        }
    }
}