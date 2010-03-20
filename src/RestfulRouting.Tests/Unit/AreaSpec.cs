using System.Linq;
using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Unit
{
	public class when_mapping_an_area
	{
		static RouteCollection routes;

		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Area<BlogsController>("area", () =>
				                              	{
				                              		Resources<BlogsController>();
				                              	});
				
			}
		}

		Establish context = () => routes = new RouteCollection();

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_add_the_routes_to_the_collection = () => routes.Count.ShouldBeGreaterThan(1);

		It should_set_the_area_to_area = () =>
												{
													foreach (var route in routes.Select(x => (Route)x))
													{
														route.Url.ShouldStartWith("area");
													}
												};
		It should_set_the_default_namespace = () =>
												{
													foreach (var route in routes.Select(x => (Route)x))
													{
														route.DataTokens["namespaces"].ShouldEqual(new[]{ typeof(BlogsController).Namespace });
													}
												};

		It should_set_the_area = () =>
									{
										foreach (var route in routes.Select(x => (Route)x))
										{
											route.DataTokens["area"].ShouldEqual("area");
										}
									};
	}

	public class when_mapping_a_non_namespaced_area
	{
		static RouteCollection routes;

		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Area("non_namespaced_area", () => Resources<BlogsController>());
			}
		}

		Establish context = () => routes = new RouteCollection();

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_routes_with_prefix = () =>
		{
			foreach (var route in routes.Select(x => (Route)x))
			{
				route.Url.ShouldStartWith("non_namespaced_area");
			}
		};
	}

	public class when_mapping_an_area_within_a_non_namespaced_area
	{
		static RouteCollection routes;

		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Area<BlogsController>("area", () =>
				{
					Area("non_namespaced_area", () => Resources<BlogsController>());
				});

			}
		}

		Establish context = () => routes = new RouteCollection();

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_routes_with_prefix = () =>
											{
												foreach (var route in routes.Select(x => (Route)x))
												{
													route.Url.ShouldStartWith("area/non_namespaced_area");
												}
											};
	}
}