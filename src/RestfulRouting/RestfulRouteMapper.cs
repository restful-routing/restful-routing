using System;
using System.Web.Routing;

namespace RestfulRouting
{
	public class RestfulRouteMapper
	{
		private RouteCollection _routeCollection;
		private string _basePath;
		private RouteConfiguration _configuration;

		public RestfulRouteMapper(RouteCollection routeCollection) : this(routeCollection, RouteConfiguration.Default())
		{
		}

		public RestfulRouteMapper(RouteCollection routeCollection, RouteConfiguration configuration)
		{
			_configuration = configuration;

			_routeCollection = routeCollection;
		}

		public RestfulRouteMapper WithConfiguration(Action<RouteConfiguration> config)
		{
			config(_configuration);
			return new RestfulRouteMapper(_routeCollection, _configuration);
		}

		public void Resources<TModel>()
		{
			new ResourcesMapper(_routeCollection, _configuration).Map<TModel>();
		}

		public void Resources(string controller)
		{
			new ResourcesMapper(_routeCollection, _configuration).Map(controller);
		}

		public void Resources<TModel>(Action<RestfulRouteMapper> map)
		{
			new ResourcesMapper(_routeCollection, _configuration).Map<TModel>(map);
		}

		public void Resources(string resource, Action<RestfulRouteMapper> map)
		{
			new ResourcesMapper(_routeCollection, _configuration).Map(resource, map);
		}

		public void Resource(string controller)
		{
			new ResourceMapper(_routeCollection, _basePath).Map(controller);
		}
	}
}
