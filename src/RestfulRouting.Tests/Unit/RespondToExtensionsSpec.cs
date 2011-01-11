using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting;
using Rhino.Mocks;

namespace RespondToExtensionsSpec
{
    [Subject(typeof(RespondToExtensions))]
    public abstract class  base_context
    {
        protected static HttpRequestBase _httpRequestBase;
        protected static RequestContext _requestContext;
        protected static RouteData _routeData;

        private Establish context = () =>
                                        {

                                            var builder = new TestControllerBuilder();
                                            _routeData = new RouteData();
                                            _routeData.Values.Add("format", "xml");
                                            var requestContext = new RequestContext(builder.HttpContext, _routeData);
                                            requestContext.HttpContext.Response.Stub(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(new Func<string, string>(s => s)).Repeat.Any();

                                            var viewData = new ViewDataDictionary();

                                            var viewContext = MockRepository.GenerateStub<ViewContext>();
                                            viewContext.RequestContext = requestContext;
                                            viewContext.ViewData = viewData;

                                            var viewDataContainer = MockRepository.GenerateStub<IViewDataContainer>();
                                            viewDataContainer.ViewData = viewData;

                                        };
    }

    public class when_modifying_a_json_request :  base_context
    {
        private static JsonResult _result = new JsonResult();

         Because of = () => _result = _result.AllowGet();

         It should_return_a_json_result_with_allow_get_set_to_allow_get = () =>
             _result.JsonRequestBehavior.ShouldEqual(JsonRequestBehavior.AllowGet);
    }

    public class when_accessing_format_from_route_data : base_context
    {
        private static string _format;

        Because of = () => _format = _routeData.Format();

        It should_return_the_format_with_a_value_of_xml = () => _format.ShouldBe("xml");
    }

}
