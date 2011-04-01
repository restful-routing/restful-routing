using System;
using System.Linq;
using System.Web.Routing;

namespace RestfulRouting.Mappers
{
    public interface IAreaMapper : IMapper
    {
    }

    public class AreaMapper : Mapper, IAreaMapper
    {
        string _areaName;
        string _ns;
        Action<AreaMapper> _subMapper;

        public AreaMapper(string areaName, string _namespace = null, Action<AreaMapper> subMapper = null)
        {
            _areaName = areaName;
            _ns = _namespace;
            _subMapper = subMapper;
        }

        public override void RegisterRoutes(RouteCollection routeCollection)
        {
            if (_subMapper != null)
            {
                _subMapper.Invoke(this);
            }

            var routes = new RouteCollection();

            BasePath = Join(BasePath, _areaName);
            AddResourcePath(_areaName);
            RegisterNested(routes, mapper => mapper.SetParentResources(ResourcePaths));

            foreach (var route in routes.Select(x => (Route)x))
            {
                ConstrainArea(route);
                routeCollection.Add(route);
            }
        }

        private void ConstrainArea(Route route)
        {
            if (route.DataTokens == null)
                route.DataTokens = new RouteValueDictionary();
            if (!string.IsNullOrEmpty(_ns))
                route.DataTokens["namespaces"] = new[] { _ns };
            if (!string.IsNullOrEmpty(_areaName))
                route.DataTokens["area"] = _areaName;
            route.DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
