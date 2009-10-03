using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourceMapper
	{
		private RouteCollection _routeCollection;

		public ResourceMapper(RouteCollection routeCollection)
		{
			_routeCollection = routeCollection;
		}


		public void Map(string controller)
		{

			// POST /session => Create
			_routeCollection.Add(new Route(
									controller,
									new RouteValueDictionary(new { action = "create", controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler()));

			// GET /session/new => New
			_routeCollection.Add(new Route(
									controller + "/new",
									new RouteValueDictionary(new { action = "new", controller }),
									new MvcRouteHandler()));

			// GET /session/edit => Edit
			// GET /session/delete => Delete
			_routeCollection.Add(new Route(
									controller + "/{action}",
									new RouteValueDictionary(new { action = "show", controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET"), action = "show|edit|delete" }),
									new MvcRouteHandler()));

			// PUT /session/1 => Update
			_routeCollection.Add(new Route(
									controller,
									new RouteValueDictionary(new { action = "update", controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("PUT") }),
									new MvcRouteHandler()));

			// DELETE /session/1 => Delete
			_routeCollection.Add(new Route(
									controller,
									new RouteValueDictionary(new { action = "destroy", controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("DELETE") }),
									new MvcRouteHandler()));

			// HTML forms can only GET or POST... this allows us to place an override value in the form to simulate a PUT or DELETE
			// The route handler then translates the overrides to the appropriate action
			// POST /session/1 => Update or Delete
			_routeCollection.Add(new Route(
									controller,
									new RouteValueDictionary(new { controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new PostOverrideRouteHandler()));
		}
	}
}