using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.TestHelper;
using NUnit.Framework;
using RestfulRouting.Html;
using RestfulRouting.Tests;
using Rhino.Mocks;
using NUnit.Should;

namespace HtmlExtensionsSpecs
{
	public abstract class base_context : Spec
	{
		protected HttpRequestBase _httpRequestBase;
		protected RequestContext _requestContext;
		protected RouteData _routeData;
		protected string _form;
		protected HtmlHelper _htmlHelper;

		protected override void given()
		{
			var builder = new TestControllerBuilder();
			var requestContext = new RequestContext(builder.HttpContext, new RouteData());
			requestContext.HttpContext.Response.Stub(x => x.ApplyAppPathModifier(null)).IgnoreArguments().Do(new Func<string, string>(s => s)).Repeat.Any();

			var viewContext = MockRepository.GenerateStub<ViewContext>();
			viewContext.RequestContext = requestContext;

			_htmlHelper = new HtmlHelper(viewContext, MockRepository.GenerateStub<IViewDataContainer>());
		}
	}

	[TestFixture]
	public class when_generating_a_put_override : base_context
	{
		private string _tag;

		protected override void when()
		{
			_tag = _htmlHelper.PutOverrideTag();
		}

		[Test]
		public void should_return_a_hidden_field_with_method_put()
		{
			_tag.ShouldBe("<input type=\"hidden\" name=\"_method\" value=\"put\" />");
		}
	}

	[TestFixture]
	public class when_generating_a_delete_override : base_context
	{
		private string _tag;

		protected override void when()
		{
			_tag = _htmlHelper.DeleteOverrideTag();
		}

		[Test]
		public void should_return_a_hidden_field_with_method_delete()
		{
			_tag.ShouldBe("<input type=\"hidden\" name=\"_method\" value=\"delete\" />");
		}
	}
}
