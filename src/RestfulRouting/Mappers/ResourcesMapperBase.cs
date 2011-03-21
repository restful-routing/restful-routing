using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Mappers
{
    public class ResourcesMapperBase<TController> : Mapper where TController : Controller
    {
        protected string resourceName;
        protected string resourcePath;
        protected string controllerName;
        protected RouteNames names = new RouteNames();
        protected Dictionary<string, Func<Route>> includedActions;
        protected bool generateFormatRoutes;

        public ResourcesMapperBase()
        {
            controllerName = ControllerName<TController>();
            As(Inflector.Pluralize(controllerName));
        }

        public void As(string resourceName)
        {
            this.resourceName = resourceName;
            CalculatePath();
        }

        public void Except(params string[] actions)
        {
            foreach (var action in actions)
            {
                includedActions.Remove(action);
            }
        }

        public void Only(params string[] actions)
        {
            var onlyIncludedActions = new Dictionary<string, Func<Route>>();
            foreach (var action in actions)
            {
                onlyIncludedActions.Add(action, includedActions[action]);
            }
            includedActions = onlyIncludedActions;
        }

        public void WithFormatRoutes()
        {
            generateFormatRoutes = true;
        }

        public void PathNames(Action<RouteNames> action)
        {
            action(names);
        }

        private void CalculatePath()
        {
            resourcePath = Join(BasePath, resourceName);
        }

        protected override void SetBasePath(string basePath)
        {
            base.SetBasePath(basePath);
            CalculatePath();
        }

        protected Route GenerateRoute(string path, string controller, string action, string[] httpMethods)
        {
            return new Route(path,
                             new RouteValueDictionary(new { controller, action }),
                             new RouteValueDictionary(new { httpMethod = new RestfulHttpMethodConstraint(httpMethods) }),
                             RouteHandler);
        }

        protected void AddIncludedActions(List<Route> routes)
        {
            routes.AddRange(includedActions.Select(x => x.Value.Invoke()).ToArray());
        }

        protected void AddFormatRoutes(List<Route> routes)
        {
            var formatRoutes = routes.Select(WithFormatExtension).ToList();
            routes.InsertRange(0, formatRoutes);
        }

        protected Route WithFormatExtension(Route implicitRoute)
        {
            var explicitRoute = new Route(implicitRoute.Url + ".{format}", implicitRoute.RouteHandler)
                                    {
                                        Constraints = new RouteValueDictionary(implicitRoute.Constraints ?? new RouteValueDictionary()),
                                        DataTokens = new RouteValueDictionary(implicitRoute.DataTokens ?? new RouteValueDictionary()),
                                        Defaults = new RouteValueDictionary(implicitRoute.Defaults ?? new RouteValueDictionary())
                                    };

            explicitRoute.Constraints.Add("format", @"[A-Za-z0-9]+");

            return explicitRoute;
        }

        public void Constrain(string key, object value)
        {
            Constraints[key] = value;
        }
    }
}