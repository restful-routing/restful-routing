using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Mappers;
using RestfulRouting.Spec.TestObjects;

namespace RestfulRouting.Spec.Mappers
{
    [Behaviors]
    public class PostsResource
    {
        It should_map_get_index = () => "~/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index()).ShouldBeNamed("posts");

        It should_map_get_show = () => "~/posts/1".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1)).ShouldBeNamed("post");

        It should_map_get_new = () => "~/posts/new".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New()).ShouldBeNamed("new_post");

        It should_map_post_create = () => "~/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create());

        It should_map_get_edit = () => "~/posts/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(1)).ShouldBeNamed("edit_post");

        It should_map_put_update = () => "~/posts/1".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(1));

        It should_map_delete_destroy = () => "~/posts/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(1));

        It should_generate_index = () => OutBoundUrl.Of<PostsController>(x => x.Index()).ShouldMapToUrl("/posts");

        It should_generate_show = () => OutBoundUrl.Of<PostsController>(x => x.Show(1)).ShouldMapToUrl("/posts/1");

        It should_generate_new = () => OutBoundUrl.Of<PostsController>(x => x.New()).ShouldMapToUrl("/posts/new");

        It should_generate_create = () => OutBoundUrl.Of<PostsController>(x => x.Create()).ShouldMapToUrl("/posts");

        It should_generate_edit = () => OutBoundUrl.Of<PostsController>(x => x.Edit(1)).ShouldMapToUrl("/posts/1/edit");

        It should_generate_update = () => OutBoundUrl.Of<PostsController>(x => x.Update(1)).ShouldMapToUrl("/posts/1");

        It should_generate_destroy = () => OutBoundUrl.Of<PostsController>(x => x.Destroy(1)).ShouldMapToUrl("/posts/1");
    }

    public class mapping_resources : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>().RegisterRoutes(routes);

        Behaves_like<PostsResource> a_posts_resource;

        It maps_correct_number_of_routes = () => routes.Count.ShouldEqual(7);
    }

    public class mapping_resources_with_as : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.As("blogs")).RegisterRoutes(routes);

        It should_map_the_resource_as_blogs = () =>
                                                  {
                                                      foreach (var routeBase in routes)
                                                      {
                                                          var route = routeBase as Route;
                                                          route.Url.ShouldStartWith("blogs");
                                                      }
                                                  };
    }

    public class mapping_resources_with_collection_route : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.Collection(x => x.Get("latest"))).RegisterRoutes(routes);

        It should_map_get_latest = () => "~/posts/latest".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Latest());
    }

    public class mapping_resources_with_member_route : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.Member(x => x.Get("hello"))).RegisterRoutes(routes);

        It should_map_get_hello = () => "~/posts/1/hello".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Hello(1));
    }

    public class mapping_resources_with_except : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.Except("index", "update")).RegisterRoutes(routes);

        It maps_correct_number_of_routes = () => routes.Count.ShouldEqual(5);

        It should_map_get_index = () => "~/posts".WithMethod(HttpVerbs.Get).ShouldBeNull();

        It should_map_get_show = () => "~/posts/1".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1));

        It should_map_get_new = () => "~/posts/new".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New());

        It should_map_post_create = () => "~/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create());

        It should_map_get_edit = () => "~/posts/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(1));

        It should_map_put_update = () => "~/posts/1".WithMethod(HttpVerbs.Put).ShouldBeNull();
    }

    public class mapping_resources_with_only : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.Only("index", "update")).RegisterRoutes(routes);

        It should_map_get_index = () => "~/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index());

        It should_map_get_show = () => "~/posts/1".WithMethod(HttpVerbs.Get).ShouldBeNull();

        It should_map_get_new = () => "~/posts/new".WithMethod(HttpVerbs.Get).ShouldBeNull();

        It should_map_post_create = () => "~/posts".WithMethod(HttpVerbs.Post).ShouldBeNull();

        It should_map_get_edit = () => "~/posts/1/edit".WithMethod(HttpVerbs.Get).ShouldBeNull();

        It should_map_put_update = () => "~/posts/1".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(1));

        It should_map_delete_destroy = () => "~/posts/1".WithMethod(HttpVerbs.Delete).ShouldBeNull();

    }

    [Behaviors]
    public class NestedCommentsResource
    {
        It should_map_posts_get_index = () => "~/posts/1/comments".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Index(1)).ShouldBeNamed("post_comments");

        It should_map_posts_get_show = () => "~/posts/1/comments/2".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Show(1, 2)).ShouldBeNamed("post_comment");

        It should_map_posts_get_new = () => "~/posts/1/comments/new".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.New(1)).ShouldBeNamed("new_post_comment");

        It should_map_posts_post_create = () => "~/posts/1/comments".WithMethod(HttpVerbs.Post).ShouldMapTo<CommentsController>(x => x.Create(1));

        It should_map_posts_get_edit = () => "~/posts/1/comments/2/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Edit(1, 2)).ShouldBeNamed("edit_post_comment");

        It should_map_posts_put_update = () => "~/posts/1/comments/2".WithMethod(HttpVerbs.Put).ShouldMapTo<CommentsController>(x => x.Update(1, 2));

        It should_map_posts_delete_destroy = () => "~/posts/1/comments/2".WithMethod(HttpVerbs.Delete).ShouldMapTo<CommentsController>(x => x.Destroy(1, 2));


        It should_generate_index = () => OutBoundUrl.Of<CommentsController>(x => x.Index(2)).ShouldMapToUrl("/posts/2/comments");

        It should_generate_show = () => OutBoundUrl.Of<CommentsController>(x => x.Show(2, 3)).ShouldMapToUrl("/posts/2/comments/3");

        It should_generate_new = () => OutBoundUrl.Of<CommentsController>(x => x.New(2)).ShouldMapToUrl("/posts/2/comments/new");

        It should_generate_create = () => OutBoundUrl.Of<CommentsController>(x => x.Create(2)).ShouldMapToUrl("/posts/2/comments");

        It should_generate_edit = () => OutBoundUrl.Of<CommentsController>(x => x.Edit(2, 3)).ShouldMapToUrl("/posts/2/comments/3/edit");

        It should_generate_update = () => OutBoundUrl.Of<CommentsController>(x => x.Update(2, 3)).ShouldMapToUrl("/posts/2/comments/3");

        It should_generate_destroy = () => OutBoundUrl.Of<CommentsController>(x => x.Destroy(2, 3)).ShouldMapToUrl("/posts/2/comments/3");
    }

    public class nested_resources : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.Resources<CommentsController>()).RegisterRoutes(routes);

        Behaves_like<PostsResource> a_posts_resource;

        Behaves_like<NestedCommentsResource> a_nested_comments_resource;
    }

    public class deep_nested_resources : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.Resources<CommentsController>(comments => comments.Resources<LikesController>())).RegisterRoutes(routes);

        Behaves_like<PostsResource> a_posts_resource;

        Behaves_like<NestedCommentsResource> a_nested_comments_resource;

        It should_map_posts_get_index = () => "~/posts/1/comments/2/likes".WithMethod(HttpVerbs.Get).ShouldMapTo<LikesController>(x => x.Index(1, 2)).ShouldBeNamed("post_comment_likes");

        It should_map_posts_get_show = () => "~/posts/1/comments/2/likes/3".WithMethod(HttpVerbs.Get).ShouldMapTo<LikesController>(x => x.Show(1, 2, 3)).ShouldBeNamed("post_comment_like");

        It should_map_posts_get_new = () => "~/posts/1/comments/2/likes/new".WithMethod(HttpVerbs.Get).ShouldMapTo<LikesController>(x => x.New(1, 2)).ShouldBeNamed("new_post_comment_like");

        It should_map_posts_post_create = () => "~/posts/1/comments/2/likes".WithMethod(HttpVerbs.Post).ShouldMapTo<LikesController>(x => x.Create(1, 2));

        It should_map_posts_get_edit = () => "~/posts/1/comments/2/likes/3/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<LikesController>(x => x.Edit(1, 2, 3)).ShouldBeNamed("edit_post_comment_like");

        It should_map_posts_put_update = () => "~/posts/1/comments/2/likes/3".WithMethod(HttpVerbs.Put).ShouldMapTo<LikesController>(x => x.Update(1, 2, 3));

        It should_map_posts_delete_destroy = () => "~/posts/1/comments/2/likes/3".WithMethod(HttpVerbs.Delete).ShouldMapTo<LikesController>(x => x.Destroy(1, 2, 3));


        It should_generate_index = () => OutBoundUrl.Of<LikesController>(x => x.Index(2, 2)).ShouldMapToUrl("/posts/2/comments/2/likes");

        It should_generate_show = () => OutBoundUrl.Of<LikesController>(x => x.Show(2, 2, 3)).ShouldMapToUrl("/posts/2/comments/2/likes/3");

        It should_generate_new = () => OutBoundUrl.Of<LikesController>(x => x.New(2, 2)).ShouldMapToUrl("/posts/2/comments/2/likes/new");

        It should_generate_create = () => OutBoundUrl.Of<LikesController>(x => x.Create(2, 2)).ShouldMapToUrl("/posts/2/comments/2/likes");

        It should_generate_edit = () => OutBoundUrl.Of<LikesController>(x => x.Edit(2, 2, 3)).ShouldMapToUrl("/posts/2/comments/2/likes/3/edit");

        It should_generate_update = () => OutBoundUrl.Of<LikesController>(x => x.Update(2, 2, 3)).ShouldMapToUrl("/posts/2/comments/2/likes/3");

        It should_generate_destroy = () => OutBoundUrl.Of<LikesController>(x => x.Destroy(2, 2, 3)).ShouldMapToUrl("/posts/2/comments/2/likes/3");
    }

    [Behaviors]
    public class FormatPostsResource
    {
        It should_map_get_index = () => "~/posts.json".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index()).WithFormat("json").ShouldBeNamed("formatted_posts");

        It should_map_get_show = () => "~/posts/1.json".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1)).WithFormat("json").ShouldBeNamed("formatted_post");

        It should_map_get_new = () => "~/posts/new.json".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New()).WithFormat("json").ShouldBeNamed("formatted_new_post");

        It should_map_post_create = () => "~/posts.json".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create()).WithFormat("json");

        It should_map_get_edit = () => "~/posts/1/edit.json".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(1)).WithFormat("json").ShouldBeNamed("formatted_edit_post");

        It should_map_put_update = () => "~/posts/1.json".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(1)).WithFormat("json");

        It should_map_delete_destroy = () => "~/posts/1.json".WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(1)).WithFormat("json");
    }

    public class mapping_resources_with_format_routes : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts => posts.WithFormatRoutes()).RegisterRoutes(routes);

        Behaves_like<FormatPostsResource> a_format_posts_resource;

        It generates_double_the_routes = () => routes.Count.ShouldEqual(14);

        It sets_the_first_7_to_format = () => routes.Take(7).ShouldEachConformTo(x => ((Route)x).Url.EndsWith(".{format}"));

        It sets_the_last_7_to_normal = () => routes.Skip(7).Take(7).ShouldEachConformTo(x => !((Route)x).Url.EndsWith("test"));
    }

    public class mapping_nested_resources_with_constraints : base_context
    {
        Because of = () => new ResourcesMapper<PostsController>(posts =>
                                                                    {
                                                                        posts.Constrain("id", @"\d+");
                                                                        posts.Resources<CommentsController>(comments =>
                                                                                                                {
                                                                                                                    comments.Resources<LikesController>(x => x.Member(m => m.Get("test")));
                                                                                                                    comments.Resource<SessionsController>();
                                                                                                                });
                                                                    }).RegisterRoutes(routes);

        static Func<string, List<Route>> RoutesForController = controller => routes.Select(x => (Route)x).Where(x => x.Defaults["controller"].ToString() == controller).ToList();

        It adds_the_constraint = () =>
                            {
                                var postRoutes = routes.Select(x => (Route)x).Where(x => x.Defaults["controller"].ToString() == "posts").ToList();
                                postRoutes.Count.ShouldBeGreaterThan(1);
                                foreach (var route in postRoutes)
                                {
                                    route.Constraints["id"].ShouldEqual(@"\d+");
                                }
                            };

        It constrains_the_id = () => "~/posts/1".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1));

        It adds_it_for_the_comments = () =>
                                          {
                                              var commentRoutes = RoutesForController("comments");
                                              commentRoutes.Count.ShouldBeGreaterThan(1);
                                              foreach (var route in commentRoutes)
                                              {
                                                  route.Constraints["postId"].ShouldEqual(@"\d+");
                                              }
                                          };

        It adds_it_for_likes = () =>
                                   {
                                       var likesRoutes = RoutesForController("likes");
                                       likesRoutes.Count.ShouldBeGreaterThan(1);
                                       foreach (var route in likesRoutes)
                                       {
                                           route.Constraints["postId"].ShouldEqual(@"\d+");
                                       }
                                   };

        It adds_it_for_sessions = () =>
                                   {
                                       var sessionRoutes = RoutesForController("sessions");
                                       sessionRoutes.Count.ShouldBeGreaterThan(1);
                                       foreach (var route in sessionRoutes)
                                       {
                                           route.Constraints["postId"].ShouldEqual(@"\d+");
                                       }
                                   };
    }
}
