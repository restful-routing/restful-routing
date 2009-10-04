using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class ResourceMapper<TController> : MapperBase<TController> where TController : Controller
	{
		public ResourceMapper(RouteCollection routeCollection, RouteConfiguration configuration) : base(routeCollection, configuration)
		{
		}

		public void Map()
		{
			Map(ResourceName());
		}

		public void Map(string resource)
		{
			var controller = resource;

			if (!string.IsNullOrEmpty(_configuration.As))
				resource = _configuration.As;
			
			var basePath = _configuration.BasePath();

			// POST /session => Create
			_routeCollection.Add(new Route(
									basePath + resource,
									new RouteValueDictionary(new { action = _configuration.ActionNames.Create, controller = controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler()));

			// GET /session/new => New
			_routeCollection.Add(new Route(
									basePath + resource + "/" + _configuration.ActionNames.New,
									new RouteValueDictionary(new { action = _configuration.ActionNames.New, controller = controller }),
									new MvcRouteHandler()));

			foreach (var member in _configuration.ActionNames.MemberRoutes.Keys)
			{
				var verbArray = _configuration.ActionNames.GetMemberVerbArray(member);
				// VERB /session/member => Member
				_routeCollection.Add(new Route(
										basePath + resource + "/" + member,
										new RouteValueDictionary(new { action = member, controller }),
										new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(verbArray) }),
										new MvcRouteHandler()));
			}

			// GET /session/edit => Edit
			// GET /session/delete => Delete
			_routeCollection.Add(new Route(
									basePath + resource + "/{action}",
									new RouteValueDictionary(new { action = _configuration.ActionNames.Show, controller = controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET"), action = _configuration.ActionNames.Show + "|" + _configuration.ActionNames.Edit + "|" + _configuration.ActionNames.Delete }),
									new MvcRouteHandler()));

			// PUT /session => Update
			_routeCollection.Add(new Route(
									basePath + resource,
									new RouteValueDictionary(new { action = _configuration.ActionNames.Update, controller = controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("PUT") }),
									new MvcRouteHandler()));

			// DELETE /session => Delete
			_routeCollection.Add(new Route(
									basePath + resource,
									new RouteValueDictionary(new { action = _configuration.ActionNames.Destroy, controller = controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("DELETE") }),
									new MvcRouteHandler()));

			// HTML forms can only GET or POST... this allows us to place an override value in the form to simulate a PUT or DELETE
			// The route handler then translates the overrides to the appropriate action
			// POST /session => Update or Delete
			_routeCollection.Add(new Route(
									basePath + resource,
									new RouteValueDictionary(new { controller = controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new PostOverrideRouteHandler()));
		}
	}
}