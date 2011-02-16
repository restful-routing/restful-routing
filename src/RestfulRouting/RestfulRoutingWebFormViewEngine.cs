using System.Web.Mvc;

namespace RestfulRouting
{
    public class RestfulRoutingWebFormViewEngine : WebFormViewEngine
    {
        public RestfulRoutingWebFormViewEngine()
        {
            AreaMasterLocationFormats = new[] {
                                                  "~/Views/{2}/{1}/{0}.master",
                                                  "~/Views/{2}/Shared/{0}.master",
                                              };

            AreaViewLocationFormats = new[] {
                                                "~/Views/{2}/{1}/{0}.aspx",
                                                "~/Views/{2}/{1}/{0}.ascx",
                                                "~/Views/{2}/Shared/{0}.aspx",
                                                "~/Views/{2}/Shared/{0}.ascx",
                                            };

            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }
    }
}