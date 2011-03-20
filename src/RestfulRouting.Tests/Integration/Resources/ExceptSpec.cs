using System.Web.Mvc;
using Machine.Specifications;
using MvcContrib.TestHelper;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Integration.Resources
{
    public class ExceptSpec : base_context
    {
        public class BlogArea : RouteSet
        {
            public BlogArea()
            {
                Resources<BlogsController>(() => Except("index", "update"));
            }
        }

        private Because of = () => new BlogArea().RegisterRoutes(routes);

        It should_not_map_get_index = () => "~/blogs".WithMethod(HttpVerbs.Get).ShouldBeNull();

        It should_not_map_put_update = () => "~/blogs/1".WithMethod(HttpVerbs.Put).ShouldBeNull();

        It should_map_create = () => "~/blogs".WithMethod(HttpVerbs.Post).ShouldMapTo<BlogsController>(x => x.Create());

        It should_map_new = () => "~/blogs/new".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.New());

        It should_map_edit = () => "~/blogs/1/edit".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Edit(1));

        It should_map_show = () => "~/blogs/1".WithMethod(HttpVerbs.Get).ShouldMapTo<BlogsController>(x => x.Show(1));

        It should_map_destroy = () => "~/blogs/1".WithMethod(HttpVerbs.Delete).ShouldMapTo<BlogsController>(x => x.Destroy(1));
    }
}