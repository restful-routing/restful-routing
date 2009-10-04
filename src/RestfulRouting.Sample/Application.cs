using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Sample.Controllers;

namespace RestfulRouting.Sample
{
	public class Application
	{
		public void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				"Default",
				"",
				new { controller = "Root", action = "Index" },
				new { httpMethod = new HttpMethodConstraint("GET") }
				);

			var map = new RestfulRouteMapper(RouteTable.Routes);

			map.Resources<BlogsController>(m => m.Resources<PostsController>());

			// shallow
			//map.Resources<BlogsController>(config => config.Shallow = true, m => m.Resources<PostsController>());
		}
	}
}