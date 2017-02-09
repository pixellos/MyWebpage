using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using RoadToCode.Module.Projects.DAL;

namespace RoadToCode.Module.Projects
{
    public class ProjectModule : NancyModule
    {
        private ProjectRepository repository;

        public ProjectModule(ProjectRepository repository) : base("/Project")
        {
            this.repository = repository;

            var models = repository.GetAllProjects();
            Get["/"] = param => Negotiate.WithView("Project").WithModel(models);

            CrmSubmodule();
        }

        public void CrmSubmodule()
        {
            Post["/Save"] = param =>
            {
                //
                DAL.Project x = this.Bind<DAL.Project>();
                repository.SavePost(x);
                return Response.AsRedirect("~/Projects",RedirectResponse.RedirectType.Permanent);
            };
            Get["/Del/{id:int}"] = (param) =>
            {
                repository.Remove(param.id);
                return "Deleted";
            };
            Get["/Create"] = (param) =>
            {
                return View["Save"];
            };
        }
    }
}