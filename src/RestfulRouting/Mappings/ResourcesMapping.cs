using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Mappings
{
    public class ResourcesMapping<TController> : Mapping
    {
        private ResourcesMapper _resourcesMapper;

        private RouteNames names;

        public ResourcesMapping(RouteNames routeNames, ResourcesMapper resourcesMapper)
        {
            names = routeNames;

            ResourceName = ControllerName<TController>();

            resourcesMapper.SetResourceAs(ResourceName);

            _resourcesMapper = resourcesMapper;
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
            _resourcesMapper.SetResourceAs(MappedName ?? ResourceName);

            var routes = new List<Route>();

            if (Collections != null && Collections.Any())
            {
                foreach (var member in Collections)
                {
                    routes.AddRange(_resourcesMapper.CollectionRoute(member.Key, member.Value).ExplicitAndImplicit());
                }
            }

            if (IncludesAction(names.IndexName))
                routes.AddRange(_resourcesMapper.IndexRoute().ExplicitAndImplicit());


            if (IncludesAction(names.CreateName))
                routes.AddRange(_resourcesMapper.CreateRoute().ExplicitAndImplicit());
                

            if (IncludesAction(names.NewName))
                routes.AddRange(_resourcesMapper.NewRoute().ExplicitAndImplicit());

            if (IncludesAction(names.EditName))
                routes.AddRange(_resourcesMapper.EditRoute().ExplicitAndImplicit());

            if (IncludesAction(names.ShowName))
                routes.AddRange(_resourcesMapper.ShowRoute().ExplicitAndImplicit());

            if (IncludesAction(names.UpdateName))
                routes.AddRange(_resourcesMapper.UpdateRoute().ExplicitAndImplicit());

            if (IncludesAction(names.DestroyName))
                routes.AddRange(_resourcesMapper.DestroyRoute().ExplicitAndImplicit());

            if (Members != null && Members.Any())
            {
                foreach (var member in Members)
                {
                    routes.AddRange(_resourcesMapper.MemberRoute(member.Key, member.Value).ExplicitAndImplicit());
                }
            }

            foreach (var route in routes)
            {
                ConfigureRoute(route);
                routeCollection.Add(route);
            }

            var newConstraints = Context.Constraints;
            
            if (newConstraints.ContainsKey("id"))
            {
                var idConstraint = newConstraints.FirstOrDefault(x => x.Key == "id");

                newConstraints.Remove("id");

                newConstraints[RouteSet.Singularize(_resourcesMapper.ResourceName) + "Id"] = idConstraint.Value;
            }

            foreach (var mapping in Mappings)
            {
                mapping.Context.Constraints = newConstraints;

                mapping.AddRoutesTo(routeCollection);
            }
        }

    }
}