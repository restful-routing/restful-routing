using Machine.Specifications;
using RestfulRouting.Tests.Integration.Behaviours;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Areas
{
	public class when_mapping_an_unnamed_area : base_context
	{
		public class BlogArea : RestfulRoutingArea
		{
			public BlogArea()
			{
				Area<BlogsController>("", () =>
				{
				    // Map("images/{id}.jpg").To<ImagesController>(x => x.Show(1));
				    Resources<BlogsController>();
				    Resources<PostsController>();
				});
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		Behaves_like<BlogsResources> a_blogs_resource;

		Behaves_like<PostsResources> a_posts_resource;

		//It prints_routes = () => PrintRoutes(routes);

	}

	public class when_mapping_two_area : base_context
	{
		public class BlogArea : RestfulRoutingArea
		{
			public BlogArea()
			{
				Area<Integration.Contexts.Admin.BlogsController>("admin", Resources<BlogsController>);
				Area<BlogsController>("", () =>
				{
					// Map("images/{id}.jpg").To<ImagesController>(x => x.Show(1));
					Resources<BlogsController>();
					Resources<PostsController>();
				});
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		Behaves_like<BlogsResources> a_blogs_resource;

		Behaves_like<PostsResources> a_posts_resource;

		Behaves_like<AdminBlogsResources> admin_blogs_resource;
	}

	public class when_mapping_an_area : base_context
	{
		public class BlogArea : RestfulRoutingArea
		{
			public BlogArea()
			{
				Area<Integration.Contexts.Admin.BlogsController>("admin");

				Resources<Integration.Contexts.Admin.BlogsController>();
			}
		}

		Because of = () => new BlogArea().RegisterRoutes(routes);

		Behaves_like<AdminBlogsResources> admin_blogs_resource;
	}
}