using System;
using System.Web.Routing;

namespace RestfulRouting
{
	public abstract class Mapper
	{
		protected Route GenerateRoute(string path, string controller, string action, string[] httpMethods)
		{
			throw new NotImplementedException();
		}
	}
}
