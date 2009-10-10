using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public abstract class MapperBase<TController> where TController : Controller
	{
		protected RouteCollection _routeCollection;

		protected RouteConfiguration _configuration;

		protected string _resourcePath;

		protected string _idSegment;


		protected string _controller;

		protected MapperBase(RouteCollection routeCollection, RouteConfiguration configuration)
		{
			_configuration = configuration;

			_routeCollection = routeCollection;
		}

		protected string ResourceName()
		{
			var controllerType = typeof(TController);

			var resource = controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length).ToLowerInvariant();

			return resource;
		}

		protected string BasePath()
		{
			var basePath = VirtualPathUtility.RemoveTrailingSlash(_configuration.PathPrefix);

			if (!string.IsNullOrEmpty(basePath))
			{
				basePath = basePath + "/";
			}

			return basePath ?? string.Empty;
		}

		protected void MapIndex()
		{
			// GET /blogs => Index
			_routeCollection.Add(new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _configuration.IndexName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler()));
		}

		protected void MapCreate()
		{
			// POST /blogs => Create
			_routeCollection.Add(new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _configuration.CreateName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler()));
		}

		protected void MapNew()
		{
			// GET /blogs/new => New
			_routeCollection.Add(new Route(
									_resourcePath + "/" + _configuration.NewName,
									new RouteValueDictionary(new { action = _configuration.NewName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler()));
		}

		protected void MapMembers()
		{
			// GET /blogs/1 => Show
			// GET /blogs/1/edit => Edit
			// GET /blogs/1/delete => Delete
			_routeCollection.Add(new Route(
									_resourcePath + _idSegment + "/{action}",
									new RouteValueDictionary(new { action = _configuration.ShowName, controller = _controller }),
									new RouteValueDictionary(new
									                         	{
									                         		httpMethod = new HttpMethodConstraint("GET"), 
																	action = _configuration.ShowName + "|" +
																		_configuration.NewName + "|" +
																		_configuration.EditName + "|" + 
																		_configuration.DeleteName
									                         	}),
									new MvcRouteHandler()));
		}

		protected void MapUpdate()
		{
			// PUT /blogs/1 => Update
			_routeCollection.Add(new Route(
									_resourcePath + _idSegment,
									new RouteValueDictionary(new { action = _configuration.UpdateName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("PUT") }),
									new MvcRouteHandler()));
		}

		protected void MapDestroy()
		{
			// DELETE /blogs/1 => Delete
			_routeCollection.Add(new Route(
									_resourcePath + _idSegment,
									new RouteValueDictionary(new { action = _configuration.DestroyName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("DELETE") }),
									new MvcRouteHandler()));
		}

		protected void MapPostOverrideForPutAndDelete()
		{
			// HTML forms can only GET or POST... this allows us to place an override value in the form to simulate a PUT or DELETE
			// The route handler then translates the overrides to the appropriate action
			// POST /blogs/1 => Update or Delete
			_routeCollection.Add(new Route(
									_resourcePath + _idSegment,
									new RouteValueDictionary(new { controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new PostOverrideRouteHandler()));
		}

		protected void MapMemberRoutes()
		{
			foreach (var member in _configuration.MemberRoutes.Keys)
			{
				var verbArray = _configuration.GetMemberVerbArray(member);
				// VERB /blogs/1/member => Member
				_routeCollection.Add(new Route(
										_resourcePath + _idSegment + "/" + member,
										new RouteValueDictionary(new { action = member, controller = _controller }),
										new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(verbArray) }),
										new MvcRouteHandler()));
			}
		}

		protected void MapCollectionRoutes()
		{
			foreach (var member in _configuration.CollectionRoutes.Keys)
			{
				var verbArray = _configuration.CollectionRoutes[member].Select(x => x.ToString().ToUpperInvariant()).ToList().ToArray();
				// VERB /blogs/member => Member
				_routeCollection.Add(new Route(
										_resourcePath + "/" + member,
										new RouteValueDictionary(new { action = member, controller = _controller }),
										new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint(verbArray) }),
										new MvcRouteHandler()));
			}
		}

		public abstract void Map(string resource);

		public void Map()
		{
			Map(ResourceName());
		}


		public void Map(Action<IRestfulRouteMapper> map)
		{
			Map(ResourceName(), map);
		}

		public void Map(string resource, Action<IRestfulRouteMapper> map)
		{
			Map(resource);

			var singular = Inflector.Net.Inflector.Singularize(resource).ToLowerInvariant();

			var configuration = _configuration.Clone();

			if (configuration.Shallow)
				configuration.PathPrefix = resource + "/{" + singular + "Id}";
			else
				configuration.PathPrefix = BasePath() + resource + "/{" + singular + "Id}";

			var mapper = new RestfulRouteMapper(_routeCollection, configuration);

			map(mapper);
		}
	}
}