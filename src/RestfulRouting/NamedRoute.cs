using System.Diagnostics;
using System.Web.Routing;

namespace RestfulRouting
{
    [DebuggerDisplay("Name = {Name}, Url = {Url}")]
    public class NamedRoute : Route
    {
        public string Name { get; set; }

        public NamedRoute(string name, string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
            Name = name;
        }
  
        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            Name = name;
        }
  
        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            Name = name;
        }
    }
}