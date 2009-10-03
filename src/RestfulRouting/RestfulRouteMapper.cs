using System.Web.Routing;

namespace RestfulRouting
{
	public class RestfulRouteMapper
	{
		private RouteCollection _routeCollection;

		public RestfulRouteMapper(RouteCollection routeCollection)
		{
			_routeCollection = routeCollection;
		}

		public void Resources<TModel>()
		{
			new ResourcesMapper(_routeCollection).Map<TModel>();
		}

		public void Resources(string controller)
		{
			new ResourcesMapper(_routeCollection).Map(controller);
		}

		public void Resource(string controller)
		{
			new ResourceMapper(_routeCollection).Map(controller);
		}
	}
}
