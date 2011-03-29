using RestfulRouting.Mappers;
using RestfulRouting.RouteDebug;
using RestfulRouting.Sample.Controllers;

namespace RestfulRouting.Sample
{
    public class Routes : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.Root<RootController>(x => x.Index());
            map.DebugRoute("routedebug");
            map.Area<BlogsController>("", area => area.Resources<BlogsController>(blogs =>
                                                                                      {
                                                                                          blogs.WithFormatRoutes();
                                                                                          blogs.Member(x => x.Get("test"));
                                                                                          blogs.Resources<PostsController>();
                                                                                      }));

            map.Area<Controllers.Admin.BlogsController>("admin", admin =>
                                                                     {
                                                                         admin.Resources<BlogsController>();
                                                                         admin.Resources<PostsController>();
                                                                     });
        }
    }
}