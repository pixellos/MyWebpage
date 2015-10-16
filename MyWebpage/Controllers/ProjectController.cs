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
        
        public ProjectController(IProjects projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        IProjects _projectsRepository;

        public PartialViewResult ProjectsCarouselViewResult()
        {
            return PartialView(_projectsRepository);
        }

        public PartialViewResult Projects()
        {
            return PartialView(_projectsRepository);
        }
    }
}