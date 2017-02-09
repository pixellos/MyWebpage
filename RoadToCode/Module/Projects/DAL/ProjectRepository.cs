using System.Collections.Generic;
using System.Linq;
using RoadToCode.Module.Blog.DAL;

namespace RoadToCode.Module.Projects.DAL
{
    public class ProjectRepository : RepoBase<Project>
    {
        public ProjectRepository() : base("Project")
        {
        }

        public void Remove(int id)
        {
            using (var db = createConnection())
            {
                var collection = GetCollection(db);
                collection.Delete(x => x.Id == id);
                db.Commit();

            }
        } 

        public List<Project> GetAllProjects()
        {
            using (var db = createConnection())
            {
                var collection = GetCollection(db);

                return collection.FindAll().ToList();
            }
        }
    }
}