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
        private IArticles _articlesRepo;
        private IProjects _projectsRepo;
        private IProject _projectRepo;
        private IUsers _usersRepo;

        public CmsController(IArticles iArticles, IProjects iProjects, IProject iProject, IUsers iUsers)
        {
            _articlesRepo = iArticles;
            _projectsRepo = iProjects;
            _usersRepo = iUsers;
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
                if (_usersRepo.IsPasswordOfUserVaild(model.UserName, model.Password))
                {
                    return View("Index", null);
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
        public ActionResult RemoveProjectHelper(string id)
        {
            _projectsRepo.RemoveById(id);
            return PartialView("Results/OK");
        }

        [HttpPost]
        public ActionResult ModifyProjectHelper(string id)
        {
            IProject project = _projectsRepo.ProjectsList.Single(x => x.Id == id);
            return PartialView("Helper/ModifyHelper", project);
        }

        [HttpPost]
        public ActionResult FinishProjectModify(Project model)
        {
            if (_projectsRepo.ProjectsList.SingleOrDefault(x => x.Id == model.Id && x.Name == model.Name) != null)
            {
                _projectsRepo.RemoveById(model.Id);
                _projectsRepo.Add(model);
                return PartialView("Results/OK");
            }
            
            return PartialView("Results/Error");
        }

        [HttpPost]
        public ActionResult FinishProjectHelper(ProjectViewModel model = null)
        {
            if (!ModelState.IsValid) return PartialView("Results/Error");

            try
            {
                _projectsRepo.Add(new Project()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Article = model.Article,
                    GitHubLink = model.GitHubLink,
                    PhotoAdress = model.PhotoAdress
                });
            }

            catch (Exception)
            {
                return PartialView("Results/Error");
            }
            return PartialView("Results/OK");
        }



        [HttpPost]
        public ActionResult AddProjectHelper()
        {

            return View("Helper/AddHelper");

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
                return PartialView("Results/OK");
            }

            return PartialView("Results/Error");
        }

        public ActionResult ArticlesManager()
        {
            return View(_articlesRepo);
        }
    }
}