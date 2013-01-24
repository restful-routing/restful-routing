using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappers;
using RestfulRouting.Spec.TestObjects;
using MvcContrib.TestHelper;

namespace RestfulRouting.Spec.Mappers
{
    public class TestRouteSet : RouteSet
    {
        public override void Map(IMapper map)
        {
            map.Path("test").To<PostsController>(x => x.Index());
            map.Connect<AnotherRouteSet>("api", new[] { typeof(PostsController).Namespace });
        }
    }

    public class AnotherRouteSet : RouteSet
    {
        public override void Map(IMapper map)
        {
            // doesn't matter what controller, we 
            map.Resource<PostsController>(p => p.Only("show"));
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

    public class connect_with_namespace : base_context
    {
        private Because of = () => new ConnectMapper<TestRouteSet>("testing").RegisterRoutes(routes);

        private It should_have_proper_namespace = () =>
        {
            var route = routes.Last() as Route;
            route.DataTokens.ContainsKey("Namespaces").ShouldBeTrue();
            route.DataTokens["Namespaces"].As<IEnumerable<string>>().First().ShouldBe(typeof(PostsController).Namespace);
        };
    }
}
