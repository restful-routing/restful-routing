using System.Web.Routing;

namespace RestfulRouting.Mappers
{
    public class ConnectMapper<TRouteSet> : Mapper where TRouteSet : RouteSet, new()
    {
        private readonly string _path;

        public ConnectMapper(string path)
        {
            _path = path;
        }

        public override void RegisterRoutes(RouteCollection routeCollection)
        {
            var routes = new RouteCollection();
            var routeSet = new TRouteSet();
            routeSet.RegisterRoutes(routes);
            foreach (var routeBase in routes)
            {
                var route = routeBase as Route;
                if (route != null)
                {
                    route.Url = Join(BasePath, _path, route.Url);
                }
                routeCollection.Add(route);
            }
        }
    }
}
