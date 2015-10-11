using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebpage.Models;
using MyWebpage.Abstract;
using Ninject;
using MyWebpage.Entity;

namespace MyWebpage.Infrastructure
{
    public class NinjectDependencyResorvel : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResorvel(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IArticles>().To<ArticleRepository>();
            _kernel.Bind<IProjects>().To<ProjectRepository>();
            _kernel.Bind<IProject>().To<Project>();
            _kernel.Bind<IArticleSheet>().To<ArticleSheet>();
        }
    }
}