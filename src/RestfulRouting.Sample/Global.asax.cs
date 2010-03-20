using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Sample.Controllers;

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

	public class Routes : RouteSet
	{
		public Routes()
		{
			Map("").To<RootController>(x => x.Index());

			Map("routedebug").To<RouteDebugController>(x => x.Index());

			Area<BlogsController>("", () =>
          	{
				Resources<BlogsController>(() =>
				{
					As("weblogs");
					Only("index", "show");
					Collection("latest", HttpVerbs.Get);

					Resources<PostsController>(() =>
					{
						Except("create", "update", "destroy");
						Resources<CommentsController>(() => Except("destroy"));
					});
				});

          	});
			
			//Area<BlogsController>("", () =>
			//                                {
			//                                    Resources<BlogsController>(() => Resources<PostsController>());
			//                                });
			//Area<Controllers.Admin.BlogsController>("admin", () =>
			//                                                    {
			//                                                        Resources<Controllers.Admin.BlogsController>();
			//                                                        Resources<Controllers.Admin.PostsController>();
			//                                                    });
		}
	}
}