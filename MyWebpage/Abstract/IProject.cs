using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyWebpage.Abstract
{
    public interface IProject
    {
        [Key]
        string Id { get; set; }
        string Name { get; set; }
        string PhotoAdress { get; set; }
        string GitHubLink { get; set; }
        string Article { get; set; }
    }
}
