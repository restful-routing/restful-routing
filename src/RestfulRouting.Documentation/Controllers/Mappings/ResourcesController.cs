using System.Web.Mvc;
using MvcFlash.Core;
using RestfulRouting.Documentation.Models;

namespace RestfulRouting.Documentation.Controllers.Mappings {
    public class ResourcesController : ApplicationController {
        public ActionResult Index(bool flash = false) {
            if (flash) {
                Flash.Success("You'd probably be looking at many resources right now. [GET]!");
            }
            return View();
        }

        public ActionResult Show(int id) {
            Flash.Success("You are getting a show [GET] for {0}.".With(id));
            return RedirectToAction("index");
        }

        public ActionResult Edit(int id) {
            Flash.Success("You just clicked edit for {0} [GET], you should be proud of yourself.".With(id));
            return RedirectToAction("index");
        }

        public ActionResult Create() {
            Flash.Success("You just created [POST] a new resource.");
            return RedirectToAction("index");
        }

        public ActionResult Update(int id) {
            Flash.Success("You just updated [PUT] your resource {0}, so cool!".With(id));
            return RedirectToAction("index");
        }

        public ActionResult New() {
            Flash.Success("Let's start creating [GET] a new resource.");
            return RedirectToAction("index");
        }

        public ActionResult Destroy(int id) {
            Flash.Success("Destroy! Destroy! Destroy {0}! [DELETE]".With(id));
            return RedirectToAction("index");
        }

        public ActionResult Lonely(int id) {
            Flash.Success("Called a route registered using Member on a resources controller with {0}.".With(id));
            return RedirectToAction("show", "extras");
        }

        public ActionResult Many() {
            Flash.Success("Called a route registered using Collection on a resources controller.");
            return RedirectToAction("show", "extras");
        }

    }
}
