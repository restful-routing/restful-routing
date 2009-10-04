using System.Web.Mvc;
using RestfulRouting.Sample.Infrastructure;
using RestfulRouting.Sample.Models;

namespace RestfulRouting.Sample.Controllers
{
	public class PostsController : Controller
	{
		private Blog _blog;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// accessing parent resource id
			if (!filterContext.ActionParameters.ContainsKey("blogId")) return;

			var blogId = (int)filterContext.ActionParameters["blogId"];
			_blog = new Blog{ Id = blogId };
		}

		public ActionResult Index()
		{
			return View(SampleData.Posts());
		}

		public ActionResult New()
		{
			return View(new Post());
		}

		public ActionResult Create(Post blog)
		{
			TempData["notice"] = "Created";

			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			return View(SampleData.Post(id));
		}

		public ActionResult Update(int id)
		{
			TempData["notice"] = "Updated " + id;

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			return View(SampleData.Post(id));
		}

		public ActionResult Destroy(int id)
		{
			TempData["notice"] = "Deleted " + id;

			return RedirectToAction("Index");
		}

		public ActionResult Show(int id)
		{
			return View(SampleData.Post(id));
		}
	}
}
