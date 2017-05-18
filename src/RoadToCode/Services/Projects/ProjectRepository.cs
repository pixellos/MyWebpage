using System.Collections.Generic;
using System.Linq;
using RoadToCode.Models;

namespace RoadToCode.Services.Projects
{
    public class ProjectRepository : Repository<Project, string>
    {
        public ProjectRepository(string connectionString) : base(connectionString, x => x.Title)
        {
        }
    }
}