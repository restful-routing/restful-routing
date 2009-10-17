using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Tests;

namespace MappingNamespaces
{
	[TestFixture]
	public class when_mapping_a_blogs_resource_in_the_admin_namespace : route_test_context
	{
		protected override void when()
		{
			_map.Namespace("admin", map =>
			                        	{
											map.Resources<BlogsController>();
			                        	});
		}

		[Test]
		public void should_map_index()
		{
			"~/admin/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());
		}

		[Test]
		public void should_map_show()
		{
			"~/admin/blogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1));
		}

		[Test]
		public void should_map_new()
		{
			"~/admin/blogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New());
		}

		[Test]
		public void should_map_edit()
		{
			"~/admin/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1));
		}

		[Test]
		public void should_map_delete()
		{
			"~/admin/blogs/1/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Delete(1));
		}

		[Test]
		public void should_map_update()
		{
			"~/admin/blogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1));
		}

		[Test]
		public void should_map_destroy()
		{
			"~/admin/blogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1));
		}

		[Test]
		public void should_map_create()
		{
			"~/admin/blogs".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create());
		}
	}
}