using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappings;

namespace RestfulRouting.Tests.Unit.Mappings
{
    [Subject(typeof(RouteMapping))]
    public class when_mapping_custom_route_mapping
    {
        static RouteMapping mapping;

        static RouteCollection collection;

        static Route route;

        private Establish context = () =>
                                        {
                                            collection = new RouteCollection();
                                            route = new Route("test/{action}", new RouteValueDictionary(new { controller = "test" }), new MvcRouteHandler());
                                            mapping = new RouteMapping(route);
                                        };

        Because of = () => mapping.AddRoutesTo(collection);

        It should_add_the_given_route_to_the_collection = () => mapping.Route.ShouldEqual(route);
    }
}
