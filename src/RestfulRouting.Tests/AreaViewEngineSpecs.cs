using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Rhino.Mocks;
using MvcContrib.TestHelper;

namespace RestfulRouting.Tests
{
	[TestFixture(Description = "Hard to test...")]
	public class AreaViewEngineSpecs : Spec
	{
		private AreaViewEngine _engine;
		private ViewEngineResult _view;
		private BlogsController _blogsController;
		private ControllerContext _controllerContext;

		private static HttpContextBase FakeHttpContext(string url)
		{
			var request = MockRepository.GenerateStub<HttpRequestBase>();
			request.Stub(x => x.AppRelativeCurrentExecutionFilePath).Return(url).Repeat.Any();
			request.Stub(x => x.PathInfo).Return(string.Empty).Repeat.Any();

			var context = MockRepository.GenerateStub<HttpContextBase>();
			context.Stub(x => x.Request).Return(request).Repeat.Any();

			return context;
		}

		protected override void given()
		{
			_engine = new AreaViewEngine();
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new AreaViewEngine());

			var map = new RestfulRouteMapper(RouteTable.Routes);
			map.Namespace("admin", new[] { typeof(BlogsController).Namespace }, m =>
			                                                                    	{
																						m.Resources<BlogsController>();
			                                                                    	});
			_blogsController = new BlogsController();
			new TestControllerBuilder().InitializeController(_blogsController);

			_controllerContext = new ControllerContext(FakeHttpContext("~/admin/blogs"), "~/admin/blogs".WithMethod(HttpVerbs.Get), _blogsController);
		}

		protected override void when()
		{
			_view = _engine.FindView(_controllerContext, "index", "", true);
		}

		[Test, Explicit("ViewEngine catches a general exception in the FileExists method, I think the problem is to do with the VirtualPathProvider/HostingEnvironment")]
		public void should_find_view()
		{
			//var stream = VirtualPathProvider.OpenFile("~/views/admin/blogs/index.aspx"); // fails
			//stream.CanRead.ShouldBeTrue();
			//stream.Close();

			_view.View.ShouldNotBeNull("Could not find view");
		}
	}
}
