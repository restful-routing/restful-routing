using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.RouteDebug
{
    public class RouteDebugController : Controller
    {
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
            public string Name { get; set; }
        }


        public ActionResult Index()
        {
            var model = new RouteDebugViewModel { RouteInfos = new List<RouteInfo>() };
            foreach (var route in RouteTable.Routes.Select(x => x as Route).Where(x => x != null))
            {
                var httpMethodConstraint = route.Constraints["httpMethod"] as HttpMethodConstraint;

                ICollection<string> allowedMethods = new string[] { };
                if (httpMethodConstraint != null)
                {
                    allowedMethods = httpMethodConstraint.AllowedMethods;
                }

                var namespaces = new string[] { };
                if (route.DataTokens != null && route.DataTokens["namespaces"] != null)
                    namespaces = route.DataTokens["namespaces"] as string[];
                var defaults = new RouteValueDictionary();
                if (route.Defaults != null)
                    defaults = route.Defaults;
                if (route.DataTokens == null)
                    route.DataTokens = new RouteValueDictionary();

                var namedRoute = route as NamedRoute;
                var routeName = "";
                if (namedRoute != null)
                {
                    routeName = namedRoute.Name;
                }

                model.RouteInfos.Add(new RouteInfo
                {
                    HttpMethod = string.Join(" ", allowedMethods.ToArray()),
                    Path = route.Url,
                    Endpoint = defaults["controller"] + "#" + defaults["action"],
                    Area = route.DataTokens["area"] as string,
                    Namespaces = string.Join(" ", namespaces.ToArray()),
                    Name = routeName
                });
            }

            return Content(GetOutput(model));
        }

        static string GetOutput(RouteDebugViewModel model)
        {
            var output = @"<!DOCTYPE HTML>
<html lang='en-GB'>
<head>
    <meta charset='UTF-8'>
    <title>Restful Routing</title>
    <style>
      * { margin: 0; padding: 0; }
      body {
        font: 16px Helvetica,Arial,sans-serif;
      }
    #container {
      background: #fff;
      padding: 50px;
    }
    h1 {
      font-size: 48px;
    }
    h1, h2 {
      margin: 0 0 6px 0;
      text-transform: uppercase;
    }
    p {
      margin: 0 0 20px 0;
    }
    p, h1, h2, h3, li { line-height: 1.6em; }
    a {
      color: #0E718F;
    }
    a:hover {
      text-decoration: none;
      color: #fff;
      background-color: #0E718F;
    }
    td, th {
      padding: 2px 4px;
      border-bottom: #666;
      text-align: left;
    }
    </style>
</head>
<body>
  <div id='container'>
    <h1>Routes</h1>
    <table>
      <thead>
        <tr>
            <th>Name</th>
            <th>HttpMethods</th>
            <th>Area</th>
            <th>Path</th>
            <th>Endpoint</th>
            <th>Namespaces</th>
        </tr>
      </thead>
      <tbody>
        {{table}}
      </tbody>
    </table>
  </div>
</body>
</html>";
            var rows = new List<string>();
            foreach (var routeInfo in model.RouteInfos)
            {
                rows.Add(string.Format(@"<tr>
          <td>{0}</td>
          <td>{1}</td>
          <td>{2}</td>
          <td><a href='{3}' target='_blank'>{3}</a></td>
          <td>{4}</td>
          <td>{5}</td>
        </tr>", routeInfo.Name, routeInfo.HttpMethod, routeInfo.Area, routeInfo.Path, routeInfo.Endpoint, routeInfo.Namespaces));

            }
            output = output.Replace("{{table}}", string.Join("", rows));
            return output;
        }
    }
}