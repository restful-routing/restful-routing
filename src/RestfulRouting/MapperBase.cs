using System;
using System.Collections.Generic;
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

		private void AddRoute(Route route)
		{
			if (MappingANamespace())
			{
				if (route.DataTokens == null)
					route.DataTokens = new RouteValueDictionary();

				route.Defaults.Add("_area", _configuration.PathPrefix);
				route.DataTokens.Add("namespaces", _configuration.Namespaces);
			}


			if (_configuration.Constraints != null && _configuration.Constraints.Count > 0)
			{
				foreach (var constraint in _configuration.Constraints)
				{
					route.Constraints.Add(constraint.Key, constraint.Value);
				}
			}
			if (!string.IsNullOrEmpty(_configuration.IdValidationRegEx))
			{
				route.Constraints.Add("id", _configuration.IdValidationRegEx);
			}
			_routeCollection.Add(route);
		}

		private bool MappingANamespace()
		{
			return !(_configuration.Namespaces == null || _configuration.Namespaces.Count() == 0);
		}

		protected void MapIndex()
		{
			if (!_configuration.Includes(_configuration.IndexName))
				return;
			// GET /blogs => Index
			var route = new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _configuration.IndexName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler());
			AddRoute(route);
		}

		protected void MapCreate()
		{
			if (!_configuration.Includes(_configuration.CreateName))
				return;
			// POST /blogs => Create
			AddRoute(new Route(
									_resourcePath,
									new RouteValueDictionary(new { action = _configuration.CreateName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("POST") }),
									new MvcRouteHandler()));
		}

		protected void MapNew()
		{
			if (!_configuration.Includes(_configuration.NewName))
				return;
			// GET /blogs/new => New
			AddRoute(new Route(
									_resourcePath + "/" + _configuration.NewName,
									new RouteValueDictionary(new { action = _configuration.NewName, controller = _controller }),
									new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
									new MvcRouteHandler()));
		}

		protected void MapMembers()
		{
			var actions = new[]{_configuration.ShowName, _configuration.NewName, _configuration.EditName, _configuration.DeleteName}
				.Where(action => _configuration.Includes(action));

			if (actions.Count() == 0)
				return;

			var actionsRegEx = string.Join("|", actions.ToArray());

			// GET /blogs/1 => Show
			// GET /blogs/1/edit => Edit
			// GET /blogs/1/delete => Delete
			AddRoute(new Route(
									_resourcePath + _idSegment + "/{action}",
									new RouteValueDictionary(new { action = _configuration.ShowName, controller = _controller }),
									new RouteValueDictionary(new
									                         	{
									                         		httpMethod = new HttpMethodConstraint("GET"), 
																	action = actionsRegEx
									                         	}),
									new MvcRouteHandler()));
		}

		protected void MapUpdate()
		{
			if (!_configuration.Includes(_configuration.UpdateName))
				return;
			// PUT /blogs/1 => Update
			AddRoute(new Route(
									_resourcePath + _idSegment,
									new RouteValueDictionary(new { action = _configuration.UpdateName, controller = _controller }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("PUT")
									}),
									new MvcRouteHandler()));
		}

		protected void MapDestroy()
		{
			if (!_configuration.Includes(_configuration.DestroyName))
				return;
			// DELETE /blogs/1 => Delete
			AddRoute(new Route(
									_resourcePath + _idSegment,
									new RouteValueDictionary(new { action = _configuration.DestroyName, controller = _controller }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("DELETE")
									}),
									new MvcRouteHandler()));
		}

		protected void MapPostOverrideForPutAndDelete()
		{
			// HTML forms can only GET or POST... this allows us to place an override value in the form to simulate a PUT or DELETE
			// The route handler then translates the overrides to the appropriate action
			// POST /blogs/1 => Update or Delete
			AddRoute(new Route(
									_resourcePath + _idSegment,
									new RouteValueDictionary(new { controller = _controller }),
									new RouteValueDictionary(new
									{
										httpMethod = new HttpMethodConstraint("POST")
									}),
									new PostOverrideRouteHandler()));
		}

		protected void MapMemberRoutes()
		{
			foreach (var member in _configuration.MemberRoutes.Keys)
			{
				var verbArray = _configuration.GetMemberVerbArray(member);
				// VERB /blogs/1/member => Member
				AddRoute(new Route(
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
				AddRoute(new Route(
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

			var mapper = new RestfulRouteMapper(_routeCollection, GetChildConfiguration(resource));

			map(mapper);
		}

		private RouteConfiguration GetChildConfiguration(string resource)
		{
			var singular = Inflector.Net.Inflector.Singularize(resource).ToLowerInvariant();

			var childConfiguration = RouteConfiguration.Default();

			childConfiguration.Shallow = _configuration.Shallow;

			if (childConfiguration.Shallow)
				childConfiguration.PathPrefix = resource + "/{" + singular + "Id}";
			else
				childConfiguration.PathPrefix = BasePath() + resource + "/{" + singular + "Id}";

			if (!string.IsNullOrEmpty(_configuration.IdValidationRegEx))
			{
				childConfiguration.Constraints = _configuration.Constraints ?? new RouteValueDictionary();
				childConfiguration.Constraints.Add(singular + "Id", _configuration.IdValidationRegEx);
			}

			childConfiguration.Namespaces = _configuration.Namespaces;

			return childConfiguration;
		}
	}
}