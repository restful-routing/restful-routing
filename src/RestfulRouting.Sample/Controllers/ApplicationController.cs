using System;
using System.Web.Mvc;
using RestfulRouting.Format;

namespace RestfulRouting.Sample.Controllers
{
    public abstract class ApplicationController : Controller
    {
        protected ActionResult RespondTo(Action<FormatCollection> format)
        {
            return new FormatResult(format);
        }

        protected ActionResult FormatView(object model, string contentType)
        {
            return new FormatView(model, contentType);
        }
    }
}