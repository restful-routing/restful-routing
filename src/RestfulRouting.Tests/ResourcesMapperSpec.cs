using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RestfulRouting.Tests;
using RestfulRouting;
using MvcContrib.TestHelper;

namespace ResourcesMapperSpec
{
	public class Blog
	{
		
	}

	public class BlogsController : Controller
	{
		public ActionResult Index()
		{
			return Content("");
		}

		public ActionResult New()
		{
			return Content("");
		}

		public ActionResult Create()
		{
			return Content("");
		}

		public ActionResult Edit(int id)
		{
			return Content("");
		}

		public ActionResult Update(int id)
		{
			return Content("");
		}

		public ActionResult Delete(int id)
		{
			return Content("");				
		}

		public ActionResult Destroy(int id)
		{
			return Content("");				
		}

		public ActionResult Show(int id)
		{
			return Content("");				
		}
	}

	[TestFixture]
	public class when_a_blogs_resource_has_been_mapped : Spec
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
			map.Resources<Blog>();
		}

		[Test]
		public void should_map_index()
		{
			"~/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());
		}

		[Test]
		public void should_map_show()
		{
			"~/blogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1));
		}

		[Test]
		public void should_map_new()
		{
			"~/blogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New());
		}

		[Test]
		public void should_map_edit()
		{
			"~/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1));
		}

		[Test]
		public void should_map_delete()
		{
			"~/blogs/1/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Delete(1));
		}

		[Test]
		public void should_map_update()
		{
			"~/blogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<BlogsController>(x => x.Update(1));				
		}

		[Test]
		public void should_map_destroy()
		{
			"~/blogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1));
		}

		[Test]
		public void should_map_create()
		{
			"~/blogs".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create());				
		}
	}

	public class when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource : Spec
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
			// TODO
		}


	}
}
