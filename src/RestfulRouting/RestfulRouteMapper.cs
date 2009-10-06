using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class RestfulRouteMapper
	{
		protected RouteCollection _routeCollection;
		public RouteConfiguration RouteConfiguration { get; private set; }

		public RestfulRouteMapper(RouteCollection routeCollection) : this(routeCollection, RouteConfiguration.Default())
		{
		}

		public RestfulRouteMapper(RouteCollection routeCollection, RouteConfiguration routeConfiguration)
		{
			_routeCollection = routeCollection;
			RouteConfiguration = routeConfiguration;
		}

		public RestfulRouteMapper WithConfiguration(Action<RouteConfiguration> config)
		{
			return new RestfulRouteMapper(_routeCollection, CloneAndAlterConfig(config));
		}

		public void Resources<TController>() where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, RouteConfiguration).Map();
		}

		public void Resources<TController>(Action<RouteConfiguration> config)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, CloneAndAlterConfig(config));
		}

		public void Resources<TController>(Action<RestfulRouteMapper> map)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, RouteConfiguration).Map(map);
		}

		public void Resources<TController>(Action<RouteConfiguration> config, Action<RestfulRouteMapper> map)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, CloneAndAlterConfig(config)).Map(map);
		}

		public void Resource<TController>() where TController : Controller
		{
			new ResourceMapper<TController>(_routeCollection, RouteConfiguration).Map();
		}

		public void Resource<TController>(Action<RouteConfiguration> config)
			where TController : Controller
		{
			new ResourceMapper<TController>(_routeCollection, CloneAndAlterConfig(config)).Map();
		}

		private RouteConfiguration CloneAndAlterConfig(Action<RouteConfiguration> action)
		{
			var configuration = RouteConfiguration.Clone();
			action(configuration);
			return configuration;
		}
	}
}
