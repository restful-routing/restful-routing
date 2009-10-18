using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Sample
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			var application = new Application();
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new AreaViewEngine());
			application.RegisterRoutes(RouteTable.Routes);
		}
	}
}