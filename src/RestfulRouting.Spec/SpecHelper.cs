using System.Web.Routing;
using Machine.Specifications;

namespace RestfulRouting.Spec
{
    public static class SpecExtensions
    {
        public static RouteData WithFormat(this RouteData routeData, string format)
        {
            routeData.Values["format"].ShouldEqual(format);
            return routeData;
        }

        public static RouteData WithName(this RouteData routeData, string name)
        {
            ((NamedRoute) routeData.Route).Name.ShouldEqual(name);
            return routeData;
        }
    }
}
