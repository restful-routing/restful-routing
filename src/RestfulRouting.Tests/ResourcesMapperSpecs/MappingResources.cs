using System.Web.Mvc;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Tests;

namespace ResourcesMapperSpecs
{
	namespace when_a_blogs_resource_has_been_mapped
	{
		public abstract class base_context : route_test_context
		{
			protected override void when()
			{
				_map.Resources<BlogsController>();
			}
		}

		[TestFixture]
		public class when_requests_come_in_for_the_blogs_resource : base_context
		{
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
		public class when_generating_urls_for_the_blogs_resource : base_context
		{
			[Test]
			public void should_generate_index()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Index()).ShouldMapToUrl("/blogs");
			}

			[Test]
			public void should_generate_show()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Show(1)).ShouldMapToUrl("/blogs/1");
			}

			[Test]
			public void should_generate_new()
			{
				OutBoundUrl.Of<BlogsController>(x => x.New()).ShouldMapToUrl("/blogs/new");
			}

			[Test]
			public void should_generate_edit()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Edit(1)).ShouldMapToUrl("/blogs/1/edit");
			}

			[Test]
			public void should_generate_delete()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Delete(1)).ShouldMapToUrl("/blogs/1/delete");
			}

			[Test]
			public void should_generate_update()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Update(1)).ShouldMapToUrl("/blogs/1");
			}

			[Test]
			public void should_generate_destroy()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Destroy(1)).ShouldMapToUrl("/blogs/1");
			}

			[Test]
			public void should_generate_create()
			{
				OutBoundUrl.Of<BlogsController>(x => x.Create()).ShouldMapToUrl("/blogs");
			}
		}

		namespace when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource
		{
			[TestFixture]
			public class when_requests_come_in_for_the_blog_posts_resource :
				when_requests_come_in_for_the_blogs_resource
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
			public class when_generating_urls_for_the_blog_posts_resource
				: base_context
			{
				protected override void when()
				{
					_map.Resources<BlogsController>(x => x.Resources<PostsController>());
				}

				[Test]
				public void should_generate_posts_index()
				{
					OutBoundUrl.Of<PostsController>(x => x.Index(1)).ShouldMapToUrl("/blogs/1/posts");
				}

				[Test]
				public void should_generate_posts_show()
				{
					OutBoundUrl.Of<PostsController>(x => x.Show(1, 2)).ShouldMapToUrl("/blogs/1/posts/2");
				}

				[Test]
				public void should_generate_posts_new()
				{
					OutBoundUrl.Of<PostsController>(x => x.New(1)).ShouldMapToUrl("/blogs/1/posts/new");
				}

				[Test]
				public void should_generate_posts_edit()
				{
					OutBoundUrl.Of<PostsController>(x => x.Edit(1, 2)).ShouldMapToUrl("/blogs/1/posts/2/edit");
				}

				[Test]
				public void should_generate_posts_delete()
				{
					OutBoundUrl.Of<PostsController>(x => x.Delete(1, 2)).ShouldMapToUrl("/blogs/1/posts/2/delete");
				}

				[Test]
				public void should_generate_posts_update()
				{
					OutBoundUrl.Of<PostsController>(x => x.Update(1, 2)).ShouldMapToUrl("/blogs/1/posts/2");
				}

				[Test]
				public void should_generate_posts_destroy()
				{
					OutBoundUrl.Of<PostsController>(x => x.Destroy(1, 2)).ShouldMapToUrl("/blogs/1/posts/2");
				}

				[Test]
				public void should_generate_posts_create()
				{
					OutBoundUrl.Of<PostsController>(x => x.Create(1)).ShouldMapToUrl("/blogs/1/posts");
				}
			}

			namespace when_a_blogs_resource_has_been_mapped_with_a_nested_posts_resource_with_a_nested_comments_resource
			{
				[TestFixture]
				public class when_a_request_comes_in_for_the_blog_post_comments_resource
					: when_requests_come_in_for_the_blog_posts_resource
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
				public class when_generating_urls_for_the_blog_post_comments_resource
					: base_context
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
						OutBoundUrl.Of<CommentsController>(x => x.Index(1, 2)).ShouldMapToUrl("/blogs/1/posts/2/comments");
					}

					[Test]
					public void should_map_post_comments_show()
					{
						OutBoundUrl.Of<CommentsController>(x => x.Show(1, 2, 3)).ShouldMapToUrl("/blogs/1/posts/2/comments/3");
					}

					[Test]
					public void should_map_post_comments_new()
					{
						OutBoundUrl.Of<CommentsController>(x => x.New(1, 2)).ShouldMapToUrl("/blogs/1/posts/2/comments/new");
					}

					[Test]
					public void should_map_post_comments_edit()
					{
						OutBoundUrl.Of<CommentsController>(x => x.Edit(1, 2, 3)).ShouldMapToUrl("/blogs/1/posts/2/comments/3/edit");
					}

					[Test]
					public void should_map_post_comments_delete()
					{
						OutBoundUrl.Of<CommentsController>(x => x.Delete(1, 2, 3)).ShouldMapToUrl("/blogs/1/posts/2/comments/3/delete");
					}

					[Test]
					public void should_map_post_comments_update()
					{
						OutBoundUrl.Of<CommentsController>(x => x.Update(1, 2, 3)).ShouldMapToUrl("/blogs/1/posts/2/comments/3");
					}

					[Test]
					public void should_map_post_comments_destroy()
					{
						OutBoundUrl.Of<CommentsController>(x => x.Destroy(1, 2, 3)).ShouldMapToUrl("/blogs/1/posts/2/comments/3");
					}

					[Test]
					public void should_map_post_comments_create()
					{
						OutBoundUrl.Of<CommentsController>(x => x.Create(1, 2)).ShouldMapToUrl("/blogs/1/posts/2/comments");
					}
				}
			}


		}

	}
}
