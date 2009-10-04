using System.Web.Mvc;
using NUnit.Framework;
using RestfulRouting.Tests;
using MvcContrib.TestHelper;

namespace ResourceMapperSpecs
{
	[TestFixture]
	public class when_specifying_action_names : route_test_context
	{
		protected override void when()
		{
			_map.Resource<PhotoController>(x =>
			{
				x.ActionNames.Show = "view";
				x.ActionNames.New = "make";
				x.ActionNames.Create = "build";
				x.ActionNames.Edit = "change";
				x.ActionNames.Update = "amend";
				x.ActionNames.Delete = "bin";
				x.ActionNames.Destroy = "abolish";
			});
		}

		[Test]
		public void should_map_show()
		{
			"~/photo".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotoController>(x => x.View());
		}

		[Test]
		public void should_map_create()
		{
			"~/photo".WithMethod(HttpVerbs.Post).ShouldMapTo<PhotoController>(x => x.Build());
		}

		[Test]
		public void should_map_new()
		{
			"~/photo/make".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotoController>(x => x.Make());
		}

		[Test]
		public void should_map_edit()
		{
			"~/photo/change".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotoController>(x => x.Change());
		}

		[Test]
		public void should_map_update()
		{
			"~/photo".WithMethod(HttpVerbs.Put).ShouldMapTo<PhotoController>(x => x.Amend());
		}


		[Test]
		public void should_map_delete()
		{
			"~/photo/bin".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotoController>(x => x.Bin());
		}

		[Test]
		public void should_map_destroy()
		{
			"~/photo".WithMethod(HttpVerbs.Delete).ShouldMapTo<PhotoController>(x => x.Abolish());
		}
	}
}
