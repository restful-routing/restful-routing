using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestfulRouting
{
    /// <summary>
    /// Correlates to the respond to block in rails, gives you the analogous feature in 
    /// an ASP.Net MVC application
    /// </summary>
    /// <remarks>
    /// return RespondTo.Do("xml")
    ///                     .Default(() => new ContentResult())
    ///                     .Format("json", () => new JsonResult())
    ///                     .Format("xml", () => new)
    ///        .End();
    /// </remarks>
    public static class RespondTo
    {
        /// <summary>
        /// Does the specified format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static DefaultRespond Do(string format)
        {
            return new DefaultRespond(format);
        }

        #region Nested type: DefaultRespond

        public class DefaultRespond
        {
            private readonly string _format;

            public DefaultRespond(string format)
            {
                _format = format.Trim().ToLower();
            }

            private Func<ActionResult> DefaultActionResult { get; set; }

            public string CurrentFormat
            {
                get { return _format; }
            }

            /// <summary>
            /// The default action result that will be returned if format is not found
            /// or the format is null or empty
            /// </summary>
            /// <param name="defaultActionResult">The default action result.</param>
            /// <returns></returns>
            public RespondWith Default(Func<ActionResult> defaultActionResult)
            {
                if (defaultActionResult == null) throw new ArgumentNullException("defaultActionResult");
                DefaultActionResult = defaultActionResult;
                return new RespondWith(_format, this);
            }

            /// <summary>
            /// The default action result that will be returned if format is not found
            /// or the format is null or empty
            /// </summary>
            /// <param name="defaultActionResult">The default action result.</param>
            /// <returns></returns>
            public RespondWith Default(ActionResult defaultActionResult)
            {
                if (defaultActionResult == null) throw new ArgumentNullException("defaultActionResult");
                return Default(() => defaultActionResult);
            }

            #region Nested type: RespondWith

            public class RespondWith
            {
                private readonly DefaultRespond _defaultRespond;
                private readonly string _format;
                private readonly Dictionary<string, Func<ActionResult>> _results;

                public RespondWith(string format, DefaultRespond defaultRespond)
                {
                    if (defaultRespond == null) throw new ArgumentNullException("defaultRespond");

                    _format = format;
                    _results = new Dictionary<string, Func<ActionResult>>();
                    _defaultRespond = defaultRespond;
                }

                /// <summary>
                /// Specifies the returned action result for a format. If the format key already exists, it will be overriden.
                /// </summary>
                /// <param name="formatKey">The format key.</param>
                /// <param name="actionResult">The action result.</param>
                /// <returns></returns>
                public RespondWith Format(string formatKey, Func<ActionResult> actionResult)
                {
                    string key = formatKey.Trim().ToLower();

                    if (_results.ContainsKey(key))
                        _results[key] = actionResult;
                    else
                        _results.Add(key, actionResult);

                    return this;
                }

                /// <summary>
                /// Specifies the returned action result for a format. If the format key already exists, it will be overriden.
                /// </summary>
                /// <param name="formatKey">The format key.</param>
                /// <param name="actionResult">The action result.</param>
                /// <returns></returns>
                public RespondWith Format(string formatKey, ActionResult actionResult)
                {
                    return Format(formatKey, () => actionResult);
                }

                /// <summary>
                /// returns the action result based on the format and the current set of action results.
                /// </summary>
                /// <returns></returns>
                public ActionResult End()
                {
                    if (string.IsNullOrEmpty(_format) || !_results.ContainsKey(_format))
                    {
                        return _defaultRespond.DefaultActionResult.Invoke();
                    }
                    return _results[_format].Invoke();
                }
            }

            #endregion
        }

        #endregion
    }
}