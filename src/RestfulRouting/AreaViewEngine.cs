using System.Web.Mvc;

namespace RestfulRouting
{
	/// <summary>
    /// Modified version of Phil Haack's sample: http://haacked.com/archive/2008/11/04/areas-in-aspnetmvc.aspx
    /// </summary>
    public class AreaViewEngine : WebFormViewEngine
    {
        public AreaViewEngine()
        {
            ViewLocationFormats = new[] { 
                "~/{0}.aspx",
                "~/{0}.ascx",
                "~/Views/{1}/{0}.aspx",
                "~/Views/{1}/{0}.ascx",
                "~/Views/Shared/{0}.aspx",
                "~/Views/Shared/{0}.ascx",
            };

            MasterLocationFormats = new[] {
                "~/{0}.master",
                "~/Shared/{0}.master",
                "~/Views/{1}/{0}.master",
                "~/Views/Shared/{0}.master",
            };

            PartialViewLocationFormats = ViewLocationFormats;
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
		{
            ViewEngineResult areaResult;

			if (controllerContext.RouteData.Values.ContainsKey("_area") && controllerContext.RouteData.Values["_area"] != null)
			{
                var areaPartialName = FormatViewName(controllerContext, partialViewName);
                areaResult = base.FindPartialView(controllerContext, areaPartialName, useCache);

                if (areaResult != null && areaResult.View != null)
				{
                    return areaResult;
                }
                
                var sharedAreaPartialName = FormatSharedViewName(controllerContext, partialViewName);
                areaResult = base.FindPartialView(controllerContext, sharedAreaPartialName, useCache);
                
                if (areaResult != null && areaResult.View != null)
				{
                    return areaResult;
                }
            }

            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
            ViewEngineResult areaResult;

			if (controllerContext.RouteData.Values.ContainsKey("_area") && controllerContext.RouteData.Values["_area"] != null)
			{
                var areaViewName = FormatViewName(controllerContext, viewName);
                areaResult = base.FindView(controllerContext, areaViewName, masterName, useCache);

                if (areaResult != null && areaResult.View != null)
				{
                    return areaResult;
                }
                
                var sharedAreaViewName = FormatSharedViewName(controllerContext, viewName);
                areaResult = base.FindView(controllerContext, sharedAreaViewName, masterName, useCache);
                
                if (areaResult != null && areaResult.View != null)
				{
                    return areaResult;
                }
            }

            return base.FindView(controllerContext, viewName, masterName, useCache);
        }

        private static string FormatViewName(ControllerContext controllerContext, string viewName)
		{
            var controllerName = controllerContext.RouteData.GetRequiredString("controller");
			var area = controllerContext.RouteData.Values["_area"].ToString();
            return "Views/" + area + "/" + controllerName + "/" + viewName;
        }

        private static string FormatSharedViewName(ControllerContext controllerContext, string viewName)
		{
			var area = controllerContext.RouteData.Values["_area"].ToString();
            return "Views/" + area + "/Shared/" + viewName;
        }
    }
}
