using System.Web.Mvc;
using RestfulRouting.Sample.Infrastructure;
using RestfulRouting.Sample.Models;

namespace RestfulRouting.Sample.Controllers
{
    public class BlogsController : Controller
    {
        public ActionResult Index()
        {
            return View(SampleData.Blogs());
        }

        public ActionResult New()
        {
            return View(new Blog());
        }
        
        public ActionResult Test(int id, string t)
        {
            var c = ControllerContext.RouteData.Values.Count;

            return Content("t: " + t);
        }

        public ActionResult Create()
        {
            TempData["notice"] = "Created";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(SampleData.Blog(id));
        }

        public ActionResult Update(int id, Blog blog)
        {
            TempData["notice"] = "Updated " + id;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(SampleData.Blog(id));
        }

        public ActionResult Destroy(int id)
        {
            TempData["notice"] = "Deleted " + id;

            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            return View(SampleData.Blog(id));
        }
    }
}