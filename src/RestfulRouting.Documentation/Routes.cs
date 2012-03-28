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