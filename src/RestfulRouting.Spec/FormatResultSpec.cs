using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace RestfulRouting.Spec
{
    public class format_result_base
    {
        private Establish context = () =>
                                        {
                                            _formatCollection = new FormatCollection();
                                            _routeValues = new RouteValueDictionary(new {format = "html"});
                                            _acceptTypes = new string[] {};
                                        };

        Because of = () => _result = FormatResult.GetResult(_formatCollection, _routeValues, _acceptTypes);

        protected static FormatCollection _formatCollection;
        protected static ActionResult _result;
        protected static RouteValueDictionary _routeValues;
        protected static string[] _acceptTypes;
    }

    public class format_result_with_matching_format : format_result_base
    {
        Establish context = () => _formatCollection["html"] = new ContentResult();

        It returns_the_associated_action_result = () => _result.ShouldEqual(_formatCollection["html"]);
    }

    public class format_result_with_unknown_format : format_result_base
    {
        It returns_not_found_result = () => _result.ShouldBeOfType<HttpNotFoundResult>();
    }

    public class format_result_with_default_result_and_unknown_format : format_result_base
    {
        Establish context = () => _formatCollection.Default = new ViewResult();

        It returns_the_default_result = () => _result.ShouldEqual(_formatCollection.Default);
    }

    public class format_result_with_no_format_and_accept_types_application_xml : format_result_base
    {
        Establish context = () =>
                                {
                                    _acceptTypes = new[] {"application/json"};
                                    _formatCollection.Json = new JsonResult();
                                };

        // pending
        It returns_json = () => _result.ShouldEqual(_formatCollection.Json);
    }
}