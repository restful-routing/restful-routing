using System.Web.Routing;

namespace RestfulRouting
{
	public static class MapRoutesExtension
	{
		public static void MapRoutes<TRoutes>(this RouteCollection routes) where TRoutes : RestfulRoutingArea, new()
		{
			new TRoutes().RegisterRoutes(routes);
		}
	}
}