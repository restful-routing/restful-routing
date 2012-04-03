using System;
using System.Web.Mvc;
using MvcFlash.Core;

namespace RestfulRouting.Documentation.Controllers.Mappings {
    public class ExtrasController : ApplicationController {
        public ActionResult Show() {
            return View();
        }

        public ActionResult UsingPath() {
            Flash.Success("Hey you just hit a route registered using Path");
            return RedirectToAction("show");
        }

        public ActionResult UsingRoute() {
            Flash.Success("Hey you just hit a route registered using Route");
            return RedirectToAction("show");
        }

        public ActionResult Member() {
            Flash.Success("Hey you just hit a route registered using Member");
            return RedirectToAction("show");
        }

        public ActionResult Awesome() {
            var model = new Awesome {
                date = DateTime.UtcNow.ToShortDateString(),
                id = "awesomwe_id",
                message = "hey you are seeing this in message"
            };

            return RespondTo(format => {
                format.Default = RedirectToAction("show");
                format.Json = () => {
                    model.id = "awesome_json";
                    return Json(model, JsonRequestBehavior.AllowGet);
                };
                format.Xml = () => {
                    model.id = "awesome_xml";
                    return Xml(model);
                };
                format["yml"] = () => {
                    model.id = "awesome_yml";
                    return View("awesome.yml", model);
                };
                format.Html = () => {
                    Flash.Success("Nothing to see here");
                    return RedirectToAction("show");
                };
            });
        }
    }

    public class Awesome {
        public string message { get; set; }
        public string id { get; set; }
        public string date { get; set; }
    }
}
