using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace RestfulRouting.Format
{
    public class FormatResult : ActionResult
    {
        public static MimeTypeList MimeTypes;
        static FormatResult()
        {
            MimeTypes = new MimeTypeList();
            MimeTypes.InitializeDefaults();
        }

        Action<FormatCollection> _format;
        FormatCollection _formatCollection = new FormatCollection();

        public FormatResult(Action<FormatCollection> format)
        {
            _format = format;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            _format(_formatCollection);

            var result = GetResult(_formatCollection, context.RouteData.Values, context.HttpContext.Request.AcceptTypes, context.HttpContext.Response);
            
            result.ExecuteResult(context);
        }

        public static ActionResult GetResult(FormatCollection formatCollection, RouteValueDictionary routeValues, string[] acceptTypes, HttpResponseBase response)
        {
            var format = GetFormat(formatCollection, routeValues, acceptTypes);
            SetContentType(response, format);
            return GetActionResult(formatCollection, format);
        }

        private static ActionResult GetActionResult(FormatCollection formatCollection, string format)
        {
            if (!formatCollection.Any())
            {
                if (formatCollection.Default != null)
                    return formatCollection.Default;
                return new HttpStatusCodeResult(406);
            }

            if (string.IsNullOrEmpty(format))
            {
                if (formatCollection.Default != null)
                    return formatCollection.Default;
                if (formatCollection["html"] != null)
                    return formatCollection["html"].Invoke();
                return new HttpStatusCodeResult(406);
            }

            if (!formatCollection.ContainsKey(format))
            {
                if (formatCollection.Default != null)
                    return formatCollection.Default;
                return new HttpNotFoundResult();
            }

            return formatCollection[format].Invoke();
        }

        public static void SetContentType(HttpResponseBase response, string format)
        {
            var mimeType = MimeTypes.LookupByFormat(format);
            if (mimeType != null)
                response.ContentType = mimeType.Type;
        }

        public static string GetFormat(FormatCollection formatCollection, RouteValueDictionary routeValues, string[] acceptTypes)
        {
            var format = "html"; // default to html if no format extension is specified
            if (routeValues["format"] == null)
            {
                if (acceptTypes.Any())
                {
                    format = ChooseFormatFromAcceptTypes(formatCollection, acceptTypes);
                }
            }
            else
            {
                format = routeValues["format"].ToString();
            }

            return format;
        }

        public static string ChooseFormatFromAcceptTypes(FormatCollection formatCollection, string[] acceptTypes)
        {
            foreach (var mimeType in MimeTypes.Parse(acceptTypes))
            {
                foreach (var key in formatCollection.Keys)
                {
                    if (mimeType.Format == key)
                        return key;
                }
            }
            return null;
        }
    }
}