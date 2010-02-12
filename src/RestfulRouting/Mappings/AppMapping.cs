using System.Web.Routing;
using System.Linq;

namespace RestfulRouting.Mappings
{
    public class AppMapping<TArea> : Mapping where TArea : RestfulRoutingArea, new()
    {
        private string pathPrefix;

        public AppMapping(string pathPrefix)
        {
            this.pathPrefix = pathPrefix;
        }

        public override void AddRoutesTo(System.Web.Routing.RouteCollection routeCollection)
        {
            var routes = new RouteCollection();
            new TArea().RegisterRoutes(routes);
            foreach (var route in routes.Select(x => (Route)x))
            {
                route.Url = pathPrefix + route.Url;
                routeCollection.Add(route);
            }
        }
    }
}