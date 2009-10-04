using System.Web.Routing;

namespace RestfulRouting.Tests
{
	public abstract class route_test_context : Spec
	{
		protected RouteCollection _routes;
		protected RestfulRouteMapper _map;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
			_map = new RestfulRouteMapper(_routes);
		}
	}
}
