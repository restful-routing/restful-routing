using System.Collections.Generic;
using RestfulRouting.Sample.Models;

namespace RestfulRouting.Sample.Infrastructure
{
    public static class SampleData
    {
        public static IList<Blog> Blogs()
        {
            return new List<Blog>
                       {
                        new Blog{ Id = 1, Author = "John Doe" },
                        new Blog{ Id = 2, Author = "Jane Doe" }
                       };
        }

        public static Blog Blog(int id)
        {
            return new Blog{Id = id};
        }

        public static IList<Post> Posts()
        {
            return new List<Post>
                       {
                        new Post
                               {
                                Id = 1,
                                   Title = "Test 1",
                                Slug = "test-1",
                                Body = "Lorem ipsum"
                               },
                           new Post
                               {
                                Id = 2,
                                   Title = "Test 2",
                                Slug = "test-2",
                                Body = "Lorem ipsum"
                               }
                       };
        }

        public static Post Post(int id)
        {
            return new Post
            {
                Id = id,
                Title = "Test " + id,
                Slug = "test-" + id,
                Body = "Lorem ipsum"
            };
        }
    }
}
