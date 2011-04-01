using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.ViewEngines;

namespace RestfulRouting.Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoutes<Routes>();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            System.Web.Mvc.ViewEngines.Engines.Clear();
            System.Web.Mvc.ViewEngines.Engines.Add(new RestfulRoutingFormatRazorViewEngine());
            System.Web.Mvc.ViewEngines.Engines.Add(new RestfulRoutingWebFormViewEngine());

            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }
    }
}