using System;
using System.Web.Routing;
using System.Web.Mvc;
using Machine.Specifications;
using RestfulRouting.Spec.TestObjects;

namespace RestfulRouting.Spec
{
    public class base_context
    {
        protected static RouteCollection routes = RouteTable.Routes;

        Establish context = () => routes.Clear();
    }

    public class route_spec : base_context
    {
        public class Routes : RouteSet
        {
            public override void Map(Mapper map)
            {
                map.Route(new Route("posts/{action}", new RouteValueDictionary(new { controller = "posts" }), new MvcRouteHandler()));
            }
        }

        Because of = () => new Routes().RegisterRoutes(routes);

        It adds_the_route_to_the_collection = () => routes.Count.ShouldEqual(1);
    }

    public class resources_spec : base_context
    {
        public class Routes : RouteSet
        {
            public override void Map(Mapper map)
            {
                map.Resources<PostsController>();
            }
        }

        Because of = () => new Routes().RegisterRoutes(routes);

        It adds_the_routes_to_the_collection = () => routes.Count.ShouldBeGreaterThan(1);
    }

    public class area_spec : base_context
    {
        public class Routes : RouteSet
        {
            public override void Map(Mapper map)
            {
                map.Area<PostsController>("test", area => area.Resources<PostsController>());
            }
        }

        Because of = () => new Routes().RegisterRoutes(routes);

        It sets_the_path_correctly = () => routes.ShouldEachConformTo(x => ((Route)x).Url.StartsWith("test/posts"));
    }
}
