using System.Web.Mvc;

namespace RestfulRouting
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString PutOverrideTag(this HtmlHelper html)
        {
            return MvcHtmlString.Create("<input type=\"hidden\" name=\"_method\" value=\"put\" />");
        }

        public static MvcHtmlString DeleteOverrideTag(this HtmlHelper html)
        {
            return MvcHtmlString.Create("<input type=\"hidden\" name=\"_method\" value=\"delete\" />");
        }
    }
}


