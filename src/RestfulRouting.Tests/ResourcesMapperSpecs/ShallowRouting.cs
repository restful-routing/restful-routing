using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	[TestFixture]
	public class when_shallow_routing_is_specified
		: route_test_context
	{
		protected override void when()
		{
			_map.Resources<BlogsController>(config => config.Shallow = true, x =>
			{
				x.Resources<PostsController>(map =>
				{
					map.Resources<CommentsController>();
				});
			});
		}

		[Test]
		public void should_map_blog_posts_index()
		{
			"~/blogs/1/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index(1));
		}

		[Test]
		public void should_map_posts_index()
		{
			"~/posts".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index(null));
		}

		[Test]
		public void should_map_posts_show()
		{
			"~/posts/2".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Show(null, 2));
		}

		[Test]
		public void should_map_posts_new()
		{
			"~/posts/new".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.New(null));
		}

		[Test]
		public void should_map_posts_edit()
		{
			"~/posts/2/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Edit(null, 2));
		}

		[Test]
		public void should_map_posts_delete()
		{
			"~/posts/2/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Delete(null, 2));
		}

		[Test]
		public void should_map_posts_update()
		{
			"~/posts/2".WithMethod(HttpVerbs.Put).ShouldMapTo<PostsController>(x => x.Update(null, 2));
		}

		[Test]
		public void should_map_posts_destroy()
		{
			"~/posts/2".WithMethod(HttpVerbs.Delete).ShouldMapTo<PostsController>(x => x.Destroy(null, 2));
		}

		[Test]
		public void should_map_posts_create()
		{
			"~/posts".WithMethod(HttpVerbs.Post).ShouldMapTo<PostsController>(x => x.Create(null));
		}


		[Test]
		public void should_map_post_comments_index()
		{
			"~/posts/2/comments".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Index(null, 2));
		}

		[Test]
		public void should_map_comments_index()
		{
			"~/comments".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Index(null, null));
		}

		[Test]
		public void should_map_comments_show()
		{
			"~/comments/3".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Show(null, null, 3));
		}

		[Test]
		public void should_map_comments_new()
		{
			"~/comments/new".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.New(null, null));
		}

		[Test]
		public void should_map_comments_edit()
		{
			"~/comments/3/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Edit(null, null, 3));
		}

		[Test]
		public void should_map_comments_delete()
		{
			"~/comments/3/delete".WithMethod(HttpVerbs.Get).ShouldMapTo<CommentsController>(x => x.Delete(null, null, 3));
		}

		[Test]
		public void should_map_comments_update()
		{
			"~/comments/3".WithMethod(HttpVerbs.Put).ShouldMapTo<CommentsController>(x => x.Update(null, null, 3));
		}

		[Test]
		public void should_map_comments_destroy()
		{
			"~/comments/3".WithMethod(HttpVerbs.Delete).ShouldMapTo<CommentsController>(x => x.Destroy(null, null, 3));
		}

		[Test]
		public void should_map_comments_create()
		{
			"~/comments".WithMethod(HttpVerbs.Post).ShouldMapTo<CommentsController>(x => x.Create(null, null));
		}
	}
}
