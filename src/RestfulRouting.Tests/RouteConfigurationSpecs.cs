using System.Web.Mvc;
using NUnit.Framework;
using NUnit.Should;
using System.Linq;
using RestfulRouting;
using RestfulRouting.Tests;

namespace RouteConfigurationSpecs
{
	public abstract class base_context : Spec
	{
		protected RouteConfiguration _configuration;

		protected override void given()
		{
			_configuration = new RouteConfiguration();
		}
	}

	[TestFixture]
	public class when_adding_custom_collection_routes : base_context
	{
		protected override void when()
		{
			_configuration.ActionNames.AddCollectionRoute<PhotosController>(x => x.Make(), HttpVerbs.Get, HttpVerbs.Post, HttpVerbs.Put, HttpVerbs.Delete, HttpVerbs.Head);
		}

		[Test]
		public void should_return_verbs_as_upper_case_string_array()
		{
			var verbArray = _configuration.ActionNames.GetCollectionVerbArray("make");
			verbArray.Count().ShouldBe(5);
			verbArray.ShouldContain("GET");
			verbArray.ShouldContain("POST");
			verbArray.ShouldContain("PUT");
			verbArray.ShouldContain("DELETE");
			verbArray.ShouldContain("HEAD");
		}
	}

	[TestFixture]
	public class when_adding_custom_member_routes : base_context
	{
		protected override void when()
		{
			_configuration.ActionNames.AddMemberRoute<PhotosController>(x => x.Make(), HttpVerbs.Get, HttpVerbs.Post, HttpVerbs.Put, HttpVerbs.Delete, HttpVerbs.Head);
		}

		[Test]
		public void should_return_verbs_as_upper_case_string_array()
		{
			var verbArray = _configuration.ActionNames.GetMemberVerbArray("make");
			verbArray.Count().ShouldBe(5);
			verbArray.ShouldContain("GET");
			verbArray.ShouldContain("POST");
			verbArray.ShouldContain("PUT");
			verbArray.ShouldContain("DELETE");
			verbArray.ShouldContain("HEAD");
		}
	}
}
