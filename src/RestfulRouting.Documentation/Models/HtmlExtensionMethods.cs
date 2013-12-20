using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestfulRouting.Documentation.Models
{
    public static class HtmlExtensionMethods
    {
        public static string GetVersion(this HtmlHelper helper)
        {
            var version = typeof (RouteSet).Assembly.GetName().Version;
            return string.Format("({0})", version);
        }
    }
}