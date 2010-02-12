using System.Web.Mvc;

namespace RestfulRouting
{
	public class RestfulRoutingViewEngine : WebFormViewEngine
	{
		public RestfulRoutingViewEngine()
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

			AreaPartialViewLocationFormats = AreaPartialViewLocationFormats;
		}
	}
}