using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using RestfulRouting.Tests;
using RestfulRouting;
using MvcContrib.TestHelper;

namespace ResourcesMapperSpec
{
	[TestFixture]
	public class when_a_blogs_resource_has_been_mapped : Spec
	{
		private RouteCollection _routes;
		private RestfulRouteMapper _map;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
			_map = new RestfulRouteMapper(_routes);
		}

		protected override void when()
		{
			_map.Resources<Blog>();
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

	[TestFixture]
	public class when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource :
		when_a_blogs_resource_has_been_mapped
	{
		private RouteCollection _routes;
		private RestfulRouteMapper _map;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
			_map = new RestfulRouteMapper(_routes);
		}

		protected override void when()
		{
			_map.Resources<Blog>(x => x.Resources<Post>());
		}

		[Test]
		public void should_map_posts_index()
		{
			"~/blogs/1/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index(1));
		}

		[Test]
		public void should_map_posts_show()
		{
			"~/blogs/1/posts/2".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(1, 2));
		}

		[Test]
		public void should_map_posts_new()
		{
			"~/blogs/1/posts/new".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New(1));
		}

		[Test]
		public void should_map_posts_edit()
		{
			"~/blogs/1/posts/2/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(1, 2));
		}

		[Test]
		public void should_map_posts_delete()
		{
			"~/blogs/1/posts/2/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Delete(1, 2));
		}

		[Test]
		public void should_map_posts_update()
		{
			"~/blogs/1/posts/2".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(1, 2));
		}

		[Test]
		public void should_map_posts_destroy()
		{
			"~/blogs/1/posts/2".WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(1, 2));
		}

		[Test]
		public void should_map_posts_create()
		{
			"~/blogs/1/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create(1));
		}
	}

	[TestFixture]
	public class when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource_with_a_nested_comments_resource
		: when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource
	{
		private RouteCollection _routes;
		private RestfulRouteMapper _map;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
			_map = new RestfulRouteMapper(_routes);
		}

		protected override void when()
		{
			_map.Resources<Blog>(x =>
			                    	{
			                    		x.Resources<Post>(m =>
			                    		                  	{
			                    		                  		m.Resources<Comment>();
			                    		                  	});
			                    	});
		}

		[Test]
		public void should_map_post_comments_index()
		{
			"~/blogs/1/posts/2/comments".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Index(1, 2));
		}

		[Test]
		public void should_map_post_comments_show()
		{
			"~/blogs/1/posts/2/comments/3".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Show(1, 2, 3));
		}

		[Test]
		public void should_map_post_comments_new()
		{
			"~/blogs/1/posts/2/comments/new".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.New(1, 2));
		}

		[Test]
		public void should_map_post_comments_edit()
		{
			"~/blogs/1/posts/2/comments/3/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Edit(1, 2, 3));
		}

		[Test]
		public void should_map_post_comments_delete()
		{
			"~/blogs/1/posts/2/comments/3/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Delete(1, 2, 3));
		}

		[Test]
		public void should_map_post_comments_update()
		{
			"~/blogs/1/posts/2/comments/3".WithMethod(HttpVerbs.Put).ShouldMapTo<CommentsController>(x => x.Update(1, 2, 3));
		}

		[Test]
		public void should_map_post_comments_destroy()
		{
			"~/blogs/1/posts/2/comments/3".WithMethod(HttpVerbs.Delete).ShouldMapTo<CommentsController>(x => x.Destroy(1, 2, 3));
		}

		[Test]
		public void should_map_post_comments_create()
		{
			"~/blogs/1/posts/2/comments".WithMethod(HttpVerbs.Post).ShouldMapTo<CommentsController>(x => x.Create(1, 2));
		}
	}

	[TestFixture]
	public class when_using_a_different_resource_name_to_the_controller_name : Spec
	{
		private RouteCollection _routes;
		private RestfulRouteMapper _map;

		protected override void given()
		{
			RouteTable.Routes.Clear();
			_routes = RouteTable.Routes;
			_map = new RestfulRouteMapper(_routes);
		}

		protected override void when()
		{
			_map.WithConfiguration(x => x.Controller = "weblogs").Resources<Blog>();
		}

		[Test]
		public void should_map_index()
		{
			"~/blogs".WithMethod(HttpVerbs.Get).ShouldMapTo<WeblogsController>(x => x.Index());
		}

		[Test]
		public void should_map_show()
		{
			"~/blogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<WeblogsController>(x => x.Show(1));
		}

		[Test]
		public void should_map_new()
		{
			"~/blogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<WeblogsController>(x => x.New());
		}

		[Test]
		public void should_map_edit()
		{
			"~/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<WeblogsController>(x => x.Edit(1));
		}

		[Test]
		public void should_map_delete()
		{
			"~/blogs/1/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<WeblogsController>(x => x.Delete(1));
		}

		[Test]
		public void should_map_update()
		{
			"~/blogs/1".WithMethod(HttpVerbs.Put).ShouldMapTo<WeblogsController>(x => x.Update(1));
		}

		[Test]
		public void should_map_destroy()
		{
			"~/blogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<WeblogsController>(x => x.Destroy(1));
		}

		[Test]
		public void should_map_create()
		{
			"~/blogs".WithMethod(HttpVerbs.Post).ShouldMapTo<WeblogsController>(x => x.Create());
		}
	}
}
