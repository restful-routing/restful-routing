using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using NUnit.Should;

namespace RestfulRouting.Tests.ResourcesMapperSpecs
{
	namespace when_including_actions
	{
		[TestFixture]
		public class when_including_index : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>(config => config.Only("index"));
			}

			[Test]
			public void should_map_index()
			{
				_routes.Count.ShouldBe(2); // with post override route
				"~/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());
			}
		}
	}
}
