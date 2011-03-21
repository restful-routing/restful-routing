using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappers;

namespace RestfulRouting.Spec.Mappers
{
    public class route_mapper : base_context
    {
        static Route route = new Route("test", new MvcRouteHandler());

        Because of = () => new RouteMapper(route).RegisterRoutes(routes);

        It adds_to_the_routes = () =>
                                    {
                                        routes.Count.ShouldEqual(1);
                                        routes[0].ShouldEqual(route);
                                    };
    }
}
