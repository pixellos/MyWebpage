using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebpage.Abstract;
using MyWebpage.Models;
using MyWebpage.Entity;
using MyWebpage.Models.ViewModels;

namespace MyWebpage.Controllers
{
    public class CmsController : Controller
    {
        IArticles _articlesRepo;
        IProjects _projectsRepo;
        IProject _projectRepo;

        public CmsController(IArticles iArticles, IProjects iProjects, IProject iProject)
        {
            _articlesRepo = iArticles;
            _projectsRepo = iProjects;
            _projectRepo = iProject;
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult LoginAccepted()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginData model = null)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName == "admin" && model.Password == "admin")
                {
                    return View("Index",null);
                }
            }
            return PartialView();
        }

        [HttpPost]
        public ViewResult ProjectManager()
        {
            
            return View(_projectsRepo);     
        }
        
        [HttpPost]
        public ActionResult RemoveHelper(string id)
        {
            _projectsRepo.RemoveById(id);
            return View("ProjectManager",_projectsRepo);
        }

        [HttpPost]
        public ActionResult ModifyHelper(string id)
        {
            IProject project = _projectsRepo.ProjectsList.Single(x => x.Id == id);
            return View("Helper/ModifyHelper", project);
        }

        [HttpPost]
        public ActionResult FinishModify(Project model)
        {
            if (_projectsRepo.ProjectsList.SingleOrDefault(x=>x.Id == model.Id && x.Name == model.Name) != null)
            {
                _projectsRepo.RemoveById(model.Id);
            }
            _projectsRepo.Add(model);
            return View("ProjectManager",_projectsRepo);
        }
       
        [HttpPost]
        public ActionResult AddProjectHelper(ProjectViewModel model = null)
        {
            if (ModelState.IsValid)
            {
                _projectsRepo.Add(new Project() { Id = model.Id, Name = model.Name, Article = model.Article, GitHubLink = model.GitHubLink, PhotoAdress = model.PhotoAdress });
            }
            return View("Helper/AddHelper",_projectRepo as IProjects);
        }

        [HttpPost]
        public ActionResult RemoveProjects()
        {
            return View(_projectsRepo);
        }

        [HttpPost]
        public ActionResult RemovePost(Project model)
        {
            if (ModelState.IsValid)
            {
                (_projectsRepo).RemoveByName(model.Name);
            }
            return View(_projectRepo);
        }

        public ActionResult ArticlesManager()
        {
            return View(_articlesRepo);
        }
    }
}