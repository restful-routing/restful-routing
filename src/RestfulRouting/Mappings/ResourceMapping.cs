using System.Collections.Generic;
using System.Linq;
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
                routes.Add(_resourceMapper.ShowRoute());

			if (IncludesAction(_names.UpdateName))
                routes.Add(_resourceMapper.UpdateRoute());

			if (IncludesAction(_names.NewName))
                routes.Add(_resourceMapper.NewRoute());

			if (IncludesAction(_names.EditName))
                routes.Add(_resourceMapper.EditRoute());

			if (IncludesAction(_names.DestroyName))
                routes.Add(_resourceMapper.DestroyRoute());

			if (IncludesAction(_names.CreateName))
                routes.Add(_resourceMapper.CreateRoute());

			if (Members != null && Members.Any())
			{
				foreach (var member in Members)
				{
					routes.Add(_resourceMapper.MemberRoute(member.Key, member.Value));
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
