using System.Web.Routing;
using RestfulRouting.RouteDebug;

namespace RestfulRouting.Mappers
{
    public class DebugRouteMapper : StandardMapper
    {
        string _path;

        public DebugRouteMapper(string path)
        {
            _path = path;
        }

        public override void RegisterRoutes(System.Web.Routing.RouteCollection routeCollection)
        {
            Path(_path).To<RouteDebugController>(x => x.Index());
            
            // add resources url
            routeCollection.Add(new Route(_path + "/resources/{name}",
                new RouteValueDictionary(new {controller = GetControllerName<RouteDebugController>(), action = "resources"}),
                new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
                new RouteValueDictionary(),
                RouteHandler));

            base.RegisterRoutes(routeCollection);
        }
    }
}