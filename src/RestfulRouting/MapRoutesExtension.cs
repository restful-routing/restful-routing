using System.Web.Routing;

namespace RestfulRouting
{
    public static class MapRoutesExtension
    {
        public static void MapRoutes<TRoutes>(this RouteCollection routes, string[] namespaces = null) where TRoutes : RouteSet, new()
        {
            new TRoutes().RegisterRoutes(routes, namespaces);
        }

        /// <summary>
        /// Allows you to redirect an old route to a new route. 
        /// Note: make sure you register the RedirectFilterAttribute in your global filters.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="oldPath"></param>
        /// <returns></returns>
        public static IRedirectMapper Redirect(this IMapper mapper, string oldPath)
        {
            var route = new RedirectRoute(oldPath);
            mapper.Route(route);
            return route;
        }
    }
}