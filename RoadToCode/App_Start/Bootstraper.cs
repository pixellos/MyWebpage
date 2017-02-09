using System.Web.Optimization;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using RoadToCode.Bundles;

namespace RoadToCode
{
    public class Bootstraper : DefaultNancyBootstrapper
    {

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var bundles = BundleTable.Bundles;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
             
            bundles.Add(Foundation.Styles());
            bundles.Add(Foundation.Scripts());

            StaticConfiguration.DisableErrorTraces = false;

            base.ApplicationStartup(container, pipelines);
        }
    }
}