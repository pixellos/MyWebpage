using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebpage.Abstract
{
    public interface IProjects
    {
        List<IProject> ProjectsList { get; set; }
        void RemoveById(string id);
        void RemoveByName(string name);
        void Add(IProject project);
    }
}