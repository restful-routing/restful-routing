using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Helpers;
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

                        // fixes issues #62 and #63
                        NameValueCollection form;
                        try {
                            // first try to get the unvalidated form first
                            form = httpContext.Request.Unvalidated().Form;
                        }
                        catch (Exception e) {
                            form = httpContext.Request.Form;
                        }

                        if (form == null)
                            continue;

                        var overridden = form["_method"] ?? form["X-HTTP-Method-Override"];
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