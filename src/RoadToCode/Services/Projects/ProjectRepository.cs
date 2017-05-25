using System.Collections.Generic;
using System.Linq;
using RoadToCode.Models;

namespace RoadToCode.Services.Projects
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(string connectionString) : base(connectionString)
        {
        }
    }
}