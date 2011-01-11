using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration
{
    [Behaviors]
    public class FormatSessionsResource
    {
        It should_map_get_show = () => "~/session.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionsController>(x => x.Show());

        It should_map_get_new = () => "~/session/new.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionsController>(x => x.New());

        It should_map_post_create = () => "~/session.xml".WithMethod(HttpVerbs.Post).ShouldMapTo<SessionsController>(x => x.Create());

        It should_map_get_edit = () => "~/session/edit.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<SessionsController>(x => x.Edit());

        It should_map_put_update = () => "~/session.xml".WithMethod(HttpVerbs.Put).ShouldMapTo<SessionsController>(x => x.Update());

        It should_map_delete_destroy = () => "~/session.xml".WithMethod(HttpVerbs.Delete).ShouldMapTo<SessionsController>(x => x.Destroy());


    }

    public class when_mapping_a_resource_with_format : base_context
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

    public class when_mapping_a_nested_resource_with_format : base_context
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

    public class when_mapping_multiple_nested_resource_with_format : base_context
    {
        public class SessionRoutes : RouteSet
        {
            public SessionRoutes()
            {
                Resource<SessionsController>(() =>
                {
                    Resource<AvatarsController>();
                    Resource<ProfilesController>();
                });
            }
        }

        Because of = () => new SessionRoutes().RegisterRoutes(routes);

        Behaves_like<SessionsResource> a_sessions_resource;

        Behaves_like<NestedSessionsAvatarsResource> a_nested_avatars_resource;

        Behaves_like<NestedSessionsProfilesResource> a_nested_profiles_resource;
    }

    public class when_mapping_a_resources_with_a_resource_with_format : base_context
    {
        public class Routes : RouteSet
        {
            public Routes()
            {
                Resource<SessionsController>(() =>
                {
                    Resources<ImagesController>();
                });
            }
        }

        Because of = () => new Routes().RegisterRoutes(routes);

        Behaves_like<ResourcesNestedWithinResource> a_nested_resources;
    }

    [Behaviors]
    public class FormatNestedSessionsAvatarsResource
    {
        It should_map_get_show = () => "~/session/avatar.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<AvatarsController>(x => x.Show());

        It should_map_get_new = () => "~/session/avatar/new.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<AvatarsController>(x => x.New());

        It should_map_post_create = () => "~/session/avatar.xml".WithMethod(HttpVerbs.Post).ShouldMapTo<AvatarsController>(x => x.Create());

        It should_map_get_edit = () => "~/session/avatar/edit.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<AvatarsController>(x => x.Edit());

        It should_map_put_update = () => "~/session/avatar.xml".WithMethod(HttpVerbs.Put).ShouldMapTo<AvatarsController>(x => x.Update());

        It should_map_delete_destroy = () => "~/session/avatar.xml".WithMethod(HttpVerbs.Delete).ShouldMapTo<AvatarsController>(x => x.Destroy());

    }

    [Behaviors]
    public class FormatNestedSessionsProfilesResource
    {
        It should_map_get_show = () => "~/session/profile.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ProfilesController>(x => x.Show());

        It should_map_get_new = () => "~/session/profile/new.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ProfilesController>(x => x.New());

        It should_map_post_create = () => "~/session/profile.xml".WithMethod(HttpVerbs.Post).ShouldMapTo<ProfilesController>(x => x.Create());

        It should_map_get_edit = () => "~/session/profile/edit.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ProfilesController>(x => x.Edit());

        It should_map_put_update = () => "~/session/profile.xml".WithMethod(HttpVerbs.Put).ShouldMapTo<ProfilesController>(x => x.Update());

        It should_map_delete_destroy = () => "~/session/profile.xml".WithMethod(HttpVerbs.Delete).ShouldMapTo<ProfilesController>(x => x.Destroy());
     
    }

    [Behaviors]
    public class FormatResourcesNestedWithinResource
    {
        It should_map_get_index = () => "~/session/images.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ImagesController>(x => x.Index());

        It should_map_get_show = () => "~/session/images/1.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ImagesController>(x => x.Show(1));

        It should_map_get_new = () => "~/session/images/new.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ImagesController>(x => x.New());

        It should_map_post_create = () => "~/session/images.xml".WithMethod(HttpVerbs.Post).ShouldMapTo<ImagesController>(x => x.Create());

        It should_map_get_edit = () => "~/session/images/1/edit.xml".WithMethod(HttpVerbs.Get).ShouldMapTo<ImagesController>(x => x.Edit(1));

        It should_map_put_update = () => "~/session/images/1.xml".WithMethod(HttpVerbs.Put).ShouldMapTo<ImagesController>(x => x.Update(1));

        It should_map_delete_destroy = () => "~/session/images/1.xml".WithMethod(HttpVerbs.Delete).ShouldMapTo<ImagesController>(x => x.Destroy(1));

    }
}
