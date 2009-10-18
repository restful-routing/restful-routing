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


			// need to use namespaces when using controllers with the same name
			var configuration = new RouteConfiguration {Namespaces = new[] {typeof (BlogsController).Namespace}};

			var map = new RestfulRouteMapper(RouteTable.Routes, configuration);

			map.Resources<BlogsController>(m => m.Resources<PostsController>());

			map.Namespace("admin", new[]{ typeof(Controllers.Admin.BlogsController).Namespace }, m =>
			                       	{
										m.Resources<Controllers.Admin.BlogsController>();
										m.Resources<Controllers.Admin.PostsController>();
			                       	});
			// shallow
			//map.Resources<BlogsController>(config => config.Shallow = true, m => m.Resources<PostsController>());
		}
	}
}