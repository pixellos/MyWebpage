using Pixel.DataSource;

namespace RoadToCode.Services.Projects
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(string connectionString) : base(connectionString)
        {
        }
    }
}