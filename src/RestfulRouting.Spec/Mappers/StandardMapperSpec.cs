using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Mappers;
using RestfulRouting.Spec.TestObjects;

namespace RestfulRouting.Spec.Mappers
{
    public class standard_mapping : base_context
    {
        Because of = () => new StandardMapper().Map("posts/{year}/{slug}").To<PostsController>(x => x.Post(2009, "")).RegisterRoutes(routes);

        It should_map_posts_post = () => "~/posts/2009/test".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Post(2009, "test"));

        It should_generate_url = () => OutBoundUrl.Of<PostsController>(x => x.Post(2009, "test")).ShouldMapToUrl("/posts/2009/test");
    }

    public class standard_mapping_respects_base_path : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(map => map.Map("hi").To<CommentsController>(x => x.Index(2))).RegisterRoutes(routes);

        // this defaults to mapping under the member now

        It should_map_posts_post = () => "~/posts/1/hi".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Index(1));

    }
}
