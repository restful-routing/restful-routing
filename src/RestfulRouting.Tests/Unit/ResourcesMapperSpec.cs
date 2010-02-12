using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Unit
{
	[Subject(typeof(ResourcesMapper))]
	public class base_context
	{
		protected static ResourcesMapper mapper;
		protected static Route route;

		Establish context = () => mapper = new ResourcesMapper(new RouteNames(), "test", "blogs");

		protected static ICollection<string> HttpMethods()
		{
			return ((HttpMethodConstraint)route.Constraints["httpMethod"]).AllowedMethods;
		}

		protected static void ShouldOnlyAllow(string httpVerb)
		{
			HttpMethods().ShouldContainOnly(httpVerb);
		}

		//It should_set_controller_to_blogs = () => route.Defaults["controller"].ShouldBe("blogs");
	}

	public class when_mapping_index_route : base_context
	{
		Because of = () => route = mapper.IndexRoute();
	
		It should_set_action_to_index = () => route.Defaults["action"].ShouldBe("index");
		
		It should_constrain_http_methods_to_get = () => ShouldOnlyAllow("GET");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs");
	}

	public class when_mapping_show_route : base_context
	{
		Because of = () => route = mapper.ShowRoute();

		It should_set_default_action_to_show = () => route.Defaults["action"].ShouldBe("show");

		It should_constrain_to_get = () => ShouldOnlyAllow("GET");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs/{id}");
	}

	public class when_mapping_edit_route : base_context
	{
		Because of = () => route = mapper.EditRoute();

		It should_set_default_action_to_show = () => route.Defaults["action"].ShouldBe("edit");

		It should_constrain_to_get = () => ShouldOnlyAllow("GET");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs/{id}/edit");
	}

	public class when_mapping_create_route : base_context
	{
		Because of = () => route = mapper.CreateRoute();

		It should_set_default_action_to_create = () => route.Defaults["action"].ShouldBe("create");

		It should_constrain_to_post = () => ShouldOnlyAllow("POST");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs");
	}

	public class when_mapping_update_route : base_context
	{
		Because of = () => route = mapper.UpdateRoute();

		It should_set_default_action_to_update = () => route.Defaults["action"].ShouldBe("update");

		It should_constrain_to_put = () => ShouldOnlyAllow("PUT");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs/{id}");
	}

	public class when_mapping_destroy_route : base_context
	{
		Because of = () => route = mapper.DestroyRoute();

		It should_set_default_action_to_destroy = () => route.Defaults["action"].ShouldBe("destroy");

		It should_constrain_to_put = () => ShouldOnlyAllow("DELETE");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs/{id}");
	}

	public class when_mapping_new_route : base_context
	{
		Because of = () => route = mapper.NewRoute();

		It should_set_default_action_to_destroy = () => route.Defaults["action"].ShouldBe("new");

		It should_constrain_to_put = () => ShouldOnlyAllow("GET");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs/new");
	}

	public class when_mapping_members : base_context
	{
		Because of = () => route = mapper.MemberRoute("up", HttpVerbs.Post);

		It should_set_default_action_to_destroy = () => route.Defaults["action"].ShouldBe("up");

		It should_constrain_to_put = () => ShouldOnlyAllow("POST");

		It should_set_the_url = () => route.Url.ShouldBe("test/blogs/{id}/up");
	}
}