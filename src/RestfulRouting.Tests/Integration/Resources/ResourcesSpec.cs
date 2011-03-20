using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Tests.Integration.Behaviours;
using RestfulRouting.Tests.Integration.Contexts;
using MvcContrib.TestHelper;

namespace RestfulRouting.Tests.Integration
{
    public class when_mapping_a_blogs_resource : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>();
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;
    }

    public class when_mapping_a_blogs_resources_with_format_routes : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                WithFormatRoutes();
                Resources<BlogsController>(() =>
                                               {
                                                   Member(x => x.Post("up"));
                                                   Collection(x => x.Get("latest"));
                                               });
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resoure;

        Behaves_like<BlogsResourcesWithFormatRoutes> a_blogs_resource_with_format_routes;

        It maps_members_with_format = () => "~/blogs/1/up.json".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Up(1)).WithFormat("json");

        It maps_collections_with_format = () => "~/blogs/latest.json".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Latest()).WithFormat("json");
    }

    public class when_mapping_a_blogs_resource_with_a_nested_posts_resource : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(Resources<PostsController>);
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsNestedUnderBlogs> a_nested_posts_resource;
    }

    public class when_mapping_a_blogs_resource_with_a_nested_post_resource_with_format : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                WithFormatRoutes();
                Resources<BlogsController>(Resources<PostsController>);
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsNestedUnderBlogs> a_nested_posts_resource;

        Behaves_like<BlogsResourcesWithFormatRoutes> a_blogs_resource_with_format_routes;

        Behaves_like<PostsNestedUnderBlogsFormat> a_nested_posts_resource_with_format_routes;
    }

    public class when_mapping_a_comments_resource_under_posts_and_blogs : base_context
    {

        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(() =>
                {
                    Resources<PostsController>(() =>
                    {
                        Resources<CommentsController>();
                    });
                });
            }
        }

        private Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsNestedUnderBlogs> a_nested_posts_resource;

        Behaves_like<CommentsNestedUnderPostsAndBlogs> a_nested_comments_resource;
    }

    public class when_mapping_a_posts_resource_and_a_blogs_resource : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>();
                Resources<PostsController>();
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsResources> a_posts_resource;
    }

    public class when_mapping_and_configuring : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(() =>
                {
                    Resources<PostsController>(() =>
                    {
                        Resources<CommentsController>(() => Only("new", "create"));
                    });
                });
                Resources<ImagesController>();
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsNestedUnderBlogs> a_nested_posts_resource;

        It should_map_comments_new = () => "~/blogs/1/posts/2/comments/new".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.New(1, 2));

        It should_map_comments_create = () => "~/blogs/1/posts/2/comments".WithMethod(HttpVerbs.Post).ShouldMapTo<CommentsController>(x => x.Create(1, 2));

        It should_not_map_comments_index = () => "~/blogs/1/posts/2/comments".WithMethod(HttpVerbs.Get).ShouldBeNull();

        Behaves_like<ImagesResources> an_images_resource;
    }

    public class when_mapping_nested_multiple : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(() =>
                {
                    Resources<PostsController>();
                    Resources<CommentsController>();
                });

            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        Behaves_like<BlogsResources> a_blogs_resource;

        Behaves_like<PostsNestedUnderBlogs> a_nested_posts_resource;

        It should_map_comments_index = () => "~/blogs/1/comments".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Index(1, null));
    }

    public class when_mapping_controller_with_non_singularizable_name : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogAdminController>(() =>
                {
                    Resources<BlogsController>();
                });
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        It should_map_blog_admin_index = () => "~/blogadmins/1/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());
    }

    public class when_mapping_nested_resources : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(() =>
                {
                    Constrain("id", @"\d+");
                    Resources<PostsController>();
                    Map("{year}/{slug}").To<PostsController>(x => x.Post(1, ""));
                });
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        It should_inherit_constraints = () =>
        {
            foreach (var route in routes.Select(x => (Route)x).Where(x => (string)x.Defaults["controller"] == "posts"))
            {
                var constraint = route.Constraints.First(x => x.Key == "blogId");
                constraint.Key.ShouldBe("blogId");
                constraint.Value.ShouldBe(@"\d+");
            }
        };
    }

    public class when_mapping_nested_singular_resource : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(() =>
                {
                });
            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        It should_set_the_correct_base_path = () =>
        {
            foreach (var route in routes.Select(x => (Route)x).Where(x => (string)x.Defaults["controller"] == "sessions"))
            {
                route.Url.ShouldStartWith("blogs/{blogId}/session");
            }
        };
    }
}