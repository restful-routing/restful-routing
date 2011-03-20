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
                DefaultIs(result);
            }

            public void DefaultIs(Func<ActionResult> result)
            {
                Default = result;
            }

            public Func<ActionResult> this[string id] 
            {
                get { return Results[id]; }
                set { Results[id] = value; }
            }
        }

        /// <summary>
        /// Will return the correct action result based on the format. Format will be read from HttpContext.Current.
        /// </summary>
        /// <param name="formatCollection">Your rules for the specific formats.</param>
        /// <returns>An action result for the format.</returns>
        public static ActionResult Do(Action<FormatCollection> formatCollection)
        {
            var format = GetFormat();
            return With(format, formatCollection);
        }

        /// <summary>
        /// Will return the correct action result based on the format. Specify the format yourself.
        /// </summary>
        /// <param name="format">The format from where you want to read it.</param>
        /// <param name="formatCollection">Your rules for the specific formats.</param>
        /// <returns>An action result for the format.</returns>
        public static ActionResult With(string format, Action<FormatCollection> formatCollection)
        {
            if (format == null) throw new ArgumentNullException("format");

            var collection = new FormatCollection();

            // populate collection
            formatCollection.Invoke(collection);

            if (collection.Default == null)
                throw new ArgumentException(
                    "Default format handler required. Please call Any() or DefaultIs() on format collection.");

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