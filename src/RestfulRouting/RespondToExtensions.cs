using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
    public static class RespondToExtensions
    {
        public static JsonResult AllowGet(this JsonResult result)
        {
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public static string Format(this RouteData routeData)
        {
            return routeData.Values.ContainsKey("format") ? routeData.Values["format"].ToString() : string.Empty;
        }
    }
}