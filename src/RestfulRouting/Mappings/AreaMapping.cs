using System.Web.Routing;
using System.Linq;

namespace RestfulRouting.Mappings
{
    public class AreaMapping<TController> : Mapping
    {
        private string _namespace;
        private string _area;
        private string _pathPrefix;

        public AreaMapping(string area) : this(area, area)
        {
            
        }

        public AreaMapping(string areaName, string pathPrefix)
        {
            _pathPrefix = pathPrefix;
            _area = areaName;
            _namespace = typeof(TController).Namespace;
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
            var routes = new RouteCollection();
            foreach (var mapping in Mappings)
            {
                mapping.AddRoutesTo(routes);
            }

            foreach (var route in routes.Select(x => (Route)x))
            {
                if (!string.IsNullOrEmpty(_pathPrefix))
                    route.Url = _pathPrefix + "/" + route.Url;
                ConstrainArea(route);
                routeCollection.Add(route);
            }
        }

        private void ConstrainArea(Route route)
        {
            if (route.DataTokens == null)
                route.DataTokens = new RouteValueDictionary();
            route.DataTokens["namespaces"] = new[]{ _namespace };
            if (!string.IsNullOrEmpty(_area))
            {
                route.DataTokens["area"] = _area;
            }
            route.DataTokens["UseNamespaceFallback"] = false;
        }
    }

    public class AreaMapping : Mapping
    {
        private string _area;

        public AreaMapping(string area)
        {
            _area = area;
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
            var routes = new RouteCollection();
            foreach (var mapping in Mappings)
            {
                mapping.AddRoutesTo(routes);
            }

            foreach (var route in routes.Select(x => (Route)x))
            {
                if (!string.IsNullOrEmpty(_area))
                    route.Url = _area + "/" + route.Url;
                routeCollection.Add(route);
            }
        }
    }
}