using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Mappers
{
    public class ResourceMapper<TController> : ResourcesMapperBase<TController> where TController : Controller
    {
        protected Action<ResourceMapper<TController>> subMapper;
        private Dictionary<string, HttpVerbs[]> members = new Dictionary<string, HttpVerbs[]>();

        public ResourceMapper(Action<ResourceMapper<TController>> subMapper = null)
        {
            As(_singularResourceName);
            includedActions = new Dictionary<string, Func<Route>>
                                  {
                                      {names.ShowName, () => GenerateNamedRoute(JoinResources(resourceName), resourcePath, controllerName, names.ShowName, new[] { "GET" })},
                                      {names.UpdateName, () => GenerateRoute(resourcePath, controllerName, names.UpdateName, new[] { "PUT" })},
                                      {names.NewName, () => GenerateNamedRoute("new_" + JoinResources(resourceName), resourcePath + "/" + names.NewName, controllerName, names.NewName, new[] { "GET" })},
                                      {names.EditName, () => GenerateNamedRoute("edit_" + JoinResources(resourceName), resourcePath + "/" + names.EditName, controllerName, names.EditName, new[] { "GET" })},
                                      {names.DestroyName, () => GenerateRoute(resourcePath, controllerName, names.DestroyName, new[] { "DELETE" })},
                                      {names.CreateName, () => GenerateRoute(resourcePath, controllerName, names.CreateName, new[] { "POST" })}
                                  };
            this.subMapper = subMapper;
        }

        public void Member(Action<AdditionalAction> action)
        {
            var additionalAction = new AdditionalAction(members);
            action(additionalAction);
        }

        private Route MemberRoute(string action, params HttpVerbs[] methods)
        {
            if (methods.Length == 0)
                methods = new[] { HttpVerbs.Get };

            return GenerateRoute(resourcePath + "/" + action, controllerName, action, methods.Select(x => x.ToString().ToUpperInvariant()).ToArray());
        }

        public override void RegisterRoutes(RouteCollection routeCollection)
        {
            if (subMapper != null)
            {
                subMapper.Invoke(this);
            }

            var routes = new List<Route>();

            AddIncludedActions(routes);

            routes.AddRange(members.Select(member => MemberRoute(member.Key, member.Value)));

            if (generateFormatRoutes)
                AddFormatRoutes(routes);

            foreach (var route in routes)
            {
                ConfigureRoute(route);
                routeCollection.Add(route);
            }

            if (Mappers.Any())
            {
                BasePath = resourcePath;

                resourcePaths.Add(_singularResourceName);
                RegisterNested(routeCollection, mapper => mapper.SetParentResources(resourcePaths));
            }
        }
    }
}
