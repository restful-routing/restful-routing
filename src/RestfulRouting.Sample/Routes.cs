using RestfulRouting.Sample.Controllers;

namespace RestfulRouting.Sample
{
    public class Routes : RouteSet
    {
        public Routes()
        {
            Map("").To<RootController>(x => x.Index());

            Map("routedebug").To<RouteDebugController>(x => x.Index());
            Area<BlogsController>("", () =>
            {
                Resources<BlogsController>(() => {
					Member(x => x.Get("test"));
                    Resources<PostsController>();
                });                
            });


            Area<Controllers.Admin.BlogsController>("admin", () =>
            {
                Resources<Controllers.Admin.BlogsController>();
                Resources<Controllers.Admin.PostsController>();
            });
        }
    }
}