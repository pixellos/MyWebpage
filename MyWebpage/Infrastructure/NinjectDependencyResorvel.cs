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
        private IKernel kernel;

        public NinjectDependencyResorvel(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            var dbConnectionHandler = new DataBaseConnectionRepositoriesSingleton();
            this.kernel.Bind<IUsers>().To<UserRepository>();
            this.kernel.Bind<IArticles>().ToConstant(Constats.ArticlesModel);
            this.kernel.Bind<IProjects>().ToConstant((IProjects) dbConnectionHandler);
            this.kernel.Bind<IBlogPost>().To<BlogPost>();
            this.kernel.Bind<IBlogPosts>().To<BlogPostRepository>();
            this.kernel.Bind<IProject>().To<Project>();
            this.kernel.Bind<IArticleSheet>().To<ArticleSheet>();
        }
    }
}