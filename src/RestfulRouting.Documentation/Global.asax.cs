using System.Web.Mvc;

namespace RestfulRouting.Documentation {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RestfulRoutingRazorViewEngine());
            AreaRegistration.RegisterAllAreas();
        }
    }
}