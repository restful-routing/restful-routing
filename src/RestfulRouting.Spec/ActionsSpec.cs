using Machine.Specifications;

namespace RestfulRouting.Spec
{
    public class actions_having_constants_for_c_sharp_spec
    {
        Establish context = () => {};
        Because of = () => {};

        It should_have_constant_to_index = () => Actions.Index.ShouldBeTheSameAs("index");
        It should_have_constant_to_show = () => Actions.Show.ShouldBeTheSameAs("show");
        It should_have_constant_to_new = () => Actions.New.ShouldBeTheSameAs("new");
        It should_have_constant_to_create = () => Actions.Create.ShouldBeTheSameAs("create");
        It should_have_constant_to_edit = () => Actions.Edit.ShouldBeTheSameAs("edit");
        It should_have_constant_to_update = () => Actions.Update.ShouldBeTheSameAs("update");
        It should_have_constant_to_destroy = () => Actions.Destroy.ShouldBeTheSameAs("destroy");
    }
}
