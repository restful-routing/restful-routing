using System.Web.Routing;
using RestfulRouting;
using $rootnamespace$.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.Routes), "Start")]

namespace $rootnamespace$
{
    public class Routes : RouteSet
    {
        public Routes()
        {
            /*
             * TODO: Add your routes here.
             * 
            Root<HomeController>(x => x.Index());
            
            Resources<BlogsController>(() =>
            {
                As("weblogs");
                Only("index", "show");
                Collection("latest", HttpVerbs.Get);

                Resources<PostsController>(() =>
                {
                    Except("create", "update", "destroy");
                    Resources<CommentsController>(() => Except("destroy"));
                });
            });

            Area<Controllers.Admin.BlogsController>("admin", () =>
            {
                Resources<Controllers.Admin.BlogsController>();
                Resources<Controllers.Admin.PostsController>();
            });
             */
        }

        public static void Start()
        {
            var routes = RouteTable.Routes;
            routes.MapRoutes<Routes>();
        }
    }
}