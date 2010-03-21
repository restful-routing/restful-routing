using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace RestfulRouting
{
	public class ResourceMapper
	{
		private RouteNames _names;
		private string _pathPrefix;
		public string ResourceName;
		private string _resourcePath;

		public ResourceMapper(RouteNames names, string pathPrefix)
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
			return new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.ShowName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("GET")
									}),
									new MvcRouteHandler());
		}

		public Route EditRoute()
		{
			// GET /session/edit => Edit
			return new Route(
									_resourcePath + "/edit",
									new RouteValueDictionary(new { action = _names.EditName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("GET")
									}),
									new MvcRouteHandler());
		}

		public Route NewRoute()
		{
			// GET /session/new => New
			return new Route(
									_resourcePath + "/" + _names.NewName,
									new RouteValueDictionary(new { action = _names.NewName, controller = ResourceName }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler());
		}

		public Route CreateRoute()
		{
			// POST /session => Create
			return new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.CreateName, controller = ResourceName }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler());
		}

		public Route UpdateRoute()
		{
			// PUT /session => Update
			return new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.UpdateName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new RestfulHttpMethodConstraint("PUT")
									}),
									new MvcRouteHandler());
		}

		public Route DestroyRoute()
		{
			// DELETE /blogs/1 => Delete
			return new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.DestroyName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new RestfulHttpMethodConstraint("DELETE")
									}),
									new MvcRouteHandler());
		}

		public Route MemberRoute(string action, params HttpVerbs[] methods)
		{
			if (methods.Length == 0)
				methods = new[] { HttpVerbs.Get };

			return new Route(
									_resourcePath + "/" + action,
									new RouteValueDictionary(new { action = action, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new RestfulHttpMethodConstraint(methods.Select(x => x.ToString().ToUpperInvariant()).ToArray())
									}),
									new MvcRouteHandler());
		}
	}
}
