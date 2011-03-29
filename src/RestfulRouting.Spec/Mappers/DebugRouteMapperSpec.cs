using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Mappers;
using RestfulRouting.RouteDebug;

namespace RestfulRouting.Spec.Mappers
{
    public class debug_route_mapper : base_context
    {
        Because of = () => new DebugRouteMapper("routedebug").RegisterRoutes(routes);

        It maps_the_route = () => "~/routedebug".WithMethod(HttpVerbs.Get).ShouldMapTo<RouteDebugController>(x => x.Index());
    }
}