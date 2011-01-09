using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public abstract class Mapper
	{
		private readonly IRouteHandler _routeHandler;

		protected Mapper()
		{
			_routeHandler = new MvcRouteHandler();
		}

		protected Route GenerateRoute(string path, string controller, string action, string[] httpMethods)
		{
			return new Route(path,
				new RouteValueDictionary(new { controller, action }),
				new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(httpMethods) }),
				_routeHandler);
		}
	}
}
