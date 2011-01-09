using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public abstract class Mapper
	{
		protected Route GenerateRoute(string path, string controller, string action, string[] httpMethods)
		{
			return new Route(path,
				new RouteValueDictionary(new { controller, action }),
				new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(httpMethods) }),
				new MvcRouteHandler());
		}
	}
}
