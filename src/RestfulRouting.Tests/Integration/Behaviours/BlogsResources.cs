using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Behaviours
{
    [Behaviors]
    public class BlogsResources
    {
        It should_map_get_index = () => "~/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());

        It should_map_get_show = () => "~/blogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1));

        It should_map_get_new = () => "~/blogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New());

        It should_map_post_create = () => "~/blogs".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create());

        It should_map_get_edit = () => "~/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1));

        It should_map_put_update = () => "~/blogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1));

        It should_map_delete_destroy = () => "~/blogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1));

        
        It should_generate_index = () => OutBoundUrl.Of<BlogsController>(x => x.Index()).ShouldMapToUrl("/blogs");

        It should_generate_show = () => OutBoundUrl.Of<BlogsController>(x => x.Show(1)).ShouldMapToUrl("/blogs/1");

        It should_generate_new = () => OutBoundUrl.Of<BlogsController>(x => x.New()).ShouldMapToUrl("/blogs/new");

        It should_generate_create = () => OutBoundUrl.Of<BlogsController>(x => x.Create()).ShouldMapToUrl("/blogs");

        It should_generate_edit = () => OutBoundUrl.Of<BlogsController>(x => x.Edit(1)).ShouldMapToUrl("/blogs/1/edit");

        It should_generate_update = () => OutBoundUrl.Of<BlogsController>(x => x.Update(1)).ShouldMapToUrl("/blogs/1");

        It should_generate_destroy = () => OutBoundUrl.Of<BlogsController>(x => x.Destroy(1)).ShouldMapToUrl("/blogs/1");
    }

    public static class SpecExtensions
    {
        public static RouteData WithFormat(this RouteData routeData, string format)
        {
            routeData.Values["format"].ShouldEqual(format);
            return routeData;
        }
    }

    [Behaviors]
    public class BlogsResourcesWithFormatRoutes
    {
        It should_map_get_index = () => "~/blogs.json".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index()).WithFormat("json");

        It should_map_get_show = () => "~/blogs/1.json".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1)).WithFormat("json");

        It should_map_get_new = () => "~/blogs/new.json".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New()).WithFormat("json");

        It should_map_post_create = () => "~/blogs.json".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create()).WithFormat("json");

        It should_map_get_edit = () => "~/blogs/1/edit.json".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1)).WithFormat("json");

        It should_map_put_update = () => "~/blogs/1.json".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1)).WithFormat("json");

        It should_map_delete_destroy = () => "~/blogs/1.json".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1)).WithFormat("json");
    }
}