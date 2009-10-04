using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class PostOverrideRouteHandler : MvcRouteHandler
	{
		protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			new PostOverrideActionTranslator().TranslateFormOverrideToAction(requestContext);

			return base.GetHttpHandler(requestContext);
		}
	}
}