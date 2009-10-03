using System;
using System.Web.Routing;

namespace RestfulRouting
{
	public class RestfulRouteMapper
	{
		private RouteCollection _routeCollection;
		private string _basePath;

		public RestfulRouteMapper(RouteCollection routeCollection) : this(routeCollection, "")
		{
		}

		public RestfulRouteMapper(RouteCollection routeCollection, string basePath)
		{
			_basePath = basePath;

			_routeCollection = routeCollection;
		}

		public void Resources<TModel>()
		{
			new ResourcesMapper(_routeCollection, _basePath).Map<TModel>();
		}

		public void Resources(string controller)
		{
			new ResourcesMapper(_routeCollection, _basePath).Map(controller);
		}

		public void Resources<TModel>(Action<RestfulRouteMapper> map)
		{
			new ResourcesMapper(_routeCollection, _basePath).Map<TModel>(map);
		}

		public void Resources(string resource, Action<RestfulRouteMapper> map)
		{
			new ResourcesMapper(_routeCollection, _basePath).Map(resource, map);
		}

		public void Resource(string controller)
		{
			new ResourceMapper(_routeCollection, _basePath).Map(controller);
		}
	}
}
