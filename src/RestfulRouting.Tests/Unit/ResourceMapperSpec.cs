using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;

namespace RestfulRouting.Tests.Unit
{
    [Subject(typeof(ResourcesMapper))]
    public class base_resource_context
    {
        protected static ResourceMapper mapper;
        protected static Route route;

        Establish context = () =>
                                {
                                    mapper = new ResourceMapper(new RouteNames(), "", new MvcRouteHandler());
                                    mapper.SetResourceAs("session");
                                };

        protected static ICollection<string> HttpMethods()
        {
            return ((HttpMethodConstraint)route.Constraints["httpMethod"]).AllowedMethods;
        }

        protected static void ShouldOnlyAllow(string httpVerb)
        {
            HttpMethods().ShouldContainOnly(httpVerb);
        }
    }

    public class when_mapping_singular_show_route : base_resource_context
    {
        Because of = () => route = mapper.ShowRoute();

        It should_set_action_to_index = () => route.Defaults["action"].ShouldEqual("show");

        It should_constrain_http_methods_to_get = () => ShouldOnlyAllow("GET");

        It should_set_the_url = () => route.Url.ShouldEqual("session");
    }

    public class when_mapping_singular_new_route : base_resource_context
    {
        Because of = () => route = mapper.NewRoute();

        It should_set_default_action_to_destroy = () => route.Defaults["action"].ShouldEqual("new");

        It should_constrain_to_put = () => ShouldOnlyAllow("GET");

        It should_set_the_url = () => route.Url.ShouldEqual("session/new");
    }

    public class when_mapping_singular_edit_route : base_resource_context
    {
        Because of = () => route = mapper.EditRoute();

        It should_set_default_action_to_show = () => route.Defaults["action"].ShouldEqual("edit");

        It should_constrain_to_get = () => ShouldOnlyAllow("GET");

        It should_set_the_url = () => route.Url.ShouldEqual("session/edit");
    }

    public class when_mapping_singular_create_route : base_resource_context
    {
        Because of = () => route = mapper.CreateRoute();

        It should_set_default_action_to_create = () => route.Defaults["action"].ShouldEqual("create");

        It should_constrain_to_post = () => ShouldOnlyAllow("POST");

        It should_set_the_url = () => route.Url.ShouldEqual("session");
    }

    public class when_mapping_singular_update_route : base_resource_context
    {
        Because of = () => route = mapper.UpdateRoute();

        It should_set_default_action_to_update = () => route.Defaults["action"].ShouldEqual("update");

        It should_constrain_to_put = () => ShouldOnlyAllow("PUT");

        It should_set_the_url = () => route.Url.ShouldEqual("session");
    }

    public class when_mapping_singular_destroy_route : base_resource_context
    {
        Because of = () => route = mapper.DestroyRoute();

        It should_set_default_action_to_destroy = () => route.Defaults["action"].ShouldEqual("destroy");

        It should_constrain_to_put = () => ShouldOnlyAllow("DELETE");

        It should_set_the_url = () => route.Url.ShouldEqual("session");
    }

    public class when_mapping_singular_members : base_resource_context
    {
        Because of = () => route = mapper.MemberRoute("up");

        It should_set_default_action_to_up = () => route.Defaults["action"].ShouldEqual("up");

        It should_constrain_to_get_by_default = () => ShouldOnlyAllow("GET");

        It should_set_the_url = () => route.Url.ShouldEqual("session/up");
    }
}
