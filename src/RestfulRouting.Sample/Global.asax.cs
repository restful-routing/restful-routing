using System.Web;
using System.Web.Routing;

namespace RestfulRouting.Sample
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			var application = new Application();
			application.RegisterRoutes(RouteTable.Routes);
		}
	}
}