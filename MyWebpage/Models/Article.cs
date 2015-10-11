using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Models;
using MyWebpage.Abstract;

namespace MyWebpage.Models
{
    public class ArticlesModel : IArticles
    {
        public List<IArticleSheet> Articles
        {
            get;
            set;
        }
    }
}
