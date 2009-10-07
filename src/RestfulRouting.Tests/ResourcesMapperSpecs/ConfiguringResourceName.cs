using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	[TestFixture]
	public class when_specifying_an_as_requirement : route_test_context
	{
		protected override void when()
		{
			_map.Resources<BlogsController>(x => x.As = "weblogs");
		}

		[Test]
		public void should_map_with_specified_resource_name()
		{
			"~/weblogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());
		}

		[Test]
		public void should_map_show()
		{
			"~/weblogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1));
		}

		[Test]
		public void should_map_new()
		{
			"~/weblogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New());
		}

		[Test]
		public void should_map_edit()
		{
			"~/weblogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1));
		}

		[Test]
		public void should_map_delete()
		{
			"~/weblogs/1/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Delete(1));
		}

		[Test]
		public void should_map_update()
		{
			"~/weblogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1));
		}

		[Test]
		public void should_map_destroy()
		{
			"~/weblogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1));
		}

		[Test]
		public void should_map_create()
		{
			"~/weblogs".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create());
		}
	}
}
