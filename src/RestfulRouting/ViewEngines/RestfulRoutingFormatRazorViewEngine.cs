using System.Collections.Generic;
using System.Web.Hosting;
using System.Web.Mvc;

namespace RestfulRouting.ViewEngines
{
    public class RestfulRoutingFormatRazorViewEngine : RestfulRoutingRazorViewEngine {

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            ViewEngineResult result = null;
            
            if (controllerContext.HttpContext.Items.Contains("format"))
            {
                var format = controllerContext.HttpContext.Items["format"];
                var view = viewName + "." + format;

                // we don't want to add a format to the master if there is no master being requested
                var master = string.IsNullOrWhiteSpace(masterName) ? masterName : masterName + "." + format;

                result = base.FindView(controllerContext, view, master, useCache);
            }

            return result ?? base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}
