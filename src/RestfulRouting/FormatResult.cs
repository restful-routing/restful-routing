using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
    public class FormatResult : ActionResult
    {
        Action<FormatCollection> _format;
        FormatCollection _formatCollection = new FormatCollection();

        public FormatResult(Action<FormatCollection> format)
        {
            _format = format;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            _format(_formatCollection);
            var result = GetResult(_formatCollection, context.RouteData.Values, context.HttpContext.Request.AcceptTypes);
            result.ExecuteResult(context);
        }

        public static ActionResult GetResult(FormatCollection formatCollection, RouteValueDictionary routeValues, string[] acceptTypes)
        {
            var format = "html"; // use html if no format extension is specified
            if (routeValues["format"] == null)
            {
                // lookup in accept types => format dictionary
            }
            else
            {
                format = routeValues["format"].ToString();
            }

            if (!formatCollection.ContainsKey(format))
            {
                if (formatCollection.Default != null)
                    return formatCollection.Default;
                return new HttpNotFoundResult();
            }

            return formatCollection[format].Invoke();
        }
    }

    public class FormatCollection : Dictionary<string, Func<ActionResult>>
    {
        public ActionResult Default { get; set; }

        public Func<ActionResult> Html
        {
            get { return this["html"]; }
            set { this["html"] = value; }
        }

        public Func<ActionResult> Xml
        {
            get { return this["xml"]; }
            set { this["xml"] = value; }
        }

        public Func<ActionResult> Json
        {
            get { return this["json"]; }
            set { this["json"] = value; }
        }

        public Func<ActionResult> Js
        {
            get { return this["js"]; }
            set { this["js"] = value; }
        }
    }
}