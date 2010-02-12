using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Sample.Controllers;

namespace RestfulRouting.Sample
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoutes<WebsiteRoutes>();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RestfulRoutingViewEngine());

			RegisterRoutes(RouteTable.Routes);
		}
	}

	public class WebsiteRoutes : RestfulRoutingArea
	{
		public WebsiteRoutes()
		{
			Map("").To<RootController>(x => x.Index());

			Map("routedebug").To<RouteDebugController>(x => x.Index());

			Area<BlogsController>("", () =>
	                                      	{
	                                      		Resources<BlogsController>(() => Resources<PostsController>());
	                                      	});
			Area<Controllers.Admin.BlogsController>("admin", () =>
			                                                 	{
			                                                 		Resources<Controllers.Admin.BlogsController>();
			                                                 		Resources<Controllers.Admin.PostsController>();
			                                                 	});
		}
	}
}