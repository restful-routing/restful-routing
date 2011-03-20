using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Mappings
{
    public abstract class Mapping
    {
        protected Mapping()
        {
            Context = new RouteContext();
        }

        public virtual void AddRoutesTo(RouteCollection routeCollection)
        {
            foreach (var mapping in Mappings)
            {
                mapping.AddRoutesTo(routeCollection);
            }
        }

        public string ControllerName<T>()
        {
            var controllerName = typeof(T).Name;

            return controllerName.Substring(0, controllerName.Length - "Controller".Length).ToLowerInvariant();
        }

        public RouteContext Context { get; set; }

        private IDictionary<string, HttpVerbs[]> _members = new Dictionary<string, HttpVerbs[]>();
        public virtual IDictionary<string, HttpVerbs[]> Members
        {
            get { return _members; }
            private set { _members = value; }
        }

        private IDictionary<string, HttpVerbs[]> _collections = new Dictionary<string, HttpVerbs[]>();
        public virtual IDictionary<string, HttpVerbs[]> Collections
        {
            get { return _collections; }
            private set { _collections = value; }
        }

        public string ResourceName;

        public string MappedName;

        private IList<string> IncludedActions { get; set; }

        public string BasePath()
        {
            var basePath = VirtualPathUtility.RemoveTrailingSlash(Context.PathPrefix);

            var nameToMap = MappedName ?? ResourceName;

            if (!string.IsNullOrEmpty(basePath))
            {
                basePath = basePath + "/" + nameToMap;
            }

            return basePath ?? nameToMap;
        }

        public bool IncludesAction(string action)
        {
            EnsureIncludedActionsIsInitialized();

            return IncludedActions.Contains(action);
        }

        public void As(string resourceName)
        {
            MappedName = resourceName;
        }

        public void Except(params string[] actions)
        {
            EnsureIncludedActionsIsInitialized();

            foreach (var action in actions)
            {
                IncludedActions.Remove(action);
            }
        }

        private void EnsureIncludedActionsIsInitialized()
        {
            var configuration = new RouteNames();
            if (IncludedActions == null)
                IncludedActions = new List<string> { configuration.IndexName, configuration.ShowName, configuration.NewName, configuration.CreateName, configuration.EditName, configuration.UpdateName, configuration.DestroyName };
        }

        public void Only(params string[] actions)
        {
            IncludedActions = new List<string>(actions);
        }

        private IList<Mapping> _mappings = new List<Mapping>();
        public IList<Mapping> Mappings
        {
            get { return _mappings; }
            set { _mappings = value; }
        }

        public void AddSubMapping(Mapping mapping)
        {
            Mappings.Add(mapping);
        }

        protected void ConfigureRoute(Route route)
        {
            if (Context.Constraints != null && Context.Constraints.Count > 0)
            {
                foreach (var constraint in Context.Constraints)
                {
                    route.Constraints.Add(constraint.Key, constraint.Value);
                }
            }
        }
    }
}