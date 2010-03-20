using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace RestfulRouting
{
	public class RouteDebugController : Controller
	{
		public ActionResult Index()
		{
			var model = new RouteDebugViewModel{RouteInfos = new List<RouteInfo>()};
			foreach (var route in RouteTable.Routes.Select(x => (Route)x))
			{
				var httpMethodConstraint = route.Constraints["httpMethod"] as HttpMethodConstraint;

				ICollection<string> allowedMethods = new string[]{};
				if (httpMethodConstraint != null)
				{
					allowedMethods = httpMethodConstraint.AllowedMethods;
				}

				var namespaces = new string[]{};
                if (route.DataTokens != null && route.DataTokens["namespaces"] != null)
			        namespaces = route.DataTokens["namespaces"] as string[];
			    var defaults = new RouteValueDictionary();
                if (route.Defaults != null)
					defaults = route.Defaults;
				if (route.DataTokens == null)
					route.DataTokens = new RouteValueDictionary();

				model.RouteInfos.Add(new RouteInfo
				                     	{
											HttpMethod = string.Join(" ", allowedMethods.ToArray()),
											Path = route.Url,
                                            Endpoint = defaults["controller"] + "#" + defaults["action"],
											Area = route.DataTokens["area"] as string,
											Namespaces = string.Join(" ", namespaces.ToArray()),
				                     	});
			}

			return View(model);
		}
	}

	public class RouteDebugViewModel
	{
		public IList<RouteInfo> RouteInfos { get; set; }
	}

	public class RouteInfo
	{
		public string HttpMethod { get; set; }
		public string Path { get; set; }
		public string Endpoint { get; set; }
		public string Area { get; set; }
		public string Namespaces { get; set; }
	}
}