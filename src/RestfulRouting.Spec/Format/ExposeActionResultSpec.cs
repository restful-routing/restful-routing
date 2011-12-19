using System.Web.Mvc;
using Machine.Specifications;
using RestfulRouting.Format;
using RestfulRouting.Format.ResultExposal;

namespace RestfulRouting.Spec.Format
{
    [Subject(typeof(FormatResult))]
    public class When_ExposeActionResult_method_called_on_format_result
    {
        static FormatResult formatResult;

        static ActionResult returnedHtmlActionResult;

        static ActionResult returnedJsonActionResult;

        Establish context = () => {
            formatResult = new FormatResult(format => {
                format.Html = () => new ViewResult();
                format.Json = () => new JsonResult();
            });
        };

        Because of = () => {
            returnedHtmlActionResult = formatResult.ExposeActionResult().Html();
            returnedJsonActionResult = formatResult.ExposeActionResult().Json();
        };

        It should_return_json_result = () => returnedHtmlActionResult.ShouldBeOfType<ViewResult>();

        It should_return_html_result = () => returnedHtmlActionResult.ShouldBeOfType<JsonResult>();
    }
}