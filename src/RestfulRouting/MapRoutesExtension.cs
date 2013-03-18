using System.Web.Routing;

namespace RestfulRouting
{
    public static class MapRoutesExtension
    {
        public static void MapRoutes<TRoutes>(this RouteCollection routes, string[] namespaces = null) where TRoutes : RouteSet, new()
        {
            new TRoutes().RegisterRoutes(routes, namespaces);
        }

        public static IRedirectMapper Redirect(this IMapper mapper, string oldPath)
        {
            var route = new RedirectRoute(oldPath);
            mapper.Route(route);
            return route;
        }
    }
}