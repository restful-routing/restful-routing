using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Behaviours
{
    [Behaviors]
    public class PostsResources
    {
        It should_map_get_index = () => "~/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index(null));

        It should_map_get_show = () => "~/posts/1".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(null, 1));

        It should_map_get_new = () => "~/posts/new".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New(null));

        It should_map_post_create = () => "~/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create(null));

        It should_map_get_edit = () => "~/posts/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(null, 1));

        It should_map_put_update = () => "~/posts/1".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(null, 1));

        It should_map_delete_destroy = () => "~/posts/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(null, 1));


        It should_generate_index = () => OutBoundUrl.Of<PostsController>(x => x.Index(null)).ShouldMapToUrl("/posts");

        It should_generate_show = () => OutBoundUrl.Of<PostsController>(x => x.Show(null, 1)).ShouldMapToUrl("/posts/1");

        It should_generate_new = () => OutBoundUrl.Of<PostsController>(x => x.New(null)).ShouldMapToUrl("/posts/new");

        It should_generate_create = () => OutBoundUrl.Of<PostsController>(x => x.Create(null)).ShouldMapToUrl("/posts");

        It should_generate_edit = () => OutBoundUrl.Of<PostsController>(x => x.Edit(null, 1)).ShouldMapToUrl("/posts/1/edit");

        It should_generate_update = () => OutBoundUrl.Of<PostsController>(x => x.Update(null, 1)).ShouldMapToUrl("/posts/1");

        It should_generate_destroy = () => OutBoundUrl.Of<PostsController>(x => x.Destroy(null, 1)).ShouldMapToUrl("/posts/1");
    }
}