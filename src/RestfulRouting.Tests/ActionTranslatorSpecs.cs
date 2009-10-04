using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using NUnit.Framework;
using RestfulRouting;
using RestfulRouting.Tests;
using Rhino.Mocks;
using NUnit.Should;

namespace ActionTranslatorSpecs
{
	public abstract class base_context : Spec
	{
		protected PostOverrideActionTranslator _translator;
		protected HttpRequestBase _httpRequestBase;
		protected RequestContext _requestContext;
		protected RouteData _routeData;

		protected override void given()
		{
			_translator = new PostOverrideActionTranslator();

			_routeData = new RouteData();

			var httpContextBase = MockRepository.GenerateMock<HttpContextBase>();
			_httpRequestBase = MockRepository.GenerateMock<HttpRequestBase>();

			httpContextBase.Stub(x => x.Request).Return(_httpRequestBase);
			_requestContext = new RequestContext(httpContextBase, _routeData);
		}

		protected void the_form_contains(NameValueCollection form)
		{
			_httpRequestBase.Stub(x => x.Form).Return(form);			
		}
	}

	[TestFixture]
	public class when_a_put_overload_exists_in_the_form : base_context
	{
		protected override void given()
		{
			base.given();
			the_form_contains(new NameValueCollection { { "_method", "put" } });
		}

		protected override void when()
		{
			_translator.TranslateFormOverrideToAction(_requestContext);
		}

		[Test]
		public void should_set_the_action_to_update()
		{
			_routeData.Values["action"].ShouldBe("update");
		}
	}

	[TestFixture]
	public class when_a_delete_overload_exists_in_the_form : base_context
	{
		protected override void given()
		{
			base.given();
			the_form_contains(new NameValueCollection { { "_method", "delete" } });
		}

		protected override void when()
		{
			_translator.TranslateFormOverrideToAction(_requestContext);
		}

		[Test]
		public void should_set_the_action_to_destroy()
		{
			_routeData.Values["action"].ShouldBe("destroy");
		}
	}

	[TestFixture]
	public class when_there_is_no_overload_in_the_form : base_context
	{
		protected override void given()
		{
			base.given();
			_httpRequestBase.Stub(x => x.Form).Return(new NameValueCollection());			
		}

		protected override void when()
		{
			_translator.TranslateFormOverrideToAction(_requestContext);
		}

		[Test]
		public void should_not_set_any_route_values()
		{
			_routeData.Values.Count.ShouldBe(0);
		}
	}
}
