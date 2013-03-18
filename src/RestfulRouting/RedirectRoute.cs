using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
    public class RedirectRoute : NamedRoute, IRedirectMapper
    {
        public RedirectRoute(string url)
            : base(Guid.NewGuid().ToString(), url, new RouteValueDictionary(), new RouteValueDictionary(), new MvcRouteHandler())
        {
            Values = new RouteValueDictionary();
            Name = Guid.NewGuid().ToString();
            IsPermanent = false;
        }

        public bool IsPermanent { get; set; }
        public RouteValueDictionary Values { get; set; }

        public IRedirectMapper WithName(string name)
        {
            Name = name;
            return this;
        }

        public IRedirectMapper From(object values)
        {
            Defaults = new RouteValueDictionary(values);
            return this;
        }

        public IRedirectMapper To(object values)
        {
            // we need a set of values that this route can resolve to
            // if you didn't use the From, let's just resolve it
            // to the same thing we are redirecting to.
            if (Defaults == null || !Defaults.Any())
                Defaults = new RouteValueDictionary(values);

            DataTokens = new RouteValueDictionary(values);
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
    }

    public interface IRedirectMapper
    {
        IRedirectMapper WithName(string name);
        IRedirectMapper From(object values);
        IRedirectMapper To(object values);
        IRedirectMapper GetOnly();
        IRedirectMapper AllowMethods(params string[] httpVerbs);
        IRedirectMapper Permanent();
        IRedirectMapper NotPermanent();
    }
}
