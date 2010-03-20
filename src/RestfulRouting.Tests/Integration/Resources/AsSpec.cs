using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts.Admin;

namespace RestfulRouting.Tests.Integration.Resources
{
	public class AsTest : base_context
	{
		public class BlogArea : RestfulRoutingArea
		{
			public BlogArea()
			{
				Resources<BlogsController>(() => As("weblogs"));
			}
		}

		private Because of = () => new BlogArea().RegisterRoutes(routes);

		It should_map_get_index = () => "~/weblogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());

		It should_map_get_show = () => "~/weblogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1));

		It should_map_get_new = () => "~/weblogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New());

		It should_map_post_create = () => "~/weblogs".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create());

		It should_map_get_edit = () => "~/weblogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1));

		It should_map_put_update = () => "~/weblogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1));

		It should_map_delete_destroy = () => "~/weblogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1));
	}
}