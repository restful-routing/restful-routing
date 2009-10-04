using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Tests;

namespace ResourceMapperSpecs
{
	[TestFixture]
	public class when_specifying_an_as_requirement : route_test_context
	{
		protected override void when()
		{
			_map.Resource<SessionController>(x => x.As = "sesh");
		}

		[Test]
		public void should_map_with_specified_resource_name()
		{
			"~/sesh".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionController>(x => x.Show());
		}
	}
}
