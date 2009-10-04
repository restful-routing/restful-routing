using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace RestfulRouting
{
	public class ResourcesMapper<TController> : MapperBase<TController> where TController : Controller
	{
		public ResourcesMapper(RouteCollection routeCollection)
			: base(routeCollection)
		{
		}

		public ResourcesMapper(RouteCollection routeCollection, RouteConfiguration configuration) : base(routeCollection, configuration)
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

			// GET /blogs => Index
			_routeCollection.Add(new Route(
									basePath + resource,
									new RouteValueDictionary(new { action = _configuration.ActionNames.Index, controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler()));

			if (_configuration.Shallow)
			{
				if (!string.IsNullOrEmpty(basePath))
				{
					basePath = "";

					// map the root index as well as the nested one
					_routeCollection.Add(new Route(
											basePath + resource,
											new RouteValueDictionary(new { action = _configuration.ActionNames.Index, controller }),
											new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
											new MvcRouteHandler()));
				}
				basePath = "";
			}

			// POST /blogs => Create
			_routeCollection.Add(new Route(
									basePath + resource,
									new RouteValueDictionary(new { action = _configuration.ActionNames.Create, controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler()));

			// GET /blogs/new => New
			_routeCollection.Add(new Route(
									basePath + resource + "/" + _configuration.ActionNames.New,
									new RouteValueDictionary(new { action = _configuration.ActionNames.New, controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler()));

			foreach (var member in _configuration.ActionNames.MemberRoutes.Keys)
			{
				var verbArray = _configuration.ActionNames.GetMemberVerbArray(member);
				// VERB /blogs/1/member => Member
				_routeCollection.Add(new Route(
										basePath + resource + "/{id}/" + member,
										new RouteValueDictionary(new { action = member, controller }),
										new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(verbArray) }),
										new MvcRouteHandler()));
			}

			foreach (var member in _configuration.ActionNames.CollectionRoutes.Keys)
			{
				var verbArray = _configuration.ActionNames.CollectionRoutes[member].Select(x => x.ToString().ToUpperInvariant()).ToList().ToArray();
				// VERB /blogs/member => Member
				_routeCollection.Add(new Route(
										basePath + resource + "/" + member,
										new RouteValueDictionary(new { action = member, controller }),
										new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(verbArray) }),
										new MvcRouteHandler()));
			}

			// GET /blogs/show => Show
			// GET /blogs/edit => Edit
			// GET /blogs/delete => Delete
			_routeCollection.Add(new Route(
									basePath + resource + "/{id}/{action}",
									new RouteValueDictionary(new { action = _configuration.ActionNames.Show, controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET"), action = _configuration.ActionNames.Show + "|" + _configuration.ActionNames.Edit + "|" + _configuration.ActionNames.Delete }),
									new MvcRouteHandler()));

			// PUT /blogs/1 => Update
			_routeCollection.Add(new Route(
									basePath + resource + "/{id}",
									new RouteValueDictionary(new { action = _configuration.ActionNames.Update, controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("PUT") }),
									new MvcRouteHandler()));

			// DELETE /blogs/1 => Delete
			_routeCollection.Add(new Route(
									basePath + resource + "/{id}",
									new RouteValueDictionary(new { action = _configuration.ActionNames.Destroy, controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("DELETE") }),
									new MvcRouteHandler()));

			// HTML forms can only GET or POST... this allows us to place an override value in the form to simulate a PUT or DELETE
			// The route handler then translates the overrides to the appropriate action
			// POST /blogs/1 => Update or Delete
			_routeCollection.Add(new Route(
									basePath + resource + "/{id}",
									new RouteValueDictionary(new { controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new PostOverrideRouteHandler()));
		}

		public void Map(Action<RestfulRouteMapper> map)
		{
			Map(ResourceName(), map);
		}

		public void Map(string resource, Action<RestfulRouteMapper> map)
		{
			Map(resource);

			var singular = Inflector.Net.Inflector.Singularize(resource).ToLowerInvariant();

			if (_configuration == null)
				_configuration = RouteConfiguration.Default();

			var configuration = _configuration.Clone();

			if (configuration.Shallow)
				configuration.PathPrefix = resource + "/{" + singular + "Id}";
			else
				configuration.PathPrefix = _configuration.BasePath() + resource + "/{" + singular + "Id}";

			var mapper = new RestfulRouteMapper(_routeCollection, configuration);

			map(mapper);
		}
	}
}