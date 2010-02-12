using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Mappings;

namespace RestfulRouting
{
	public abstract class RestfulRoutingArea
	{
		private Mapping _currentMapping;
		private RouteNames names;
		private IList<Mapping> mappings = new List<Mapping>();
		private string _pathPrefix;

		protected RestfulRoutingArea()
		{
			names = new RouteNames();
		}

		public void As(string resourceName)
		{
			_currentMapping.As(resourceName);
		}

		public void Only(params string[] actions)
		{
			_currentMapping.Only(actions);
		}

		public void Except(params string[] actions)
		{
			_currentMapping.Except(actions);
		}

		public void Member(string actionName, params HttpVerbs[] verbs)
		{
			_currentMapping.Members[actionName.ToLowerInvariant()] = verbs;
		}

		public void Constrain(string key, object value)
		{
			_currentMapping.Context.Constraints[key] = value;
		}

		public void Resources<TController>()
			where TController : Controller
		{
			Resources<TController>(() => { });
		}

		public void Resources<TController>(Action nestedAction)
			where TController : Controller
		{
			var controllerName = typeof(TController).Name;

			var resourceName = controllerName.Substring(0, controllerName.Length - "Controller".Length).ToLowerInvariant();

			var resourcesMapping = new ResourcesMapping<TController>(names, new ResourcesMapper(names, _pathPrefix, resourceName));

			AddMapping(resourcesMapping);

			MapNested(resourcesMapping, () => MapWithNestedPathPrefix(nestedAction));
		}

		private void MapNested(Mapping mapping, Action action)
		{
			var parentMapping = _currentMapping;

			_currentMapping = mapping;

			action();

			_currentMapping = parentMapping;
		}

		private void MapWithNestedPathPrefix(Action action)
		{
			var singular = Inflector.Net.Inflector.Singularize(_currentMapping.ResourceName).ToLowerInvariant();
			var beforeNestedPathPrefix = _pathPrefix;

			var basePath = VirtualPathUtility.RemoveTrailingSlash(_pathPrefix);

			if (!string.IsNullOrEmpty(basePath))
			{
				basePath = basePath + "/";
			}

			_pathPrefix = basePath + _currentMapping.ResourceName + "/{" + singular + "Id}";

			action();

			_pathPrefix = beforeNestedPathPrefix;
		}

		public StandardMapping Map(string url)
		{
			var mapping = new StandardMapping(_currentMapping == null ? "" : _currentMapping.BasePath()).Map(url);

			AddMapping(mapping);

			return mapping;
		}

		private void AddMapping(Mapping mapping)
		{
			if (_currentMapping != null)
				_currentMapping.AddSubMapping(mapping);
			else
				mappings.Add(mapping);
		}

		public void Area<T>(string area, Action action)
		{
			var mapping = new AreaMapping<T>(area);

			AddMapping(mapping);

			MapNested(mapping, action);
		}

		public void Area(string area, Action action)
		{
			var mapping = new AreaMapping(area);

			AddMapping(mapping);

			MapNested(mapping, action);
		}

        public void App<T>(string pathPrefix) where T : RestfulRoutingArea, new()
        {
            AddMapping(new AppMapping<T>(pathPrefix));
        }

		public void RegisterRoutes(RouteCollection routeCollection)
		{
			foreach (var mapping in mappings)
			{
				mapping.AddRoutesTo(routeCollection);
			}
		}
	}
}