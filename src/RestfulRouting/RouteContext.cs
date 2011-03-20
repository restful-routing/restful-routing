using System.Web.Routing;

namespace RestfulRouting
{
    public class RouteContext
    {
        public RouteContext()
        {
            Constraints = new RouteValueDictionary();
            Defaults = new RouteValueDictionary();
        }

        public RouteValueDictionary Constraints { get; set; }
        public RouteValueDictionary Defaults { get; set; }

        public string PathPrefix;
        public bool GenerateFormatRoutes { get; set; }
    }
}