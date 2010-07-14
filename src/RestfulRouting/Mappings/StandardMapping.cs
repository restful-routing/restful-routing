using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Mappings
{
    public class StandardMapping : Mapping
    {
        public Route Route;
        private string _pathPrefix;

        public StandardMapping(string pathPrefix)
        {
            _pathPrefix = pathPrefix;
        }

        public override void AddRoutesTo(RouteCollection routeCollection)
        {
            routeCollection.Add(Route);
        }

        public StandardMapping Map(string url)
        {
            var basePath = "";
            if (!string.IsNullOrEmpty(_pathPrefix))
                basePath = _pathPrefix + "/";

            Route = new Route(basePath + url,
                new RouteValueDictionary(),
                new RouteValueDictionary(new { httpMethod = new HttpMethodConstraint("GET") }),
                new RouteValueDictionary(),
                new MvcRouteHandler());

            return this;
        }

        private static string GetActionName<TController>(Expression<Func<TController, object>> actionExpression)
        {
            var body = ((MethodCallExpression)actionExpression.Body);
            var actionName = body.Method.Name.ToLowerInvariant();
            return actionName;
        }

        public StandardMapping To<T>(Expression<Func<T, object>> func)
        {
            Route.Defaults["controller"] = ControllerName<T>();

            Route.Defaults["action"] = GetActionName(func);

            return this;
        }

        public StandardMapping Constrain(string name, object constraint)
        {
            Route.Constraints[name] = constraint;

            return this;
        }

        public StandardMapping Default(string name, object constraint)
        {
            Route.Defaults[name] = constraint;

            return this;
        }

        public StandardMapping GetOnly()
        {
            Route.Constraints["httpMethod"] = new HttpMethodConstraint("GET");

            return this;
        }

		public StandardMapping Allow(params HttpVerbs[] methods)
		{
			Route.Constraints["httpMethod"] = new HttpMethodConstraint(methods.Select(x => x.ToString().ToUpperInvariant()).ToArray());

			return this;
		}
    }
}