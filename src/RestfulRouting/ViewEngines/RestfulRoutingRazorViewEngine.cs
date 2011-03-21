using System.Web.Mvc;

namespace RestfulRouting
{
    public class RestfulRoutingRazorViewEngine : RazorViewEngine
    {
        public RestfulRoutingRazorViewEngine()
        {
            AreaMasterLocationFormats = new[] {
                                                  "~/Views/{2}/{1}/{0}.cshtml",
                                                  "~/Views/{2}/{1}/{0}.vbhtml",
                                                  "~/Views/{2}/Shared/{0}.vbhtml",
                                                  "~/Views/{2}/Shared/{0}.cshtml",
                                              };

            AreaViewLocationFormats = new[] {
                                                "~/Views/{2}/{1}/{0}.cshtml",
                                                "~/Views/{2}/{1}/{0}.vbhtml",
                                                "~/Views/{2}/Shared/{0}.cshtml",
                                                "~/Views/{2}/Shared/{0}.vbhtml",
                                            };

            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }
    }
}