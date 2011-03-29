using System.Web.Routing;
using RestfulRouting;
using $rootnamespace$.Controllers;

[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.Routes), "Start")]

namespace $rootnamespace$
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.DebugRoute("routedebug");
            /*
             * TODO: Add your routes here.
             * 
            map.Root<HomeController>(x => x.Index());
            
            map.Resources<BlogsController>(blogs =>
            {
                blogs.As("weblogs");
                blogs.Only("index", "show");
                blogs.Collection(x => x.Get("latest"));

                blogs.Resources<PostsController>(posts =>
                {
                    posts.Except("create", "update", "destroy");
                    posts.Resources<CommentsController>(c => c.Except("destroy"));
                });
            });

            map.Area<Controllers.Admin.BlogsController>("admin", admin =>
            {
                admin.Resources<Controllers.Admin.BlogsController>();
                admin.Resources<Controllers.Admin.PostsController>();
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