using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MyWebpage.Models
{
    public class Project : IProject
    {
        [Key]
        public string Id { get; set; }

        public string Article { get; set; }
        public string GitHubLink { get; set; }
        public string Name { get; set; }
        public string PhotoAdress { get; set; }
    }
}