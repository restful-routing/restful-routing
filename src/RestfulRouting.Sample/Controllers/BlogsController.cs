using System.Collections.Generic;
using System.Web.Mvc;
using RestfulRouting.Sample.Infrastructure;
using RestfulRouting.Sample.Models;

namespace RestfulRouting.Sample.Controllers
{
	public class BlogsController : Controller
	{
		private BlogRepository _repository;

		public BlogsController() : this(new BlogRepository()) {}

		public BlogsController(BlogRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Index()
		{
			return View(_repository.GetAll());
		}

		public ActionResult New()
		{
			return View(new Blog());
		}

		public ActionResult Create([Bind(Exclude="Id")]Blog blog)
		{
			_repository.Save(blog);

			TempData["notice"] = "Created";

			return RedirectToAction("Index");
		}

		public ActionResult Edit(int id)
		{
			return View(_repository.Get(id));
		}

		public ActionResult Update(int id, Blog blog)
		{
			_repository.Save(blog);

			TempData["notice"] = "Updated " + id;

			return RedirectToAction("Index");
		}

		public ActionResult Delete(int id)
		{
			var blog = _repository.Get(id);

			return View(blog);
		}

		public ActionResult Destroy(int id)
		{
			_repository.Delete(id);

			TempData["notice"] = "Deleted " + id;

			return RedirectToAction("Index");
		}

		public ActionResult Show(int id)
		{
			return View(_repository.Get(id));
		}
	}
}