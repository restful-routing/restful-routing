using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Mappers;
using System.Linq;

namespace RestfulRouting
{
    public class Mapper
    {
        protected List<Mapper> Mappers = new List<Mapper>();
        protected string BasePath;
        protected IRouteHandler RouteHandler = new MvcRouteHandler();
        protected RouteValueDictionary Constraints = new RouteValueDictionary();

        public void Root<TController>(Expression<Func<TController, object>> action)
        {
            AddMapper(new RootMapper<TController>(action));
        }

        public void Route(RouteBase routeBase)
        {
            AddMapper(new RouteMapper(routeBase));
        }

        public void Resources<TController>(Action<ResourcesMapper<TController>> mapper = null) where TController : Controller
        {
            AddMapper(new ResourcesMapper<TController>(mapper));
        }

        public void Resource<TController>(Action<ResourceMapper<TController>> mapper = null) where TController : Controller
        {
            AddMapper(new ResourceMapper<TController>(mapper));
        }

        public void Area<TController>(string name, Action<AreaMapper> mapper = null) where TController : Controller
        {
            AddMapper(new AreaMapper(name, typeof(TController).Namespace, mapper));
        }

        public void Area<TController>(string name, string pathPrefix, Action<AreaMapper> mapper) where TController : Controller
        {
            AddMapper(new AreaMapper(name, typeof(TController).Namespace, mapper));
        }

        public void Area(string name, Action<AreaMapper> mapper)
        {
            AddMapper(new AreaMapper(name, null, mapper));
        }

        public void Area(string name, string pathPrefix, Action<AreaMapper> mapper)
        {
            AddMapper(new AreaMapper(name, null, mapper));
        }

        public virtual StandardMapper Map(string path)
        {
            var mapper = new StandardMapper().Map(path);
            AddMapper(mapper);
            return mapper;
        }

        public virtual void Connect<TRouteSet>(string path = "") where TRouteSet : RouteSet, new()
        {
            AddMapper(new ConnectMapper<TRouteSet>(path));
        }

        private void AddMapper(Mapper mapper)
        {
            Mappers.Add(mapper);
        }

        protected virtual void SetBasePath(string basePath)
        {
            BasePath = basePath;
        }

        public virtual void RegisterRoutes(RouteCollection routeCollection)
        {
            foreach (var mapper in Mappers)
            {
                mapper.SetRouteHandler(RouteHandler);
                mapper.RegisterRoutes(routeCollection);
            }
        }

        protected string Join(params string[] parts)
        {
            var validParts = parts.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return string.Join("/", validParts);
        }

        protected string ControllerName<T>()
        {
            var controllerName = typeof(T).Name;

            return controllerName.Substring(0, controllerName.Length - "Controller".Length).ToLowerInvariant();
        }

        protected void RegisterNested(RouteCollection routeCollection)
        {
            foreach (var mapper in Mappers)
            {
                mapper.SetBasePath(BasePath);
                mapper.SetRouteHandler(RouteHandler);
                mapper.InheritConstraints(Constraints);
                mapper.RegisterRoutes(routeCollection);
            }
        }

        private void InheritConstraints(RouteValueDictionary constraints)
        {
            foreach (var constraint in constraints)
            {
                if (!Constraints.ContainsKey(constraint.Key))
                {
                    Constraints[constraint.Key] = constraint.Value;
                }
            }
        }

        public void SetRouteHandler(IRouteHandler routeHandler)
        {
            RouteHandler = routeHandler;
        }

        protected void ConfigureRoute(Route route)
        {
            if (route.Constraints == null)
                route.Constraints = new RouteValueDictionary();
            foreach (var constraint in Constraints)
            {
                route.Constraints[constraint.Key] = constraint.Value;
            }
        }
    }
}
