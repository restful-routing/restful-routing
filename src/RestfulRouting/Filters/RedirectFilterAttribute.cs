using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting.Filters
{
    public class RedirectFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            var redirect = filterContext.RouteData.Route as RedirectRoute;
            if (redirect != null)
            {
                var helper = new UrlHelper(filterContext.RequestContext);
                var values = new RouteValueDictionary(filterContext.RequestContext.RouteData.Values);
                var merged = new RouteValueDictionary(redirect.DataTokens);

                // keep the values we specified, and add the other routeValues
                // that we we didn't have overrides for.
                foreach (var key in values.Keys.Where(key => !merged.ContainsKey(key)))
                    merged.Add(key, filterContext.RouteData.Values[key]);

                var url = helper.RouteUrl(merged);
                filterContext.Result = new RedirectResult(url, redirect.IsPermanent);
            }
        }
    }
}
