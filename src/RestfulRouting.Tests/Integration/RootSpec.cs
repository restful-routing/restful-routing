using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Mappings;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
    [Subject(typeof(StandardMapping))]
    public class when_mapping_the_root : base_context
    {
        public class WebsiteApp : RouteSet
        {
            public WebsiteApp()
            {
                Root<BlogsController>(x => x.Index());
            }
        }

        Because of = () => new WebsiteApp().RegisterRoutes(routes);

        It should_map_root = () => "~/".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());

        It should_generate_url = () => OutBoundUrl.Of<BlogsController>(x => x.Index()).ShouldMapToUrl("/");
    }

    [Subject(typeof(StandardMapping))]
    public class when_mapping_the_area_root : base_context
    {
        public class WebsiteApp : RouteSet
        {
            public WebsiteApp()
            {
                Area("admin", () => Root<BlogsController>(x => x.Index()));
            }
        }

        Because of = () => new WebsiteApp().RegisterRoutes(routes);

        It should_map_area_root = () => "~/admin".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());

        It should_generate_url = () => OutBoundUrl.Of<BlogsController>(x => x.Index()).ShouldMapToUrl("/admin");
    }
}
