using System.Web.Mvc;

namespace RestfulRouting.Sample.Controllers
{
	public class RootController : Controller
	{
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Blogs");
		}
	}
}