using System.Web.Routing;
using RestfulRouting.Documentation.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RestfulRouting.Documentation.Routes), "Start")]

namespace RestfulRouting.Documentation
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.DebugRoute("routedebug");
            map.Root<HomeController>(x => x.Index());
            map.Resource<QuickStartController>(quickstart => quickstart.Only("show"));
            map.Connect<OtherRouteSet>();
        }

        public static void Start()
        {
            var routes = RouteTable.Routes;
            routes.MapRoutes<Routes>();
        }
    }

    public class OtherRouteSet : RouteSet {
        public override void Map(IMapper map) 
        {
            map.Resource<RouteSetController>(routeSet => routeSet.Only("show"));
        }
    }
}