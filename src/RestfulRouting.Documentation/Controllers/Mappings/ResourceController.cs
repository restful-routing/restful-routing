using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlash.Core;

namespace RestfulRouting.Documentation.Controllers.Mappings {
    public class ResourceController : ApplicationController {
        public ActionResult Show(bool flash = false) {
            if (flash) {
                Flash.Success("Welcome to the Show [GET]!");
            }
            return View();
        }

        public ActionResult Edit() {
            Flash.Success("You just clicked edit [GET], you should be proud of yourself.");
            return RedirectToAction("show");
        }

        public ActionResult Create() {
            Flash.Success("You just created [POST] a new resource.");
            return RedirectToAction("show");
        }

        public ActionResult Update() {
            Flash.Success("You just updated [PUT] your resource, so cool!");
            return RedirectToAction("show");
        }

        public ActionResult New() {
            Flash.Success("Let's start creating [GET] a new resource.");
            return RedirectToAction("show");
        }

        public ActionResult Destroy() {
            Flash.Success("Destroy! Destroy! [DELETE]");
            return RedirectToAction("show");
        }
    }
}
