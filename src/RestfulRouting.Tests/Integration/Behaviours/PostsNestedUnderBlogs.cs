using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Behaviours
{
    [Behaviors]
    public class PostsNestedUnderBlogs
    {
        It should_map_posts_get_index = () => "~/blogs/1/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index(1));

        It should_map_posts_get_show = () => "~/blogs/1/posts/2".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1, 2));

        It should_map_posts_get_new = () => "~/blogs/1/posts/new".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New(1));

        It should_map_posts_post_create = () => "~/blogs/1/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create(1));

        It should_map_posts_get_edit = () => "~/blogs/1/posts/2/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(1, 2));

        It should_map_posts_put_update = () => "~/blogs/1/posts/2".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(1, 2));

        It should_map_posts_delete_destroy = () => "~/blogs/1/posts/2".WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(1, 2));


        It should_generate_index = () => OutBoundUrl.Of<PostsController>(x => x.Index(1)).ShouldMapToUrl("/blogs/1/posts");

        It should_generate_show = () => OutBoundUrl.Of<PostsController>(x => x.Show(1, 2)).ShouldMapToUrl("/blogs/1/posts/2");

        It should_generate_new = () => OutBoundUrl.Of<PostsController>(x => x.New(1)).ShouldMapToUrl("/blogs/1/posts/new");

        It should_generate_create = () => OutBoundUrl.Of<PostsController>(x => x.Create(1)).ShouldMapToUrl("/blogs/1/posts");

        It should_generate_edit = () => OutBoundUrl.Of<PostsController>(x => x.Edit(1, 2)).ShouldMapToUrl("/blogs/1/posts/2/edit");

        It should_generate_update = () => OutBoundUrl.Of<PostsController>(x => x.Update(1, 2)).ShouldMapToUrl("/blogs/1/posts/2");

        It should_generate_destroy = () => OutBoundUrl.Of<PostsController>(x => x.Destroy(1, 2)).ShouldMapToUrl("/blogs/1/posts/2");
    }
}