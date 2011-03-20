using System.Collections.Generic;
using System.Web.Routing;

namespace RestfulRouting
{

    public static class MapRoutesExtension
    {
        public static void MapRoutes<TRoutes>(this RouteCollection routes) where TRoutes : RouteSet, new()
        {
            new TRoutes().RegisterRoutes(routes);
        }

        public static IEnumerable<Route> ExplicitAndImplicit(this Route implicitRoute)
        {
            var explicitRoute = new Route(implicitRoute.Url + ".{format}", implicitRoute.RouteHandler)
            {
                Constraints = new RouteValueDictionary(implicitRoute.Constraints ?? new RouteValueDictionary()),
                DataTokens = new RouteValueDictionary(implicitRoute.DataTokens ?? new RouteValueDictionary()),
                Defaults = new RouteValueDictionary(implicitRoute.Defaults ?? new RouteValueDictionary())
            };

            explicitRoute.Constraints.Add("format", @"[A-Za-z0-9]+");

            return new[] { explicitRoute, implicitRoute };
        }
    }
}