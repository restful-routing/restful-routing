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
			if (!filterContext.RouteData.Values.ContainsKey("blogId")) return;

			var blogId = int.Parse(filterContext.RouteData.Values["blogId"].ToString());
			_blog = new Blog{ Id = blogId };
		}

		public ActionResult Index()
		{
            return RespondTo.Do(format: RouteData.Format())
                  .Default(View(SampleData.Posts()))
                  .Format("json", Json(SampleData.Posts()).AllowGet())
                .End();
		}

		public ActionResult New()
		{
			return View(new Post{ Blog = _blog });
		}

		public ActionResult Create(Post blog)
		{
			TempData["notice"] = "Created";

			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			var post = SampleData.Post(id);
			post.Blog = _blog;
			return View(post);
		}

		public ActionResult Update(int id)
		{
			TempData["notice"] = "Updated " + id;

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var post = SampleData.Post(id);
			post.Blog = _blog;
			return View(post);
		}

		public ActionResult Destroy(int id)
		{
			TempData["notice"] = "Deleted " + id;

			return RedirectToAction("Index");
		}

		public ActionResult Show(int id)
		{
            return RespondTo.Do(format: RouteData.Format())
               .Default(View(SampleData.Post(id)))
               .Format("json", Json(SampleData.Post(id)).AllowGet())
             .End();
		}
	}
}
