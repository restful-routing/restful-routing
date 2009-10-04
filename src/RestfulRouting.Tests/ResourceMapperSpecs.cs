using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;
using MvcContrib.TestHelper;

namespace ResourceMapperSpec
{
	public class base_context : Spec
	{
		protected RouteCollection _routes;
		protected RestfulRouteMapper _map;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
			_map = new RestfulRouteMapper(_routes);
		}

		protected override void when()
		{
		}
	}

	public class SessionController : Controller
	{
		public ActionResult New()
		{
			return Content("");
		}

		public ActionResult Create()
		{
			return Content("");
		}

		public ActionResult Edit()
		{
			return Content("");
		}

		public ActionResult Update()
		{
			return Content("");
		}

		public ActionResult Delete()
		{
			return Content("");
		}

		public ActionResult Destroy()
		{
			return Content("");
		}

		public ActionResult Show()
		{
			return Content("");
		}

		public ActionResult MoveUp()
		{
			return Content("");
		}

		public ActionResult MoveDown()
		{
			return Content("");
		}
	}

	[TestFixture]
	public class when_a_session_resouce_has_been_mapped : base_context
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

	[TestFixture]
	public class when_specifying_an_as_requirement : base_context
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

	[TestFixture]
	public class when_specifying_custom_resource_members : base_context
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

	[TestFixture]
	public class when_specifying_action_names : base_context
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
