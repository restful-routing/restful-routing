using System.Web.Mvc;
using System.Web.Routing;
using RestfulRouting.Documentation.Controllers;
using RestfulRouting.Documentation.Controllers.Mappings;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RestfulRouting.Documentation.Routes), "Start")]

namespace RestfulRouting.Documentation {
    public class Routes : RouteSet {
        public override void Map(IMapper map) {
            // register the route debugger
            map.DebugRoute("routedebug");
            // register the root of the site
            map.Root<HomeController>(x => x.Index());
            map.Resource<QuickStartController>(quickstart => quickstart.Only("show"));

            // Connecting RouteSets: notice that we are connecting another RouteSet to this one
            map.Connect<OtherRouteSet>();

            // Mapping an area: notice, all these controllers are part of the area
            map.Area<AreasController>("mappings", area => {
                area.Resource<ResourceController>();
                area.Resources<ResourcesController>(resources => {
                    // we are nesting a resource inside of another resource
                    resources.Resources<OtherResourcesController>(other => other.Only("index"));
                    // using collection
                    resources.Collection(r => r.Get("many"));
                    // using member
                    resources.Member(r => r.Get("lonely"));
                });
                area.Resource<AreasController>();
                area.Resource<ExtrasController>(extras => {
                    // renaming the url part
                    extras.As("extras");
                    // using member, notice collection is unavailable
                    extras.Member(e => e.Get("member"));
                    // Use path
                    extras.Path("using_path").To<ExtrasController>(e => e.UsingPath()).GetOnly();
                    // Using route
                    extras.Route(new Route("mappings/extras/with_route", new RouteValueDictionary(new { controller = "extras", action = "usingroute", area = "mappings" }), new MvcRouteHandler()));
                    // Format route action, register like always
                    extras.Member(e => e.Get("awesome"));
                    // enable format routes
                    extras.WithFormatRoutes();
                });
            });

            // Not the route debuger, just a controller
            map.Resource<RouteDebuggerController>(debug => debug.Only("show"));


        }

        public static void Start() {
            var routes = RouteTable.Routes;
            RouteTable.Routes.MapHubs();
            routes.MapRoutes<Routes>();
        }
    }

    public class OtherRouteSet : RouteSet {
        public override void Map(IMapper map) {
            map.Resource<RouteSetController>(routeSet => routeSet.Only("show"));
        }
    }
}