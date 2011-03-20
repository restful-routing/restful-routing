using System.Collections.Generic;
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

    	    var routes = new List<Route>();

			if (Collections != null && Collections.Any())
			{
				foreach (var member in Collections)
				{
					routes.Add(_resourcesMapper.CollectionRoute(member.Key, member.Value));
				}
			}

            if (IncludesAction(names.IndexName))
                routes.Add(_resourcesMapper.IndexRoute());

            if (IncludesAction(names.CreateName))
                routes.Add(_resourcesMapper.CreateRoute());

            if (IncludesAction(names.NewName))
                routes.Add(_resourcesMapper.NewRoute());

            if (IncludesAction(names.EditName))
                routes.Add(_resourcesMapper.EditRoute());

            if (IncludesAction(names.ShowName))
                routes.Add(_resourcesMapper.ShowRoute());

            if (IncludesAction(names.UpdateName))
                routes.Add(_resourcesMapper.UpdateRoute());

            if (IncludesAction(names.DestroyName))
                routes.Add(_resourcesMapper.DestroyRoute());

            if (Members != null && Members.Any())
            {
                foreach (var member in Members)
                {
                    routes.Add(_resourcesMapper.MemberRoute(member.Key, member.Value));
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

                newConstraints[Inflector.Singularize(_resourcesMapper.ResourceName) + "Id"] = idConstraint.Value;
            }

            foreach (var mapping in Mappings)
            {
                mapping.Context.Constraints = newConstraints;

                mapping.AddRoutesTo(routeCollection);
            }
        }


    }
}