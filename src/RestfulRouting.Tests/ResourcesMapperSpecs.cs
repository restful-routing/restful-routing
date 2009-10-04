using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using NUnit.Should;
using RestfulRouting.Tests;
using RestfulRouting;
using MvcContrib.TestHelper;

namespace ResourcesMapperSpec
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

	[TestFixture]
	public class when_a_blogs_resource_has_been_mapped : base_context
	{
		protected override void when()
		{
			_map.Resources<BlogsController>();
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
		protected override void when()
		{
			_map.Resources<BlogsController>(x => x.Resources<PostsController>());
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
		protected override void when()
		{
			_map.Resources<BlogsController>(x =>
			                    	{
			                    		x.Resources<PostsController>(m =>
			                    		                  	{
			                    		                  		m.Resources<CommentsController>();
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
	public class when_using_a_different_resource_name_to_the_controller_name : base_context
	{
		protected override void when()
		{
			_map.Resources<BlogsController>(x => x.Controller = "weblogs");
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

	[TestFixture]
	public class when_specifying_a_string_id_requirement : base_context
	{
		protected override void when()
		{
			_map.Resources<BlogsController>(x => x.IdValidationRegEx = @"\d+");
		}

		[Test]
		public void should_map_integers()
		{
			"~/blogs/1234".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1234));
		}

		[Test]
		public void should_not_map_strings()
		{
			"~/blogs/janedoe".Route().ShouldBeNull();
		}
	}

	[TestFixture]
	public class when_specifying_an_as_requirements : base_context
	{
		protected override void when()
		{
			_map.Resources<BlogsController>(x => x.As = "weblogs");
		}

		[Test]
		public void should_map_with_specified_resource_name()
		{
			"~/weblogs".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Index());
		}
	}

	[TestFixture]
	public class when_specifying_action_names: base_context
	{
		protected override void when()
		{
			_map.Resources<PhotosController>(x => {
				x.ActionNames.Index = "list";
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

	[TestFixture]
	public class when_specifying_custom_resource_members : base_context
	{
		protected override void when()
		{
			_map.Resources<PhotosController>(x =>
			{
				x.ActionNames.AddMemberRoute<PhotosController>(a => a.MoveUp(1), HttpVerbs.Post);
				x.ActionNames.AddMemberRoute<PhotosController>(a => a.MoveDown(1), HttpVerbs.Get, HttpVerbs.Post);
			});
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
	public class when_specifying_custom_collection_actions : base_context
	{
		protected override void when()
		{
			_map.Resources<PhotosController>(x =>
			{
				x.ActionNames.AddCollectionRoute<PhotosController>(c => c.Online(), HttpVerbs.Post);
				x.ActionNames.AddCollectionRoute<PhotosController>(c => c.Offline(), HttpVerbs.Get, HttpVerbs.Post);
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
