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
        List<IProject> _project;
        public Projects()
        {
            _project = new List<IProject>();
        }

        public List<IProject> ProjectsList
        {
            get
            {
                return _project;
            }

            set
            {
                _project = value;
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

        public void RemoveByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
