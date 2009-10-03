using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourceMapper
	{
		private RouteCollection _routeCollection;
		private string _basePath;

		public ResourceMapper(RouteCollection routeCollection) : this(routeCollection, "")
		{
		}

		public ResourceMapper(RouteCollection routeCollection, string basePath)
		{
			_basePath = VirtualPathUtility.RemoveTrailingSlash(basePath);
			if (!string.IsNullOrEmpty(_basePath))
			{
				_basePath = _basePath + "/";
			}
			_routeCollection = routeCollection;
		}


		public void Map(string resource)
		{

			// POST /session => Create
			_routeCollection.Add(new Route(
									_basePath + resource,
									new RouteValueDictionary(new { action = "create", controller = resource }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler()));

			// GET /session/new => New
			_routeCollection.Add(new Route(
									_basePath + resource + "/new",
									new RouteValueDictionary(new { action = "new", controller = resource }),
									new MvcRouteHandler()));

			// GET /session/edit => Edit
			// GET /session/delete => Delete
			_routeCollection.Add(new Route(
									_basePath + resource + "/{action}",
									new RouteValueDictionary(new { action = "show", controller = resource }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET"), action = "show|edit|delete" }),
									new MvcRouteHandler()));

			// PUT /session/1 => Update
			_routeCollection.Add(new Route(
									_basePath + resource,
									new RouteValueDictionary(new { action = "update", controller = resource }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("PUT") }),
									new MvcRouteHandler()));

			// DELETE /session/1 => Delete
			_routeCollection.Add(new Route(
									_basePath + resource,
									new RouteValueDictionary(new { action = "destroy", controller = resource }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("DELETE") }),
									new MvcRouteHandler()));

			// HTML forms can only GET or POST... this allows us to place an override value in the form to simulate a PUT or DELETE
			// The route handler then translates the overrides to the appropriate action
			// POST /session/1 => Update or Delete
			_routeCollection.Add(new Route(
									_basePath + resource,
									new RouteValueDictionary(new { controller = resource }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new PostOverrideRouteHandler()));
		}
	}
}