using System.Web.Mvc;
using NUnit.Framework;
using RestfulRouting.Tests;
using MvcContrib.TestHelper;

namespace ResourceMapperSpecs
{
	[TestFixture]
	public class when_specifying_custom_resource_members : route_test_context
	{
		protected override void when()
		{
			_map.Resource<SessionController>(x =>
			{
				x.ActionNames.AddMemberRoute<SessionController>(a => a.MoveUp(), HttpVerbs.Post);
				x.ActionNames.AddMemberRoute<SessionController>(a => a.MoveDown(), HttpVerbs.Get, HttpVerbs.Post);
			});
		}

		[Test]
		public void should_map_moveup()
		{
			"~/session/moveup".WithMethod(HttpVerbs.Post).ShouldMapTo<SessionController>(x => x.MoveUp());
		}

		[Test]
		public void should_map_movedown_get()
		{
			"~/session/movedown".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionController>(x => x.MoveDown());
		}

		[Test]
		public void should_map_movedown_post()
		{
			"~/session/movedown".WithMethod(HttpVerbs.Post).ShouldMapTo<SessionController>(x => x.MoveDown());
		}
	}
}
