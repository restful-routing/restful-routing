using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Mappings
{
    public class ResourceMapping<TController> : Mapping
    {
        private RouteNames _names;

        private ResourceMapper _resourceMapper;

        public ResourceMapping(RouteNames names, ResourceMapper resourceMapper)
        {
            _names = names;

            ResourceName = ControllerName<TController>();

            MappedName = Inflector.Singularize(ResourceName);

            resourceMapper.ResourceName = ResourceName;

            _resourceMapper = resourceMapper;
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
            _resourceMapper.SetResourceAs(MappedName ?? ResourceName);

            var routes = new List<Route>();

            if (IncludesAction(_names.ShowName))
                routes.AddRange(_resourceMapper.ShowRoute().ExplicitAndImplicit());

            if (IncludesAction(_names.UpdateName))
                routes.AddRange(_resourceMapper.UpdateRoute().ExplicitAndImplicit());

            if (IncludesAction(_names.NewName))
                routes.AddRange(_resourceMapper.NewRoute().ExplicitAndImplicit());

            if (IncludesAction(_names.EditName))
                routes.AddRange(_resourceMapper.EditRoute().ExplicitAndImplicit());

            if (IncludesAction(_names.DestroyName))
                routes.AddRange(_resourceMapper.DestroyRoute().ExplicitAndImplicit());

            if (IncludesAction(_names.CreateName))
                routes.AddRange(_resourceMapper.CreateRoute().ExplicitAndImplicit());

            if (Members != null && Members.Any())
            {
                foreach (var member in Members)
                {
                    routes.AddRange(_resourceMapper.MemberRoute(member.Key, member.Value).ExplicitAndImplicit());
                }
            }

            foreach (var route in routes)
            {
                ConfigureRoute(route);
                routeCollection.Add(route);
            }

            foreach (var mapping in Mappings)
            {
                mapping.AddRoutesTo(routeCollection);
            }
        }
    }
}
