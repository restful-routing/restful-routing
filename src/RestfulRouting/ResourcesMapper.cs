using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourcesMapper
	{
		private RouteNames _names;
		private string _pathPrefix;
		public string _resourceName;
		private string _resourcePath;

		public ResourcesMapper(RouteNames names, string pathPrefix, string resourceName)
		{
			_resourceName = resourceName;
			_pathPrefix = pathPrefix;
			_names = names;
			if (!string.IsNullOrEmpty(pathPrefix))
				_resourcePath = _pathPrefix + "/" + _resourceName;
			else
				_resourcePath = _resourceName;
		}

		public void SetResourceAs(string name)
		{
			if (!string.IsNullOrEmpty(_pathPrefix))
				_resourcePath = _pathPrefix + "/" + name;
			else
				_resourcePath = name;
		}

		public Route IndexRoute()
		{
			// GET /blogs => Index
			var route = new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _names.IndexName, controller = _resourceName }),
                                    new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler());
			return route;
		}

		public Route ShowRoute()
		{
			// GET /blogs/1 => Show
			return new Route(
									_resourcePath + "/{id}",
									new RouteValueDictionary(new { action = _names.ShowName, controller = _resourceName }),
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
									new RouteValueDictionary(new { action = _names.CreateName, controller = _resourceName }),
                                    new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler());
		}

		public Route UpdateRoute()
		{
			// PUT /blogs/1 => Update
			return new Route(
									_resourcePath + "/{id}",
									new RouteValueDictionary(new { action = _names.UpdateName, controller = _resourceName }),
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
									new RouteValueDictionary(new { action = _names.DestroyName, controller = _resourceName }),
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
									new RouteValueDictionary(new { action = _names.NewName, controller = _resourceName }),
                                    new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler());
		}

		public Route EditRoute()
		{
			// GET /blogs/1/edit => Edit
			return new Route(
									_resourcePath + "/{id}/edit",
									new RouteValueDictionary(new { action = _names.EditName, controller = _resourceName }),
									new RouteValueDictionary(new
									{
                                        httpMethod = new HttpMethodConstraint("GET")
									}),
									new MvcRouteHandler());
		}

		public Route MemberRoute(string action, params HttpVerbs[] methods)
		{
			return new Route(
									_resourcePath + "/{id}/" + action,
									new RouteValueDictionary(new { action = action, controller = _resourceName }),
									new RouteValueDictionary(new
									{
                                        httpMethod = new RestfulHttpMethodConstraint(methods.Select(x => x.ToString().ToUpperInvariant()).ToArray())
									}),
									new MvcRouteHandler());
		}

		public Route CollectionRoute(string action, params HttpVerbs[] methods)
		{
			return new Route(
									_resourcePath + "/" + action,
									new RouteValueDictionary(new { action = action, controller = _resourceName }),
									new RouteValueDictionary(new
									{
										httpMethod = new RestfulHttpMethodConstraint(methods.Select(x => x.ToString().ToUpperInvariant()).ToArray())
									}),
									new MvcRouteHandler());
		}
	}
}