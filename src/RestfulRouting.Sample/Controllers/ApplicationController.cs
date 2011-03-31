using System;
using System.Web.Mvc;
using RestfulRouting.Format;

namespace RestfulRouting.Sample.Controllers
{
    public class ApplicationController : Controller
    {
        protected ActionResult RespondTo(Action<FormatCollection> format)
        {
            return new FormatResult(format);
        }
    }
}