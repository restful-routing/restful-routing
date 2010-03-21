using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Mappings;
using RestfulRouting.Tests.Integration.Behaviours;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
	[Subject(typeof(StandardMapping))]
	public class when_mapping_a_custom_route : base_context
	{
		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Route(new Route("posts/{action}", new RouteValueDictionary(new { controller = "posts" }), new MvcRouteHandler()));
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_posts_latest = () => "~/posts/latest".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Latest());
	}
}