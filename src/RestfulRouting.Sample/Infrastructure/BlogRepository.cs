using System;
using System.Collections.Generic;
using System.Linq;
using RestfulRouting.Sample.Models;

namespace RestfulRouting.Sample.Infrastructure
{
	public class BlogRepository
	{
		private static IList<Blog> _data;

		static BlogRepository()
		{
			_data = new List<Blog>
			        	{
			        		new Blog{ Id = 1, Author = "John Doe" },
			        		new Blog{ Id = 2, Author = "Jane Doe" }
			        	};
		}

		public IList<Blog> GetAll()
		{
			return _data.OrderBy(x => x.Id).ToList();
		}

		public Blog Get(int id)
		{
			return _data.SingleOrDefault(x => x.Id == id);
		}

		public void Save(Blog blog)
		{
			if (blog.Id == 0)
			{
				Create(blog);
			}
			else
			{
				Update(blog);
			}
		}

		private void Update(Blog blog)
		{
			var existing = Get(blog.Id);
			existing.Author = blog.Author;
		}

		private void Create(Blog blog)
		{
			blog.Id = _data.Count + 1;
			_data.Add(blog);
		}

		public void Delete(int id)
		{
			var blog = Get(id);
			_data.Remove(blog);
		}
	}
}