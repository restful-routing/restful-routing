using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.RouteDebug
{
    public class RouteDebugController : Controller
    {
        public class RouteDebugViewModel
        {
            public string DebugPath { get; set; }
            public IList<RouteInfo> RouteInfos { get; set; }

            public string GetPath(string name)
            {
                return DebugPath + name;
            }
        }

        public class RouteInfo
        {
            public int Position { get; set; }
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
            int position = 1;
            foreach (var route in RouteTable.Routes.Select(x => x as Route).Where(x => x != null))
            {
				// issue: #33 Fix
                var httpMethodConstraint = (route.Constraints ?? new RouteValueDictionary())["httpMethod"] as HttpMethodConstraint;

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
                    Position = position,
                    HttpMethod = string.Join(" ", allowedMethods.ToArray()),
                    Path = route.Url,
                    Endpoint = defaults["controller"] + "#" + defaults["action"],
                    Area = route.DataTokens["area"] as string,
                    Namespaces = string.Join(" ", namespaces.ToArray()),
                    Name = routeName
                });
                position++;
            }

            var debugPath = (from p in model.RouteInfos
                             where p.Endpoint.Equals("routedebug#resources", StringComparison.InvariantCultureIgnoreCase)
                             select p.Path.Replace("{name}", string.Empty)).FirstOrDefault();
            model.DebugPath = debugPath;
     
            return Content(Debugger(model));
        }

        public string Debugger(RouteDebugViewModel model) 
        {
            var style = model.GetPath("style.css");
            var jqueryDatatables = model.GetPath("jquery.dataTables.min.js");

            var routeInfos = new StringBuilder();

            foreach (var routeinfo in model.RouteInfos) 
            {
                routeInfos.AppendLine("<tr>");
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Position);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Name);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.HttpMethod);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Area);
                routeInfos.AppendFormat("<td class='path'><a href='{0}' target='_blank'>{0}</a></td>", routeinfo.Path);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Endpoint);
                routeInfos.AppendFormat("<td>{0}</td>", routeinfo.Namespaces);
                routeInfos.AppendLine("</tr>");
            }

            return string.Format(HtmlPage, style, jqueryDatatables, routeInfos);
        }

        public ActionResult Resources(string name)
        {
            try
            {
                var content = GetContent(name);
                var contentType = "text/html";

                var extension = Path.GetExtension(name).ToLowerInvariant();

                switch (extension) {
                    case ".css":
                        contentType = "text/css";
                        break;
                    case ".jpg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".js":
                        contentType = "text/javascript";
                        break;
                    case ".ttf":
                        contentType = "application/x-font-ttf";
                        break;
                    case ".woff":
                        contentType = "application/x-woff";
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrWhiteSpace(content) || extension == ".cshtml")
                    return HttpNotFound();

                return Content(content, contentType, Encoding.UTF8);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
     
        private string GetContent(string name)
        {
            try 
            {
                using (var reader = new StreamReader(GetType().Assembly.GetManifestResourceStream(string.Format("RestfulRouting.RouteDebug.Content.{0}", name))))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e) {
                return string.Empty;
            }
        }

        const string HtmlPage =
@"<!DOCTYPE HTML>
<html lang='en-GB'>
<head>
    <meta charset='UTF-8'>
    <title>Restful Routing</title>
    <link href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.10/themes/hot-sneaks/jquery-ui.css' rel='stylesheet' type='text/css' />
    <link href='{0}' rel='stylesheet' type='text/css' />
    <script src='http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.1.min.js' type='text/javascript'></script>
    <script src='{1}' type='text/javascript'></script>
    <script type='text/javascript'>
        $(document).ready(function () {{
            $('#routes').dataTable({{
                'bJQueryUI': true,
                'sPaginationType': 'full_numbers',
                'bLengthChange': false,
                'bAutoWidth': true,
                'bPaginate':false
            }});
        }});
    </script>
</head>
<body>
  <div id='container'>
    <div id='header'>
        <h1>restful-routing</h1>
        <h2>route debugger</h2>
        <p>
            <a href='http://stevehodgkiss.github.com/restful-routing/'>For more help go to the wiki page at http://stevehodgkiss.github.com/restful-routing/ </a>
        </p>
    </div>
    <div id='routes-container'>
        <table id='routes'>
          <thead>
            <tr>
                <th>Position</th>
                <th>Name</th>
                <th>HttpMethods</th>
                <th>Area</th>
                <th>Path</th>
                <th>Endpoint</th>
                <th>Namespaces</th>
            </tr>
          </thead>
          <tbody>
            {2}
          </tbody>
        </table>
    </div>
  </div>
  <a href='http://github.com/stevehodgkiss/restful-routing' id='github'>
      <img src='http://s3.amazonaws.com/github/ribbons/forkme_right_darkblue_121621.png'>
    </a>
</body>
</html>";
    }
}