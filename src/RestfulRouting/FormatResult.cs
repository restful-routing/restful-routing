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
            TrySetContentType(context);            
            result.ExecuteResult(context);
        }

        /// <summary>
        /// Tries to set the content type of the response.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void TrySetContentType(ControllerContext context)
        {
            try
            {
                if (context.RouteData.Values.ContainsKey("format"))
                {
                    string format = context.RouteData.Values["format"].ToString();
                    var response = context.HttpContext.Response;

                    switch (format)
                    {
                        case "xml":
                            response.ContentType = "text/xml";
                            break;
                        case "json":
                            response.ContentType = "application/json";
                            break;
                        case "js":
                            response.ContentType = "application/javascript";
                            break;
                        case "css":
                            response.ContentType = "text/css";
                            break;
                        case "html":
                            response.ContentType = "text/html";
                            break;
                        case "csv":
                            response.ContentType = "text/csv";
                            break;
                        case "txt" :
                            response.ContentType = "text/plain";
                            break;
                        case "cmd":
                            response.ContentType = "text/cmd";
                            break;
                        default:
                            break;
                    }
                }
                
            }
            catch (Exception e)
            {
                // fail gracefully
            }
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