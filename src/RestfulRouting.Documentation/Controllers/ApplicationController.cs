using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using RestfulRouting.Format;

namespace RestfulRouting.Documentation.Controllers {
    public abstract class ApplicationController : Controller {
        protected ActionResult RespondTo(Action<FormatCollection> format) {
            return new FormatResult(format);
        }

        public ActionResult Xml<T>(T model) {
            var ser = new XmlSerializer(typeof(T));
            using (var memStream = new MemoryStream()) {
                using (var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8)) {
                    xmlWriter.Namespaces = true;
                    ser.Serialize(xmlWriter, model);
                    var xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                    return Content(xml, "application/xml");
                }
            }
        }
    }
}
