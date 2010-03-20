using System.Web.Routing;
using Machine.Specifications;
using RestfulRouting.Mappings;
using Rhino.Mocks;

namespace RestfulRouting.Tests.Unit.Mappings
{
    public class TestMapping : Mapping
    {
        public TestMapping()
        {
        }
    }

    public class when_configuring_a_mapping
    {
        protected static Mapping mapping;

        Establish context = () => mapping = new TestMapping();
    }

    [Subject(typeof(Mapping))]
    public class when_configuring_resource_name : when_configuring_a_mapping
    {
        Because of = () => mapping.As("test");

        It should_set_the_mapped_name = () => mapping.MappedName.ShouldEqual("test");

        It should_set_the_base_path = () => mapping.BasePath().ShouldEqual("test");
    }

    public class when_configuring_only : when_configuring_a_mapping
    {
        Because of = () => mapping.Only("index");

        It should_include_index = () => mapping.IncludesAction("index").ShouldBeTrue();

        It should_not_include_create = () => mapping.IncludesAction("create").ShouldBeFalse();
    }

    public class when_configuring_except : when_configuring_a_mapping
    {
        Because of = () => mapping.Except("index");

        It should_not_include_index = () => mapping.IncludesAction("index").ShouldBeFalse();

        It should_include_create = () => mapping.IncludesAction("create").ShouldBeTrue();
    }

    public class when_adding_to_routes : when_configuring_a_mapping
    {
        static RouteCollection routes;

        private static Mapping _subMapping;

        Establish context = () =>
                                {
                                    routes = new RouteCollection();
                                    _subMapping = MockRepository.GenerateMock<Mapping>();
                                    mapping.AddSubMapping(_subMapping);
                                };

        Because of = () => mapping.AddRoutesTo(routes);

        It should_call_map_on_all_its_sub_mappings = () => _subMapping.AssertWasCalled(x => x.AddRoutesTo(routes));
    }
}