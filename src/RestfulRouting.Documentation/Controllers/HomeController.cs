using System.Web.Mvc;

namespace RestfulRouting.Documentation.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
