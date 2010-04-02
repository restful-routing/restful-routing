using System.Web.Mvc;
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
}