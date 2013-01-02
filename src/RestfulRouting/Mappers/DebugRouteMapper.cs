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

        public override void RegisterRoutes(RouteCollection routeCollection)
        {
            Path(_path).To<RouteDebugController>(x => x.Index());

            base.RegisterRoutes(routeCollection);
        }
    }
}