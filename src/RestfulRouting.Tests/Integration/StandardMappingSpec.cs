using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Mappings;
using RestfulRouting.Tests.Integration.Behaviours;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
	[Subject(typeof(StandardMapping))]
	public class when_mapping_a_standard_route : base_context
	{
		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Map("posts/{year}/{slug}").To<PostsController>(x => x.Post(2009, ""));
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_posts_post = () => "~/posts/2009/test".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Post(2009, "test"));

		It should_generate_url = () => OutBoundUrl.Of<PostsController>(x => x.Post(2009, "test")).ShouldMapToUrl("/posts/2009/test");
	}

	public class when_mapping_a_standard_route_under_a_resource : base_context
	{
		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Resources<PostsController>(() =>
				                           	{
												Map("{year}/{slug}").To<PostsController>(x => x.Post(2009, "")).Constrain("slug", @"\w+").Constrain("year", @"\d+");
				                           	});
				
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_posts_post = () => "~/posts/2009/test".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Post(2009, "test"));

		It should_generate_url = () => OutBoundUrl.Of<PostsController>(x => x.Post(2009, "test")).ShouldMapToUrl("/posts/2009/test");

		Behaves_like<PostsResources> a_posts_resource;
	}

    public class when_mapping_a_standard_route_under_a_nested_resource : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<PostsController>(() =>
                {
                    Resources<CommentsController>(() => 
                    {
                        Map("{year}/{slug}").To<PostsController>(x => x.Post(2009, "")).Constrain("slug", @"\w+").Constrain("year", @"\d+");
                    });
                });

            }
        }

        Because of = () => new BlogArea().RegisterRoutes(routes);

        It should_map_posts_post = () => "~/posts/2/comments/2009/test".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Post(2009, "test"));

        Behaves_like<PostsResources> a_posts_resource;
    }
    
}