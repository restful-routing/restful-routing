using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
    public class RedirectRoute : NamedRoute, IRedirectMapper
    {
        public RedirectRoute(string url)
            : base(string.Empty, url, new RouteValueDictionary(), new RouteValueDictionary(), new MvcRouteHandler())
        {
            Values = new RouteValueDictionary();
            DataTokens = new RouteValueDictionary();
            IsPermanent = false;
        }

        public bool IsPermanent { get; set; }
        public RouteValueDictionary Values { get; set; }
        public string[] Namespaces { get; set; }

        public IRedirectMapper WithName(string name)
        {
            Name = name;
            return this;
        }

        public IRedirectMapper Default(object values)
        {
            Defaults = new RouteValueDictionary(values);
            return this;
        }

        public IRedirectMapper To(object values)
        {
            // we need a set of values that this route can resolve to
            // if you didn't use the Default, let's just resolve it
            // to the same thing we are redirecting to.
            if (Defaults == null || !Defaults.Any())
                Defaults = new RouteValueDictionary(values);

            DataTokens["new_path"] = new RouteValueDictionary(values);

            return this;
        }

        public IRedirectMapper To<TController>(Expression<Func<TController, object>> func, string area = "")
            where TController : Controller
        {
            var controllerName = GetControllerName<TController>();
            var action = GetActionName(func);
            To(new {controller = controllerName, action = action, area = area });
            return this;
        }

        public IRedirectMapper GetOnly()
        {
            if (Constraints.ContainsKey("httpMethod"))
                Constraints.Remove("httpMethod");

            Constraints.Add("httpMethod", new HttpMethodConstraint("GET"));
            return this;
        }

        public IRedirectMapper AllowMethods(params string[] httpVerbs)
        {
            if (Constraints.ContainsKey("httpMethod"))
                Constraints.Remove("httpMethod");

            Constraints.Add("httpMethod", new HttpMethodConstraint(httpVerbs));
            return this;
        }

        public IRedirectMapper Permanent()
        {
            IsPermanent = true;
            return this;
        }

        public IRedirectMapper NotPermanent()
        {
            IsPermanent = false;
            return this;
        }

        private static string GetActionName<TController>(Expression<Func<TController, object>> actionExpression)
        {
            var body = ((MethodCallExpression)actionExpression.Body);
            var actionName = body.Method.Name;
            return RouteSet.LowercaseDefaults ? actionName.ToLowerInvariant() : actionName;
        }

        private static string GetControllerName<T>()
            where T : Controller
        {
            var controllerName = typeof(T).Name;

            string name = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            return RouteSet.LowercaseDefaults ? name.ToLowerInvariant() : name;
        }

    }

    public interface IRedirectMapper
    {
        IRedirectMapper WithName(string name);
        IRedirectMapper Default(object values);
        IRedirectMapper To(object values);
        IRedirectMapper To<TController>(Expression<Func<TController, object>> func, string area = "")
            where TController : Controller;
        IRedirectMapper GetOnly();
        IRedirectMapper AllowMethods(params string[] httpVerbs);
        IRedirectMapper Permanent();
        IRedirectMapper NotPermanent();
    }
}
