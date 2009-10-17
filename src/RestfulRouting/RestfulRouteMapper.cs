using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public interface IRestfulRouteMapper
	{
		void Resources<TController>() 
			where TController : Controller;

		void Resources<TController>(Action<RouteConfiguration> config)
			where TController : Controller;

		void Resources<TController>(Action<IRestfulRouteMapper> map)
			where TController : Controller;

		void Resources<TController>(Action<RouteConfiguration> config, Action<IRestfulRouteMapper> map)
			where TController : Controller;

		void Resource<TController>() 
			where TController : Controller;

		void Resource<TController>(Action<RouteConfiguration> config)
			where TController : Controller;

		void Namespace(string path, Action<IRestfulRouteMapper> map);
	}

	public class RestfulRouteMapper : IRestfulRouteMapper
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

		public void Resources<TController>() where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, RouteConfiguration).Map();
		}

		public void Resources<TController>(Action<RouteConfiguration> config)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, CreateConfig(config)).Map();
		}

		public void Resources<TController>(Action<IRestfulRouteMapper> map)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, RouteConfiguration).Map(map);
		}

		public void Resources<TController>(Action<RouteConfiguration> config, Action<IRestfulRouteMapper> map)
			where TController : Controller
		{
			new ResourcesMapper<TController>(_routeCollection, CreateConfig(config)).Map(map);
		}

		public void Resource<TController>() where TController : Controller
		{
			new ResourceMapper<TController>(_routeCollection, RouteConfiguration).Map();
		}

		public void Resource<TController>(Action<RouteConfiguration> config)
			where TController : Controller
		{
			new ResourceMapper<TController>(_routeCollection, CreateConfig(config)).Map();
		}

		private RouteConfiguration CreateConfig(Action<RouteConfiguration> action)
		{
			var configuration = RouteConfiguration.Default();

			configuration.PathPrefix = RouteConfiguration.PathPrefix;

			action(configuration);

			return configuration;
		}

		public void Namespace(string path, Action<IRestfulRouteMapper> map)
		{
			var mapper = new RestfulRouteMapper(_routeCollection);

			mapper.RouteConfiguration.PathPrefix = path;

			map(mapper);
		}
	}
}
