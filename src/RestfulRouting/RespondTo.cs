using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
    /// <summary>
    /// return Responder.Do(format => {
    /// format.Any(() => View(SampleData.Blogs());
    /// format["json"] = () => Json(SampleData.Blogs());
    /// });
    /// </summary>
    public static class Responder
    {
        public class FormatCollection
        {
            public Func<ActionResult> Default { get; set; }
            public IDictionary<string, Func<ActionResult>> Results { get; set; }

            public FormatCollection()
            {
                Results = new Dictionary<string, Func<ActionResult>>();
            }

            public void Any(Func<ActionResult> result)
            {
                IsDefault(result);
            }

            public void IsDefault(Func<ActionResult> result)
            {
                Default = result;
            }

            public Func<ActionResult> this[string id]      // indexer
            {
                get { return Results[id]; }
                set { Results[id] = value; }
            }
        }

        public static ActionResult Do(Action<FormatCollection> formatCollection)
        {
            var collection = new FormatCollection();
            var format = GetFormat();

            // populate collection
            formatCollection.Invoke(collection);

            if (string.IsNullOrEmpty(format) || !collection.Results.ContainsKey(format)) {
                return collection.Default.Invoke();
            }

            return collection[format].Invoke();
        }

        private static string GetFormat()
        {
            try {
                var httpContext = new HttpContextWrapper(HttpContext.Current);
                var routeValues = RouteTable.Routes.GetRouteData(httpContext).Values;
                return routeValues.ContainsKey("format") ? routeValues["format"].ToString() : string.Empty;
            }
            catch (Exception e) {
                return string.Empty;
            }
        }
    }
}