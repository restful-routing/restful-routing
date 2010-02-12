using System.Web.Mvc;

namespace RestfulRouting.Tests.Integration.Contexts
{
	public class CommentsController : Controller
	{
		public ActionResult Index(int? blogId, int? postId)
		{
			return Content("");
		}

		public ActionResult New(int? blogId, int? postId)
		{
			return Content("");
		}

		public ActionResult Create(int? blogId, int? postId)
		{
			return Content("");
		}

		public ActionResult Edit(int? blogId, int? postId, int id)
		{
			return Content("");
		}

		public ActionResult Update(int? blogId, int? postId, int id)
		{
			return Content("");
		}

		public ActionResult Delete(int? blogId, int? postId, int id)
		{
			return Content("");
		}

		public ActionResult Destroy(int? blogId, int? postId, int id)
		{
			return Content("");
		}

		public ActionResult Show(int? blogId, int? postId, int id)
		{
			return Content("");
		}
	}
}