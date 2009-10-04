using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class MapperBase<TController> where TController : Controller
	{
		protected RouteCollection _routeCollection;

		protected RouteConfiguration _configuration;

		public MapperBase(RouteCollection routeCollection)
			: this(routeCollection, RouteConfiguration.Default())
		{
		}

		public MapperBase(RouteCollection routeCollection, RouteConfiguration configuration)
		{
			_configuration = configuration;
			_routeCollection = routeCollection;
		}

		protected string ResourceName()
		{
			var controllerType = typeof(TController);

			var resource = controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length).ToLowerInvariant();

			return resource;
		}
	}
}