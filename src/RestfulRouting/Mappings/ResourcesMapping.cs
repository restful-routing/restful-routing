using System.Linq;
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