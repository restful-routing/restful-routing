using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappings;
using RestfulRouting.Tests.Integration.Contexts;
using Rhino.Mocks;
using System.Linq;

namespace RestfulRouting.Tests.Unit.Mappings
{
    [Subject(typeof(AppMapping<>))]
    public class when_mapping_an_app : base_context
    {
        static AppMapping<ForumsApp> mapping;

        public class ForumsApp : RouteSet
        {
            public ForumsApp()
            {
                Resources<PostsController>();
            }
        }

        static RouteCollection routes = new RouteCollection();

        Establish context = () => mapping = new AppMapping<ForumsApp>("forums/");

        Because of = () => mapping.AddRoutesTo(routes);

        It should_map_posts = () => routes.Count.ShouldBeGreaterThan(1);

        It should_map_the_app_with_the_forums_prefix = () =>
                                                           {
                                                               foreach (var route in routes.Select(x => (Route)x))
                                                               {
                                                                   route.Url.ShouldStartWith("forums/");
                                                               }
                                                           };
    }

    [Subject(typeof(AppMapping<>))]
    public class when_mapping_an_app_with_no_trailing_slash : base_context
    {
        static AppMapping<ForumsApp> mapping;

        public class ForumsApp : RouteSet
        {
            public ForumsApp()
            {
                Resources<PostsController>();
            }
        }

        static RouteCollection routes = new RouteCollection();

        Establish context = () => mapping = new AppMapping<ForumsApp>("forums");

        Because of = () => mapping.AddRoutesTo(routes);
    }
}