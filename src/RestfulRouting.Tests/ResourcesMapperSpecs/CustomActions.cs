using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	[TestFixture]
	public class when_specifying_custom_resource_actions : route_test_context
	{
		protected override void when()
		{
			_map.Resources<PhotosController>(x =>
			{
				x.AddMemberRoute<PhotosController>(a => a.DoSomething(1));
				x.AddMemberRoute<PhotosController>(a => a.MoveUp(1), HttpVerbs.Post);
				x.AddMemberRoute<PhotosController>(a => a.MoveDown(1), HttpVerbs.Get, HttpVerbs.Post);
			});
		}

		[Test]
		public void should_default_to_GET()
		{
			"~/photos/1/dosomething".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.DoSomething(1));
		}

		[Test]
		public void should_map_moveup()
		{
			"~/photos/1/moveup".WithMethod(HttpVerbs.Post).ShouldMapTo<PhotosController>(x => x.MoveUp(1));
		}

		[Test]
		public void should_map_movedown_get()
		{
			"~/photos/1/movedown".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.MoveDown(1));
		}

		[Test]
		public void should_map_movedown_post()
		{
			"~/photos/1/movedown".WithMethod(HttpVerbs.Post).ShouldMapTo<PhotosController>(x => x.MoveDown(1));
		}
	}

	[TestFixture]
	public class when_specifying_custom_collection_actions : route_test_context
	{
		protected override void when()
		{
			_map.Resources<PhotosController>(x =>
			{
				x.AddCollectionRoute<PhotosController>(c => c.Online(), HttpVerbs.Post);
				x.AddCollectionRoute<PhotosController>(c => c.Offline(), HttpVerbs.Get, HttpVerbs.Post);
			});
		}

		[Test]
		public void should_map_online()
		{
			"~/photos/online".WithMethod(HttpVerbs.Post).ShouldMapTo<PhotosController>(x => x.Online());
		}

		[Test]
		public void should_map_pffline()
		{
			"~/photos/offline".WithMethod(HttpVerbs.Get).ShouldMapTo<PhotosController>(x => x.Offline());
		}

		[Test]
		public void should_map_offline()
		{
			"~/photos/offline".WithMethod(HttpVerbs.Post).ShouldMapTo<PhotosController>(x => x.Offline());
		}
	}
}
