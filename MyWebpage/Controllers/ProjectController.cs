using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebpage.Abstract;

namespace MyWebpage.Controllers
{
    public class ProjectController : Controller
    {
        IProjects _projectsRepository;
        public ProjectController(IProjects projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        public PartialViewResult Projects()
        {
            return PartialView(_projectsRepository);
        }
    }
}