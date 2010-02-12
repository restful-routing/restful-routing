using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;

namespace RestfulRouting.Tests.Integration.Behaviours
{
    [Behaviors]
    public class AdminBlogsResources
    {
        It should_map_get_index = () => "~/admin/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.Index());

        It should_map_get_show = () => "~/admin/blogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.Show(1));

        It should_map_get_new = () => "~/admin/blogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.New());

        It should_map_post_create = () => "~/admin/blogs".WithMethod(HttpVerbs.Post).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.Create());

        It should_map_get_edit = () => "~/admin/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.Edit(1));

        It should_map_put_update = () => "~/admin/blogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.Update(1));

        It should_map_delete_destroy = () => "~/admin/blogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<Contexts.Admin.BlogsController>(x => x.Destroy(1));


        // OutBoundUrl doesn't work with areas yet.

        //It should_generate_index = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.Index()).ShouldMapToUrl("/admin/blogs");

        //It should_generate_show = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.Show(1)).ShouldMapToUrl("/admin/blogs/1");

        //It should_generate_new = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.New()).ShouldMapToUrl("/admin/blogs/new");

        //It should_generate_create = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.Create()).ShouldMapToUrl("/admin/blogs");

        //It should_generate_edit = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.Edit(1)).ShouldMapToUrl("/admin/blogs/1/edit");

        //It should_generate_update = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.Update(1)).ShouldMapToUrl("/admin/blogs/1");

        //It should_generate_destroy = () => OutBoundUrl.Of<Contexts.Admin.BlogsController>(x => x.Destroy(1)).ShouldMapToUrl("/admin/blogs/1");
    }
}