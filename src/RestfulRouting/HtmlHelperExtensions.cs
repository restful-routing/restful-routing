using System.Web.Mvc;

namespace RestfulRouting.Html
{
	public static class HtmlHelperExtensions
	{
		public static string RestfulFormFor(this HtmlHelper html, object entity, params object[] associatedEntities)
		{
			return new FormHelper(new UrlHelper(html.ViewContext.RequestContext)).FormFor(entity, associatedEntities);
		}

		public static string RestfulDeleteFormFor(this HtmlHelper html, object entity, params object[] associatedEntities)
		{
			return new FormHelper(new UrlHelper(html.ViewContext.RequestContext)).DeleteFormFor(entity, associatedEntities);
		}
	}
}
