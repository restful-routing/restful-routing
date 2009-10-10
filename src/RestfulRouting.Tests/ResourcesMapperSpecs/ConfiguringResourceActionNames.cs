using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	[TestFixture]
	public class when_configuring_action_names : route_test_context
	{
		protected override void when()
		{
			_map.Resources<PhotosController>(x =>
			{
				x.Index = "list";
				x.Show = "view";
				x.New = "make";
				x.Create = "build";
				x.Edit = "change";
				x.Update = "amend";
				x.Delete = "bin";
				x.Destroy = "abolish";
			});
		}

		[Test]
		public void should_map_list()
		{
			"~/photos".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.List());
		}

		[Test]
		public void should_map_make()
		{
			"~/photos/make".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.Make());
		}

		[Test]
		public void should_map_build()
		{
			"~/photos".WithMethod(HttpVerbs.Post).ShouldMapTo<PhotosController>(x => x.Build());
		}

		[Test]
		public void should_map_change()
		{
			"~/photos/1/change".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.Change(1));
		}

		[Test]
		public void should_map_amend()
		{
			"~/photos/1".WithMethod(HttpVerbs.Put).ShouldMapTo<PhotosController>(x => x.Amend(1));
		}

		[Test]
		public void should_map_abolish()
		{
			"~/photos/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<PhotosController>(x => x.Abolish(1));
		}

		[Test]
		public void should_map_bin()
		{
			"~/photos/1/bin".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.Bin(1));
		}

		[Test]
		public void should_map_view()
		{
			"~/photos/1".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.View(1));
		}
	}
}
