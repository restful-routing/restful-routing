using System.Web.Mvc;
using MvcFlash.Core;

namespace RestfulRouting.Documentation.Controllers.Mappings
{
    public class OtherResourcesController : Controller {
        public ActionResult Index() {
            Flash.Success("accessed a nested resource method");
            return RedirectToAction("index", "resources");
        }
    }
}
