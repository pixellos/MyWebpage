using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;

namespace MyWebpage.Models.ViewModels
{
    public class ProjectViewModel : IProject
    {
        public string Article { get; set; }

        public string GitHubLink { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string PhotoAdress { get; set; }

        public bool Choosed { get; set; }
    }
}