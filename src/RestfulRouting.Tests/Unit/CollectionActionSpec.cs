using System.Collections.Generic;
using System.Web.Mvc;
using Machine.Specifications;

namespace RestfulRouting.Tests.Unit
{
	public class additional_action_context : base_context
	{
		protected static AdditionalAction AdditionalAction;
		protected static Dictionary<string, HttpVerbs[]> _actionsAndMethods;

		Establish context = () =>
		                    	{
		                    		_actionsAndMethods = new Dictionary<string, HttpVerbs[]>();
									AdditionalAction = new AdditionalAction(_actionsAndMethods);
		                    	};
	}

	public class collection_get : additional_action_context
	{
		Because of = () => AdditionalAction.Get("actionname");

		It should_map_get_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Get });
	}

	public class collection_post : additional_action_context
	{
		Because of = () => AdditionalAction.Post("actionname");

		It should_map_post_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Post });
	}

	public class collection_put : additional_action_context
	{
		Because of = () => AdditionalAction.Put("actionname");

		It should_map_post_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Put });
	}

	public class collection_delete : additional_action_context
	{
		Because of = () => AdditionalAction.Delete("actionname");

		It should_map_delete_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Delete });
	}

	public class collection_head : additional_action_context
	{
		Because of = () => AdditionalAction.Head("actionname");

		It should_map_delete_actionname = () => _actionsAndMethods["actionname"].ShouldEqual(new[] { HttpVerbs.Head });
	}

	public class collection_multiple : additional_action_context
	{
		Because of = () =>
		{
			AdditionalAction.Get("actionname");
			AdditionalAction.Post("actionname");
		};

		It should_map_get_and_post = () => _actionsAndMethods["actionname"].ShouldContain(HttpVerbs.Get, HttpVerbs.Post);
	}

}
