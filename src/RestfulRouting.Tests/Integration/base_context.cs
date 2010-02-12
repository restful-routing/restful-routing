using System;
using System.IO;
using System.Web.Routing;
using Machine.Specifications;
using System.Linq;

namespace RestfulRouting.Tests.Integration
{
	[Subject(typeof(RestfulRoutingArea))]
	public class base_context
	{
		protected static RouteCollection routes = RouteTable.Routes;

		Establish context = () => routes.Clear();

		public static void PrintRoutes(RouteCollection routes)
		{
			var file = File.CreateText("c:\\code\\routes.txt");
			file.WriteLine("method | path | controller#action | area");
			foreach (var route in routes.Select(x => (Route)x))
			{
				file.WriteLine(
					string.Join(" ", ((HttpMethodConstraint)route.Constraints["httpMethod"]).AllowedMethods.ToArray()) + " | " +
					route.Url + " | " +
					route.Defaults["controller"] + "#" + route.Defaults["action"] + " | " +
					route.Constraints["area"]
					);
			}
			file.Close();
		}
	}
}