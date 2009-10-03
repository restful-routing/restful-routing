using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;
using MvcContrib.TestHelper;

namespace ResourceMapperSpec
{
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
	}

	[TestFixture]
	public class when_a_session_resouce_has_been_mapped : Spec
	{

		private RouteCollection _routes;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
		}

		protected override void when()
		{
			var map = new RestfulRouteMapper(_routes);
			map.Resource("session");
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
