using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;
using RestfulRouting.Mappings;
using Rhino.Mocks;

namespace RestfulRouting.Tests.Unit
{
	public class member_action_context : base_context
	{
		protected static MemberAction _memberAction;
		protected static Dictionary<string, HttpVerbs[]> _actionsAndMethods;

		Establish context = () =>
		                    	{
		                    		_actionsAndMethods = new Dictionary<string, HttpVerbs[]>();
		                    		var mapping = MockRepository.GenerateMock<Mapping>();
		                    		mapping.Stub(x => x.Members).Return(_actionsAndMethods);
		                    		_memberAction = new MemberAction(mapping);
		                    	};
	}

	public class get : member_action_context
	{
		Because of = () => _memberAction.Get("actionname");

		It should_map_get_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Get });
	}

	public class post : member_action_context
	{
		Because of = () => _memberAction.Post("actionname");

		It should_map_post_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Post });
	}

	public class put : member_action_context
	{
		Because of = () => _memberAction.Put("actionname");

		It should_map_post_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Put });
	}

	public class delete : member_action_context
	{
		Because of = () => _memberAction.Delete("actionname");

		It should_map_delete_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Delete });
	}

	public class head : member_action_context
	{
		Because of = () => _memberAction.Head("actionname");

		It should_map_delete_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Head });
	}
}
