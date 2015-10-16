using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyWebpage.Abstract;
using MyWebpage.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace MyWebpage.Entity
{
    public class ProjectContext :DbContext
    {
       
        public ProjectContext() : base("Server = tcp:lv9kyjkui8.database.windows.net, 1433; Database=MyWebpageDataBase;User ID = SQLDatabase@lv9kyjkui8;Password=Test123456; Trusted_Connection=False;Encrypt=True;Connection Timeout = 30;")
        { }
        public DbSet<Project> Projects { get; set; }
    }

    public class ProjectRepository : IProjects
    {
        public void Add(IProject project)
        {
            if (project != null)
            {
                using (ProjectContext connection = new ProjectContext())
                {
                        connection.Projects.Add(project as Project);
                    try
                    {
                        connection.SaveChanges();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void RemoveById(string id)
        {
            using (ProjectContext connection = new ProjectContext())
            {
                var toremove = connection.Projects.SingleOrDefault(x => x.Id == id);
                connection.Projects.Remove(toremove);
                connection.SaveChanges();
            }
        }

        public void RemoveByName(string Name)
        {
            using (ProjectContext connection = new ProjectContext())
            {
                var toremove = connection.Projects.SingleOrDefault(x => x.Name == Name);
                if (toremove !=null)
                {
                    connection.Projects.Remove(toremove);
                }
                connection.SaveChanges();
            }
        }
        public List<IProject> ProjectsList
        {
            get
            {
                using (ProjectContext connection = new ProjectContext())
                {
                    var toReturn = (connection.Projects.ToList<IProject>());
                    return toReturn;
                }
            }
            set
            {
            }
        }
    }
}
