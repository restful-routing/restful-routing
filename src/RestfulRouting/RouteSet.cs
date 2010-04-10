using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Mappings;

namespace RestfulRouting
{
	public abstract class RouteSet
	{
		private Mapping _currentMapping;
		private RouteNames names;
		private IList<Mapping> mappings = new List<Mapping>();
		private string _pathPrefix;
		public static Func<string, string> Singularize = Inflector.Net.Inflector.Singularize;

		protected RouteSet()
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

		public void Collection(string actionName, params HttpVerbs[] verbs)
		{
			_currentMapping.Collections[actionName.ToLowerInvariant()] = verbs;
		}

		public void Constrain(string key, object value)
		{
			_currentMapping.Context.Constraints[key] = value;
		}

		private void GenerateNestedPathPrefix()
		{
			var basePath = VirtualPathUtility.RemoveTrailingSlash(_pathPrefix);

			if (!string.IsNullOrEmpty(basePath))
			{
				basePath = basePath + "/";
			}

			if (_currentMapping != null && _currentMapping.ResourceName != null)
			{
				_pathPrefix = basePath + (_currentMapping.MappedName ?? _currentMapping.ResourceName);
			}
		}

		public void Resources<TController>()
			where TController : Controller
		{
			Resources<TController>(() => { });
		}

		public void Resources<TController>(Action nestedAction)
			where TController : Controller
		{
			var previousPath = _pathPrefix;

			GenerateNestedPathPrefix();

			if (_currentMapping != null && _currentMapping.ResourceName != null && IsNestedResourcesMapping())
			{
				var singular = Singularize(_currentMapping.ResourceName).ToLowerInvariant();
				_pathPrefix += "/{" + singular + "Id}";
			}

			var resourcesMapping = new ResourcesMapping<TController>(names, new ResourcesMapper(names, _pathPrefix));

			MapNested(resourcesMapping, nestedAction);

			_pathPrefix = previousPath;
		}

		private bool IsNestedResourcesMapping()
		{
			return _currentMapping.GetType().Name.StartsWith("Resources");
		}

		public void Resource<TController>()
			where TController : Controller
		{
			Resource<TController>(() => { });
		}


		public void Resource<TController>(Action nestedAction)
			where TController : Controller
		{
			var previousPath = _pathPrefix;

			GenerateNestedPathPrefix();

			var resourcesMapping = new ResourceMapping<TController>(names, new ResourceMapper(names, _pathPrefix));

			MapNested(resourcesMapping, nestedAction);

			_pathPrefix = previousPath;
		}

		private void MapNested(Mapping mapping, Action action)
		{
			AddMapping(mapping);

			var parentMapping = _currentMapping;

			_currentMapping = mapping;

			action();

			_currentMapping = parentMapping;
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

		public void Area<T>(string areaName)
		{
			var areaMapping = new AreaMapping<T>(areaName, "");

			foreach (var mapping in mappings)
			{
				areaMapping.AddSubMapping(mapping);
			}
			mappings = new List<Mapping>{areaMapping};

			_currentMapping = areaMapping;
		}

		public void Area<T>(string area, Action action)
		{
			Area<T>(area, area, action);
		}

		public void Area<T>(string areaName, string pathPrefix, Action action)
		{
			var mapping = new AreaMapping<T>(areaName, pathPrefix);

			MapNested(mapping, action);
		}

		public void Area(string area, Action action)
		{
			var mapping = new AreaMapping(area);

			MapNested(mapping, action);
		}

        public void Connect<T>(string pathPrefix) where T : RouteSet, new()
        {
            AddMapping(new AppMapping<T>(pathPrefix));
        }

		public void Route(RouteBase routeBase)
		{
			AddMapping(new RouteMapping(routeBase));
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