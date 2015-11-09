using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyWebpage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new {controller = "Main", action = "Index"});
         
            routes.MapRoute(
                name: "Droids",
                url: "Droid",
                defaults: new {controller = "Warring", action = "PageDoesntExistOrYouDontHaveAccess"}
                );
            routes.MapRoute(
                name: "Actions",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Article", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}