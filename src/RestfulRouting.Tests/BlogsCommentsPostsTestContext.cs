using System.Web.Mvc;

namespace RestfulRouting.Tests
{
	public class Blog
	{

	}

	public class Post
	{

	}

	public class Comment
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

	public class PostsController : Controller
	{
		public ActionResult Index(int blogId)
		{
			return Content("");
		}

		public ActionResult New(int blogId)
		{
			return Content("");
		}

		public ActionResult Create(int blogId)
		{
			return Content("");
		}

		public ActionResult Edit(int blogId, int id)
		{
			return Content("");
		}

		public ActionResult Update(int blogId, int id)
		{
			return Content("");
		}

		public ActionResult Delete(int blogId, int id)
		{
			return Content("");
		}

		public ActionResult Destroy(int blogId, int id)
		{
			return Content("");
		}

		public ActionResult Show(int blogId, int id)
		{
			return Content("");
		}
	}

	public class CommentsController : Controller
	{
		public ActionResult Index(int blogId, int postId)
		{
			return Content("");
		}

		public ActionResult New(int blogId, int postId)
		{
			return Content("");
		}

		public ActionResult Create(int blogId, int postId)
		{
			return Content("");
		}

		public ActionResult Edit(int blogId, int postId, int id)
		{
			return Content("");
		}

		public ActionResult Update(int blogId, int postId, int id)
		{
			return Content("");
		}

		public ActionResult Delete(int blogId, int postId, int id)
		{
			return Content("");
		}

		public ActionResult Destroy(int blogId, int postId, int id)
		{
			return Content("");
		}

		public ActionResult Show(int blogId, int postId, int id)
		{
			return Content("");
		}
	}

	public class WeblogsController : Controller
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
}
