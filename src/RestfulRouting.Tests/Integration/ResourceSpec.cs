using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
	[Behaviors]
	public class SessionsResource
	{
		It should_map_get_show = () => "~/session".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionsController>(x => x.Show());

		It should_map_get_new = () => "~/session/new".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionsController>(x => x.New());

		It should_map_post_create = () => "~/session".WithMethod(HttpVerbs.Post).ShouldMapTo<SessionsController>(x => x.Create());

		It should_map_get_edit = () => "~/session/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionsController>(x => x.Edit());

		It should_map_put_update = () => "~/session".WithMethod(HttpVerbs.Put).ShouldMapTo<SessionsController>(x => x.Update());

		It should_map_delete_destroy = () => "~/session".WithMethod(HttpVerbs.Delete).ShouldMapTo<SessionsController>(x => x.Destroy());


		It should_generate_show = () => OutBoundUrl.Of<SessionsController>(x => x.Show()).ShouldMapToUrl("/session");

		It should_generate_new = () => OutBoundUrl.Of<SessionsController>(x => x.New()).ShouldMapToUrl("/session/new");

		It should_generate_create = () => OutBoundUrl.Of<SessionsController>(x => x.Create()).ShouldMapToUrl("/session");

		It should_generate_edit = () => OutBoundUrl.Of<SessionsController>(x => x.Edit()).ShouldMapToUrl("/session/edit");

		It should_generate_update = () => OutBoundUrl.Of<SessionsController>(x => x.Update()).ShouldMapToUrl("/session");

		It should_generate_destroy = () => OutBoundUrl.Of<SessionsController>(x => x.Destroy()).ShouldMapToUrl("/session");
	}

	public class when_mapping_a_resource : base_context
	{
		public class SessionRoutes : RouteSet
		{
			public SessionRoutes()
			{
				Resource<SessionsController>();
			}
		}

		Because of = () => new SessionRoutes().RegisterRoutes(routes);

		Behaves_like<SessionsResource> a_sessions_resource;
	}

	public class when_mapping_a_nested_resource : base_context
	{
		public class SessionRoutes : RouteSet
		{
			public SessionRoutes()
			{
				Resource<SessionsController>(() => Resource<AvatarsController>());
			}
		}

		Because of = () => new SessionRoutes().RegisterRoutes(routes);

		Behaves_like<SessionsResource> a_sessions_resource;

		Behaves_like<NestedSessionsAvatarsResource> a_nested_avatars_resource;
	}

	[Behaviors]
	public class NestedSessionsAvatarsResource
	{
		It should_map_get_show = () => "~/session/avatar".WithMethod(HttpVerbs.Get).ShouldMapTo<AvatarsController>(x => x.Show());

		It should_map_get_new = () => "~/session/avatar/new".WithMethod(HttpVerbs.Get).ShouldMapTo<AvatarsController>(x => x.New());

		It should_map_post_create = () => "~/session/avatar".WithMethod(HttpVerbs.Post).ShouldMapTo<AvatarsController>(x => x.Create());

		It should_map_get_edit = () => "~/session/avatar/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<AvatarsController>(x => x.Edit());

		It should_map_put_update = () => "~/session/avatar".WithMethod(HttpVerbs.Put).ShouldMapTo<AvatarsController>(x => x.Update());

		It should_map_delete_destroy = () => "~/session/avatar".WithMethod(HttpVerbs.Delete).ShouldMapTo<AvatarsController>(x => x.Destroy());


		It should_generate_show = () => OutBoundUrl.Of<AvatarsController>(x => x.Show()).ShouldMapToUrl("/session/avatar");

		It should_generate_new = () => OutBoundUrl.Of<AvatarsController>(x => x.New()).ShouldMapToUrl("/session/avatar/new");

		It should_generate_create = () => OutBoundUrl.Of<AvatarsController>(x => x.Create()).ShouldMapToUrl("/session/avatar");

		It should_generate_edit = () => OutBoundUrl.Of<AvatarsController>(x => x.Edit()).ShouldMapToUrl("/session/avatar/edit");

		It should_generate_update = () => OutBoundUrl.Of<AvatarsController>(x => x.Update()).ShouldMapToUrl("/session/avatar");

		It should_generate_destroy = () => OutBoundUrl.Of<AvatarsController>(x => x.Destroy()).ShouldMapToUrl("/session/avatar");
	}
}
