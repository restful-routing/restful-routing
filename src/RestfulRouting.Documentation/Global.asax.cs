using System.Web.Mvc;
using RestfulRouting.Filters;

namespace RestfulRouting.Documentation {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RestfulRoutingRazorViewEngine());
            AreaRegistration.RegisterAllAreas();

            GlobalFilters.Filters.Add(new RedirectFilterAttribute());
        }
    }
}