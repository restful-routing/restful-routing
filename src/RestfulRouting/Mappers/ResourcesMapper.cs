using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace RestfulRouting.Mappers
{
    public class ResourcesMapper<TController> : ResourcesMapperBase<TController> where TController : Controller
    {
        private Action<ResourcesMapper<TController>> subMapper;
        private Dictionary<string, HttpVerbs[]> collections = new Dictionary<string, HttpVerbs[]>();
        private Dictionary<string, HttpVerbs[]> members = new Dictionary<string, HttpVerbs[]>();

        public ResourcesMapper(Action<ResourcesMapper<TController>> subMapper = null)
        {
            includedActions = new Dictionary<string, Func<Route>>
                                  {
                                      {names.IndexName, () => GenerateRoute(resourcePath, controllerName, names.IndexName, new[] { "GET" })},
                                      {names.CreateName, () => GenerateRoute(resourcePath, controllerName, names.CreateName, new[] { "POST" })},
                                      {names.NewName, () => GenerateRoute(resourcePath + "/" + names.NewName, controllerName, names.NewName, new[] { "GET" })},
                                      {names.EditName, () => GenerateRoute(resourcePath + "/{id}/" + names.EditName, controllerName, names.EditName, new[] { "GET" })},
                                      {names.ShowName, () => GenerateRoute(resourcePath + "/{id}", controllerName, names.ShowName, new[] { "GET" })},
                                      {names.UpdateName, () => GenerateRoute(resourcePath + "/{id}", controllerName, names.UpdateName, new[] { "PUT" })},
                                      {names.DestroyName, () => GenerateRoute(resourcePath + "/{id}", controllerName, names.DestroyName, new[] { "DELETE" })}
                                  };
            this.subMapper = subMapper;
        }

        public void Collection(Action<AdditionalAction> action)
        {
            var additionalAction = new AdditionalAction(collections);
            action(additionalAction);
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

            return GenerateRoute(resourcePath + "/{id}/" + action, controllerName, action, methods.Select(x => x.ToString().ToUpperInvariant()).ToArray());
        }

        private Route CollectionRoute(string action, params HttpVerbs[] methods)
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

            var routes = collections.Select(collection => CollectionRoute(collection.Key, collection.Value)).ToList();

            AddIncludedActions(routes);

            routes.AddRange(members.Select(member => MemberRoute(member.Key, member.Value)));

            if (generateFormatRoutes)
                AddFormatRoutes(routes);

            foreach (var route in routes)
            {
                routeCollection.Add(route);
            }

            if (Mappers.Any())
            {
                var singular = Inflector.Singularize(resourceName);
                BasePath = Join(resourcePath, "{" + singular + "Id}");

                RegisterNested(routeCollection);
            }
        }
    }
}
