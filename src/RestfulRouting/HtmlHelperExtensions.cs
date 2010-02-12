using System.Web.Mvc;

namespace RestfulRouting
{
    public static class HtmlHelperExtensions
    {
        public static string PutOverrideTag(this HtmlHelper html)
        {
            return "<input type=\"hidden\" name=\"_method\" value=\"put\" />";
        }

        public static string DeleteOverrideTag(this HtmlHelper html)
        {
            return "<input type=\"hidden\" name=\"_method\" value=\"delete\" />";
        }
    }
}


