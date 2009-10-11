using System;
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
			_configuration.AddCollectionRoute<PhotosController>(x => x.DoSomething(1));
			_configuration.AddCollectionRoute<PhotosController>(x => x.Make(), HttpVerbs.Get, HttpVerbs.Post, HttpVerbs.Put, HttpVerbs.Delete, HttpVerbs.Head);
		}

		[Test]
		public void should_default_to_GET()
		{
			var verbArray = _configuration.GetCollectionVerbArray("dosomething");
			verbArray.Count().ShouldBe(1);
			verbArray.First().ShouldBe("GET");
		}

		[Test]
		public void should_return_verbs_as_upper_case_string_array()
		{
			var verbArray = _configuration.GetCollectionVerbArray("make");
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
			_configuration.AddMemberRoute<PhotosController>(x => x.DoSomething(1));
			_configuration.AddMemberRoute<PhotosController>(x => x.Make(), HttpVerbs.Get, HttpVerbs.Post, HttpVerbs.Put, HttpVerbs.Delete, HttpVerbs.Head);
		}


		[Test]
		public void should_default_to_GET()
		{
			var verbArray = _configuration.GetMemberVerbArray("dosomething");
			verbArray.Count().ShouldBe(1);
			verbArray.First().ShouldBe("GET");
		}

		[Test]
		public void should_return_verbs_as_upper_case_string_array()
		{
			var verbArray = _configuration.GetMemberVerbArray("make");
			verbArray.Count().ShouldBe(5);
			verbArray.ShouldContain("GET");
			verbArray.ShouldContain("POST");
			verbArray.ShouldContain("PUT");
			verbArray.ShouldContain("DELETE");
			verbArray.ShouldContain("HEAD");
		}
	}

	[TestFixture]
	public class when_excluding_actions : base_context
	{
		protected override void when()
		{
			_configuration.Except("delete");
		}

		[Test]
		public void should_not_include_delete()
		{
			_configuration.Includes("delete").ShouldBeFalse();
		}

		[Test]
		public void should_include_other_defaults()
		{
			_configuration.Includes("index").ShouldBeTrue();
			_configuration.Includes("show").ShouldBeTrue();
			_configuration.Includes("new").ShouldBeTrue();
			_configuration.Includes("create").ShouldBeTrue();
			_configuration.Includes("edit").ShouldBeTrue();
			_configuration.Includes("update").ShouldBeTrue();
			_configuration.Includes("destroy").ShouldBeTrue();
		}
	}

	[TestFixture]
	public class when_including_actions : base_context
	{
		protected override void when()
		{
			_configuration.Only("index");
		}

		[Test]
		public void should_not_include_delete()
		{
			_configuration.Includes("index").ShouldBeTrue();
		}

		[Test]
		public void should_include_other_defaults()
		{
			_configuration.Includes("delete").ShouldBeFalse();
			_configuration.Includes("show").ShouldBeFalse();
			_configuration.Includes("new").ShouldBeFalse();
			_configuration.Includes("create").ShouldBeFalse();
			_configuration.Includes("edit").ShouldBeFalse();
			_configuration.Includes("update").ShouldBeFalse();
			_configuration.Includes("destroy").ShouldBeFalse();
		}
	}
}
