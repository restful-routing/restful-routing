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
            base.RegisterRoutes(routeCollection);
        }
    }
}