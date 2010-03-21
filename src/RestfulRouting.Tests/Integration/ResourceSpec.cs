using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
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
}
