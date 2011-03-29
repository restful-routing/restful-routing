using System.Web.Routing;

namespace RestfulRouting.Mappers
{
    public class RouteMapper : Mapper
    {
        RouteBase _routeBase;

        public RouteMapper(RouteBase routeBase)
        {
            _routeBase = routeBase;
        }

        public override void RegisterRoutes(RouteCollection routeCollection)
        {
            routeCollection.Add(_routeBase);
        }
    }
}
