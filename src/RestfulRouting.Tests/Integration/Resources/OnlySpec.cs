using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Behaviours;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Resources
{
	[Behaviors]
	public class BlogsWithoutIndexAndUpdate
	{
		It should_map_get_index = () => "~/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());

		It should_map_put_update = () => "~/blogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1));

		It should_not_map_create = () => "~/blogs".WithMethod(HttpVerbs.Post).ShouldBeNull();

		It should_not_map_new = () => "~/blogs/new".WithMethod(HttpVerbs.Get).ShouldBeNull();

		It should_not_map_edit = () => "~/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldBeNull();

		It should_not_map_show = () => "~/blogs/1".WithMethod(HttpVerbs.Get).ShouldBeNull();

		It should_not_map_destroy = () => "~/blogs/1".WithMethod(HttpVerbs.Delete).ShouldBeNull();
	}

	public class OnlyTest : base_context
	{
		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Resources<BlogsController>(() => Only("index", "update"));
			}
		}

		private Because of = () => new BlogArea().RegisterRoutes(routes);

		Behaves_like<BlogsWithoutIndexAndUpdate> blogs_without_index_and_update;
	}

	public class OnlyWithNestedResource : base_context
	{
		public class BlogArea : RouteSet
		{
			public BlogArea()
			{
				Resources<BlogsController>(() =>
				                           	{
				                           		Only("index", "update");
				                           		Resources<PostsController>();
				                           	});
			}
		}

		private Because of = () => new BlogArea().RegisterRoutes(routes);

		Behaves_like<BlogsWithoutIndexAndUpdate> blogs_without_index_and_update;

		Behaves_like<PostsNestedUnderBlogs> posts_nested_under_blogs;
	}
}