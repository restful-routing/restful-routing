using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using NUnit.Should;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	namespace when_excluding_actions
	{
		[TestFixture]
		public class when_excluding_index : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("index"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs".WithMethod(HttpVerbs.Get).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_show : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("show"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs/1".WithMethod(HttpVerbs.Get).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_edit : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("edit"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_update : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("update"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs/1".WithMethod(HttpVerbs.Put).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_new : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("new"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs/1/new".WithMethod(HttpVerbs.Get).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_create : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("create"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs".WithMethod(HttpVerbs.Post).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_delete : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("delete"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs/1/delete".WithMethod(HttpVerbs.Get).ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_excluding_destroy : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Except("destroy"));
			}

			[Test]
			public void should_not_map()
			{
				"~/blogs/1".WithMethod(HttpVerbs.Delete).ShouldBeNull();
			}
		}
	}
}
