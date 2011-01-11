using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Machine.Specifications;
using RestfulRouting;

namespace RespondToSpec
{
    [Subject(typeof(RespondTo))]
    public abstract class base_context
    {
        protected static Func<string, ActionResult> _response;

        private Establish context = () =>
                                        {
                                            _response =  (format) => {
                                                            return RespondTo.Do(format)
                                                                      .Default(() => new ContentResult())
                                                                      .Format("json", new JsonResult())
                                                                  .End();
                                                          };
                                        };
    }

    public class when_passing_in_a_format :base_context
    {
        private static string _format;
        
        Because of = () => _format = "json";
        
        private It should_return_a_json_result = () => _response.Invoke(_format).ShouldBe(typeof (JsonResult));
    }

    public class when_passing_in_a_null_or_empty_format : base_context
    {
        private static string _format;

        Because of = () => _format = "";

        private It should_return_the_default_result = () => _response.Invoke(_format).ShouldBe(typeof(ContentResult));
    }
}
