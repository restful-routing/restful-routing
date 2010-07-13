using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappings;
using RestfulRouting.Tests.Integration.Contexts;

namespace RestfulRouting.Tests.Unit.Mappings
{
    [Subject(typeof(StandardMapping))]
    public class StandardMappingSpec : base_context
    {
        static StandardMapping mapping;

        Establish context = () => mapping = new StandardMapping("");

        Because of = () => mapping.Map("redirects/{id}").To<BlogsController>(x => x.Show(1)).Constrain("slug", @"\w+").GetOnly().Default("id", -1);

        It should_map_to_show = () => mapping.Route.Defaults["action"].ShouldEqual("show");

        It should_set_the_url = () => mapping.Route.Url.ShouldEqual("redirects/{id}");

        It should_set_the_constraint = () => mapping.Route.Constraints["slug"].ShouldEqual(@"\w+");

		It should_be_get_only = () =>
		                        	{
		                        		mapping.Route.Constraints["httpMethod"].ShouldBeOfType<HttpMethodConstraint>();
		                        		var constraint = (HttpMethodConstraint) mapping.Route.Constraints["httpMethod"];
										constraint.AllowedMethods.ShouldContain("GET");
		                        	};

        It should_default_to_minus_1_id = () => mapping.Route.Defaults["id"].ShouldEqual(-1);
    }
}