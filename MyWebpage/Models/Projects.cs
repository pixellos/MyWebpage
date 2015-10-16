using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebpage.Abstract;

namespace MyWebpage.Models
{
    public class Projects : IProjects
    {
        List<IProject> project;
        public Projects()
        {
            this.project = new List<IProject>();
        }

        public List<IProject> ProjectsList
        {
            get
            {
                return this.project;
            }

            set
            {
                this.project = value;
            }
        }

        public void Add(IProject project)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
