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
    public class when_cloning : base_context
    {
        private RouteConfiguration _clone;

        protected override void given()
        {
            base.given();
            _configuration.As = "1";
            _configuration.IdValidationRegEx = @"\d+";
            _configuration.PathPrefix = "test";
            _configuration.Shallow = true;
            _configuration.ActionNames.Show = "view";
            _configuration.ActionNames.New = "make";
            _configuration.ActionNames.Create = "build";
            _configuration.ActionNames.Edit = "change";
            _configuration.ActionNames.Update = "amend";
            _configuration.ActionNames.Delete = "bin";
            _configuration.ActionNames.Destroy = "abolish";
        }

        protected override void when()
        {
            _clone = (RouteConfiguration)((ICloneable)_configuration).Clone();
        }

        [Test]
        public void should_do_a_shallow_copy()
        {
            _clone.As.ShouldBe("1");
            _clone.IdValidationRegEx.ShouldBe(@"\d+");
            _clone.PathPrefix.ShouldBe("test");
            _clone.Shallow.ShouldBeTrue();
            _clone.ActionNames.Show.ShouldBe("view");
            _clone.ActionNames.New.ShouldBe("make");
            _clone.ActionNames.Create.ShouldBe("build");
            _clone.ActionNames.Edit.ShouldBe("change");
            _clone.ActionNames.Update.ShouldBe("amend");
            _clone.ActionNames.Delete.ShouldBe("bin");
            _clone.ActionNames.Destroy.ShouldBe("abolish");
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
