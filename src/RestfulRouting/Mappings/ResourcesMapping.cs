using System.Linq;
using System.Web.Routing;

namespace RestfulRouting.Mappings
{
    public class ResourcesMapping<TController> : Mapping
    {
        private ResourcesMapper _resourcesMapper;

        protected string _resourcePath;

        private RouteNames names;

        public ResourcesMapping(RouteNames routeNames, ResourcesMapper resourcesMapper)
        {
            names = routeNames;

            ResourceName = ControllerName<TController>();

            _resourcesMapper = resourcesMapper;
        }

        protected void ConfigureRoute(Route route)
        {
            if (Context.Constraints != null && Context.Constraints.Count > 0)
            {
                foreach (var constraint in Context.Constraints)
                {
                    route.Constraints.Add(constraint.Key, constraint.Value);
                }
            }
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
        	_resourcesMapper.SetResourceAs(MappedName ?? ResourceName);

			if (Collections != null && Collections.Any())
			{
				foreach (var member in Collections)
				{
					routeCollection.Add(_resourcesMapper.CollectionRoute(member.Key, member.Value));
				}
			}

            if (IncludesAction(names.IndexName))
                routeCollection.Add(_resourcesMapper.IndexRoute());

            if (IncludesAction(names.CreateName))
                routeCollection.Add(_resourcesMapper.CreateRoute());

            if (IncludesAction(names.NewName))
                routeCollection.Add(_resourcesMapper.NewRoute());

            if (IncludesAction(names.EditName))
                routeCollection.Add(_resourcesMapper.EditRoute());

            if (IncludesAction(names.ShowName))
                routeCollection.Add(_resourcesMapper.ShowRoute());

            if (IncludesAction(names.UpdateName))
                routeCollection.Add(_resourcesMapper.UpdateRoute());

            if (IncludesAction(names.DestroyName))
                routeCollection.Add(_resourcesMapper.DestroyRoute());

            foreach (var route in routeCollection)
            {
                ConfigureRoute(route as Route);
            }

            if (Members != null && Members.Any())
            {
                foreach (var member in Members)
                {
                    routeCollection.Add(_resourcesMapper.MemberRoute(member.Key, member.Value));
                }
            }

            foreach (var mapping in Mappings)
            {
                mapping.AddRoutesTo(routeCollection);
            }
        }


    }
}