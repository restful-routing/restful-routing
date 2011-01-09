using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourcesMapper : Mapper
	{
		private RouteNames _names;
		private string _pathPrefix;
		public string ResourceName;
		private string _resourcePath;

		public ResourcesMapper(RouteNames names, string pathPrefix)
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
			if (ResourceName == null)
				ResourceName = name;
		}

		public Route IndexRoute()
		{
			// GET /blogs => Index
			var route = new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.IndexName, controller = ResourceName }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler());
			return route;
		}

		public Route ShowRoute()
		{
			// GET /blogs/1 => Show
			return new Route(
									_resourcePath + "/{id}",
									new RouteValueDictionary(new { action = _names.ShowName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("GET")
									}),
									new MvcRouteHandler());
		}

		public Route CreateRoute()
		{
			// POST /blogs => Create
			return new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.CreateName, controller = ResourceName }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler());
		}

		public Route UpdateRoute()
		{
			// PUT /blogs/1 => Update
			return new Route(
									_resourcePath + "/{id}",
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
									_resourcePath + "/{id}",
									new RouteValueDictionary(new { action = _names.DestroyName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new RestfulHttpMethodConstraint("DELETE")
									}),
									new MvcRouteHandler());
		}

		public Route NewRoute()
		{
			// GET /blogs/new => New
			return new Route(
									_resourcePath + "/" + _names.NewName,
									new RouteValueDictionary(new { action = _names.NewName, controller = ResourceName }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler());
		}

		public Route EditRoute()
		{
			// GET /blogs/1/edit => Edit
			return new Route(
									_resourcePath + "/{id}/edit",
									new RouteValueDictionary(new { action = _names.EditName, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("GET")
									}),
									new MvcRouteHandler());
		}

		public Route MemberRoute(string action, params HttpVerbs[] methods)
		{
			if (methods.Length == 0)
				methods = new []{HttpVerbs.Get};

			return new Route(
									_resourcePath + "/{id}/" + action,
									new RouteValueDictionary(new { action = action, controller = ResourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new RestfulHttpMethodConstraint(methods.Select(x => x.ToString().ToUpperInvariant()).ToArray())
									}),
									new MvcRouteHandler());
		}

		public Route CollectionRoute(string action, params HttpVerbs[] methods)
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