using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace RestfulRouting
{
	public class ResourceMapper : Mapper
	{
		private RouteNames _names;
		private string _pathPrefix;
		public string ResourceName;
		private string _resourcePath;

		public ResourceMapper(RouteNames names, string pathPrefix, IRouteHandler routeHandler)
			: base(routeHandler)
		{
			_pathPrefix = pathPrefix;
			_names = names;
		}

		public void SetResourceAs(string name)
		{
			if (!string.IsNullOrEmpty(_pathPrefix))
				_resourcePath = _pathPrefix + "/" + name;
			else
				_resourcePath = name;
		}

		public Route ShowRoute()
		{
			// GET /session => Show
			return GenerateRoute(_resourcePath, ResourceName, _names.ShowName, new[] { "GET" });
		}

		public Route EditRoute()
		{
			// GET /session/edit => Edit
			return GenerateRoute(_resourcePath + "/" + _names.EditName, ResourceName, _names.EditName, new[] { "GET" });
		}

		public Route NewRoute()
		{
			// GET /session/new => New
			return GenerateRoute(_resourcePath + "/" + _names.NewName, ResourceName, _names.NewName, new[] { "GET" });
		}

		public Route CreateRoute()
		{
			// POST /session => Create
			return GenerateRoute(_resourcePath, ResourceName, _names.CreateName, new[] { "POST" });
		}

		public Route UpdateRoute()
		{
			// PUT /session => Update
			return GenerateRoute(_resourcePath, ResourceName, _names.UpdateName, new[] { "PUT" });
		}

		public Route DestroyRoute()
		{
			// DELETE /blogs/1 => Delete
			return GenerateRoute(_resourcePath, ResourceName, _names.DestroyName, new[] { "DELETE" });
		}

		public Route MemberRoute(string action, params HttpVerbs[] methods)
		{
			if (methods.Length == 0)
				methods = new[] { HttpVerbs.Get };

			return GenerateRoute(_resourcePath + "/" + action, ResourceName, action, methods.Select(x => x.ToString().ToUpperInvariant()).ToArray());
		}
	}
}
