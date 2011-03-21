using System;
using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappers;
using RestfulRouting.Spec.TestObjects;
using System.Linq;

namespace RestfulRouting.Spec.Mappers
{
    public class area_mapper : base_context
    {
        Because of = () => new AreaMapper("test", typeof(PostsController).Namespace, x => x.Resources<PostsController>()).RegisterRoutes(routes);

        static Func<RouteBase, RouteValueDictionary> DataTokens = (RouteBase x) => ((Route)x).DataTokens;

        It constrains_the_namespace = () => routes.ShouldEachConformTo(x => ((string[])DataTokens(x)["namespaces"]).Contains(typeof(PostsController).Namespace));

        It sets_the_area = () => routes.ShouldEachConformTo(x => DataTokens(x)["area"].ToString() == "test");

        It sets_namespace_fallback = () => routes.ShouldEachConformTo(x => (bool)DataTokens(x)["UseNamespaceFallback"] == false);
    }
}
