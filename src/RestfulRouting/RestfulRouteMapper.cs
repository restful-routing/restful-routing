using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class RestfulRouteMapper
	{
		protected RouteCollection _routeCollection;
		private RouteConfiguration _routeConfiguration;

		public RestfulRouteMapper(RouteCollection routeCollection) : this(routeCollection, RouteConfiguration.Default())
		{
		}

		public RestfulRouteMapper(RouteCollection routeCollection, RouteConfiguration routeConfiguration)
		{
			_routeCollection = routeCollection;
			_routeConfiguration = routeConfiguration;
		}

		public void Resources<TController>() where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, _routeConfiguration).Map();
		}

		public void Resources<TController>(Action<RouteConfiguration> config)
			where TController : Controller
		{
			config(_routeConfiguration);

			new ResourcesMapper<TController>(_routeCollection, _routeConfiguration).Map();
		}

		public void Resources<TController>(Action<RestfulRouteMapper> map)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, _routeConfiguration).Map(map);
		}

		public void Resources<TController>(Action<RouteConfiguration> config, Action<RestfulRouteMapper> map)
			where TController : Controller
		{
			config(_routeConfiguration);

			new ResourcesMapper<TController>(_routeCollection, _routeConfiguration).Map(map);
		}


		public void Resource<TController>() where TController : Controller
		{
			new ResourceMapper<TController>(_routeCollection, _routeConfiguration).Map();
		}

		public void Resource<TController>(Action<RouteConfiguration> config)
			where TController : Controller
		{
			config(_routeConfiguration);

			new ResourceMapper<TController>(_routeCollection, _routeConfiguration).Map();
		}
	}
}
