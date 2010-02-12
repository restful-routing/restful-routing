using System;
using System.Web.Mvc;

namespace RestfulRouting.Tests.Integration.Contexts
{
	public class PostsController : Controller
	{
		public ActionResult Index(int? blogId)
		{
			return Content("");
		}

		public ActionResult New(int? blogId)
		{
			return Content("");
		}

		public ActionResult Create(int? blogId)
		{
			return Content("");
		}

		public ActionResult Edit(int? blogId, int id)
		{
			return Content("");
		}

		public ActionResult Update(int? blogId, int id)
		{
			return Content("");
		}

		public ActionResult Delete(int? blogId, int id)
		{
			return Content("");
		}

		public ActionResult Destroy(int? blogId, int id)
		{
			return Content("");
		}

		public ActionResult Show(int? blogId, int id)
		{
			return Content("");
		}

		public ActionResult Post(int year, string slug)
		{
			return null;
		}
	}
}