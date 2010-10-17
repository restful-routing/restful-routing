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


			if (IncludesAction(_names.ShowName))
				routeCollection.Add(_resourceMapper.ShowRoute());

			if (IncludesAction(_names.UpdateName))
				routeCollection.Add(_resourceMapper.UpdateRoute());

			if (IncludesAction(_names.NewName))
				routeCollection.Add(_resourceMapper.NewRoute());

			if (IncludesAction(_names.EditName))
				routeCollection.Add(_resourceMapper.EditRoute());

			if (IncludesAction(_names.DestroyName))
				routeCollection.Add(_resourceMapper.DestroyRoute());

			if (IncludesAction(_names.CreateName))
				routeCollection.Add(_resourceMapper.CreateRoute());

			foreach (var route in routeCollection)
			{
				ConfigureRoute(route as Route);
			}

			if (Members != null && Members.Any())
			{
				foreach (var member in Members)
				{
					routeCollection.Add(_resourceMapper.MemberRoute(member.Key, member.Value));
				}
			}

			foreach (var mapping in Mappings)
			{
				mapping.AddRoutesTo(routeCollection);
			}
		}
	}
}
