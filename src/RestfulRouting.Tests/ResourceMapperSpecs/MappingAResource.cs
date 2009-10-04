using System.Web.Mvc;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;
using MvcContrib.TestHelper;

namespace ResourceMapperSpecs
{
	[TestFixture]
	public class when_a_session_resouce_has_been_mapped : route_test_context
	{

		protected override void when()
		{
			var map = new RestfulRouteMapper(_routes);
			map.Resource<SessionController>();
		}

		[Test]
		public void should_map_show()
		{
			"~/session".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionController>(x => x.Show());
		}

		[Test]
		public void should_map_create()
		{
			"~/session".WithMethod(HttpVerbs.Post).ShouldMapTo<SessionController>(x => x.Create());
		}

		[Test]
		public void should_map_new()
		{
			"~/session/new".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionController>(x => x.New());
		}

		[Test]
		public void should_map_edit()
		{
			"~/session/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionController>(x => x.Edit());
		}

		[Test]
		public void should_map_update()
		{
			"~/session".WithMethod(HttpVerbs.Put).ShouldMapTo<SessionController>(x => x.Update());
		}


		[Test]
		public void should_map_delete()
		{
			"~/session/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionController>(x => x.Delete());
		}

		[Test]
		public void should_map_destroy()
		{
			"~/session".WithMethod(HttpVerbs.Delete).ShouldMapTo<SessionController>(x => x.Destroy());
		}
	}
}
