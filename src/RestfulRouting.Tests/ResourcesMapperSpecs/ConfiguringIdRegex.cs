using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using NUnit.Should;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	namespace when_specifying_an_integer_id_requirement
	{
		[TestFixture]
		public class when_mapping_a_non_nested_resource : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(x => x.IdValidationRegEx = @"\d+");
			}

			[Test]
			public void should_map_integers()
			{
				"~/blogs/1234".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1234));
			}

			[Test]
			public void should_not_map_strings()
			{
				"~/blogs/janedoe".Route().ShouldBeNull();
			}
		}

		[TestFixture]
		public class when_mapping_a_nested_resource : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(x => x.IdValidationRegEx = @"\d+", x => x.Resources<PostsController>());
			}

			[Test]
			public void should_map_integers()
			{
				"~/blogs/1234/posts/1".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1234, 1));
			}

			[Test]
			public void should_not_map_strings()
			{
				"~/blogs/janedoe/posts/lorem".Route().ShouldBeNull();
			}
		}
	}
}
