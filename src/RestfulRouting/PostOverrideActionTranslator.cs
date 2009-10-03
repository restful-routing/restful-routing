using System.Web.Routing;

namespace RestfulRouting
{
	public class PostOverrideActionTranslator
	{
		public void TranslateFormOverrideToAction(RequestContext requestContext)
		{
			var formMethod = requestContext.HttpContext.Request.Form["_method"];

			if (formMethod == null)
				return;

			switch (formMethod.ToUpperInvariant())
			{
				case "PUT":
					requestContext.RouteData.Values["action"] = "update";
					break;
				case "DELETE":
					requestContext.RouteData.Values["action"] = "destroy";
					break;
				default:
					break;
			}
		}
	}
}