using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MyWebpage.Models
{
    public class ArticleSheet : IArticleSheet
    {
        [Key]
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string PhotoShowAdress { get; set; }
        public string Article { get; set; }

    }
}
