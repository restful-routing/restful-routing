using System.Web.Routing;

namespace RestfulRouting.Mappings
{
    public class RouteMapping : Mapping
    {
        public RouteBase Route;

        public RouteMapping(RouteBase route)
        {
            Route = route;
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
            routeCollection.Add(Route);
        }
    }
}
