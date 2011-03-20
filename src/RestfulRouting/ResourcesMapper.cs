using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
    public class ResourcesMapper : Mapper
    {
        private RouteNames _names;
        private string _pathPrefix;
        public string ResourceName;
        private string _resourcePath;

        public ResourcesMapper(RouteNames names, string pathPrefix, IRouteHandler routeHandler)
            : base(routeHandler)
        {
            _pathPrefix = pathPrefix;
            _names = names;
        }

        public void SetResourceAs(string name)
        {
            if (!string.IsNullOrEmpty(_pathPrefix))
                _resourcePath = _pathPrefix + "/" + name;
            else
                _resourcePath = name;
            if (ResourceName == null)
                ResourceName = name;
        }

        public Route IndexRoute()
        {
            // GET /blogs => Index
            return GenerateRoute(_resourcePath, ResourceName, _names.IndexName, new[] { "GET" });
        }

        public Route ShowRoute()
        {
            // GET /blogs/1 => Show
            return GenerateRoute(_resourcePath + "/{id}", ResourceName, _names.ShowName, new[] { "GET" });
        }

        public Route CreateRoute()
        {
            // POST /blogs => Create
            return GenerateRoute(_resourcePath, ResourceName, _names.CreateName, new[] { "POST" });
        }

        public Route UpdateRoute()
        {
            // PUT /blogs/1 => Update
            return GenerateRoute(_resourcePath + "/{id}", ResourceName, _names.UpdateName, new[] { "PUT" });
        }

        public Route DestroyRoute()
        {
            // DELETE /blogs/1 => Delete
            return GenerateRoute(_resourcePath + "/{id}", ResourceName, _names.DestroyName, new[] { "DELETE" });
        }

        public Route NewRoute()
        {
            // GET /blogs/new => New
            return GenerateRoute(_resourcePath + "/" + _names.NewName, ResourceName, _names.NewName, new[] { "GET" });
        }

        public Route EditRoute()
        {
            // GET /blogs/1/edit => Edit
            return GenerateRoute(_resourcePath + "/{id}/" + _names.EditName, ResourceName, _names.EditName, new[] { "GET" });
        }

        public Route MemberRoute(string action, params HttpVerbs[] methods)
        {
            if (methods.Length == 0)
                methods = new []{HttpVerbs.Get};

            return GenerateRoute(_resourcePath + "/{id}/" + action, ResourceName, action, methods.Select(x => x.ToString().ToUpperInvariant()).ToArray());
        }

        public Route CollectionRoute(string action, params HttpVerbs[] methods)
        {
            if (methods.Length == 0)
                methods = new[] { HttpVerbs.Get };

            return GenerateRoute(_resourcePath + "/" + action, ResourceName, action, methods.Select(x => x.ToString().ToUpperInvariant()).ToArray());
        }
    }
}