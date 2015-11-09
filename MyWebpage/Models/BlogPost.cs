using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;

namespace MyWebpage.Models
{
    public class BlogPost : IBlogPost
    {
        [Key]
        public int Id { get; set; }

        public DateTime BlogDateTime { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string PhotoAdress { get; set; }
        public string PhotoDescribtion { get; set; }
    }
}