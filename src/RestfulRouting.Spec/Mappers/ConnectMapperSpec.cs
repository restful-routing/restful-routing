using System.Web.Mvc;
using Machine.Specifications;
using RestfulRouting.Mappers;
using RestfulRouting.Spec.TestObjects;
using MvcContrib.TestHelper;

namespace RestfulRouting.Spec.Mappers
{
    public class TestRouteSet : RouteSet
    {
        public override void Map(Mapper map)
        {
            map.Map("test").To<PostsController>(x => x.Index());
        }
    }

    public class connect_mapper : base_context
    {
        Because of = () => new ConnectMapper<TestRouteSet>(null).RegisterRoutes(routes);

        It maps_normally = () => "~/test".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index());
    }

    public class connect_mapper_with_path : base_context
    {
        Because of = () => new ConnectMapper<TestRouteSet>("testing").RegisterRoutes(routes);

        It maps_normally = () => "~/testing/test".WithMethod(HttpVerbs.Get).ShouldMapTo<PostsController>(x => x.Index());
    }
}
