using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace RestfulRouting
{
    public class RestfulHttpMethodConstraint : HttpMethodConstraint
    {
        public RestfulHttpMethodConstraint(params string[] allowedMethods)
            : base(allowedMethods)
        {
        }
        protected override bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            switch (routeDirection)
            {
                case RouteDirection.IncomingRequest:
                    foreach (var method in AllowedMethods)
                    {
                        if (String.Equals(method, httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
                            return true;

                        if (httpContext.Request.Form == null)
                            continue;

                        var overridden = httpContext.Request.Form["_method"] ?? httpContext.Request.Form["X-HTTP-Method-Override"];
                        if (String.Equals(method, overridden, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }

                    }
                    break;
            }

            return base.Match(httpContext, route, parameterName, values, routeDirection);
        }

    }
}