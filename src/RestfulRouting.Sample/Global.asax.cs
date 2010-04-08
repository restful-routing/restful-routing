using System.Web.Mvc;
using System.Web.Routing;

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

			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RestfulRoutingViewEngine());

			RegisterRoutes(RouteTable.Routes);
		}
	}
}