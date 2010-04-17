using System.Collections.Generic;
using System.Web.Mvc;

namespace RestfulRouting
{
	public class AdditionalAction
	{
		private IDictionary<string, HttpVerbs[]> _actions;

		public AdditionalAction(IDictionary<string, HttpVerbs[]> actions)
		{
			this._actions = actions;
		}

		public void Get(string action)
		{
			_actions[action.ToLowerInvariant()] = new[] { HttpVerbs.Get };
		}

		public void Post(string action)
		{
			_actions[action.ToLowerInvariant()] = new[] { HttpVerbs.Post };
		}

		public void Put(string action)
		{
			_actions[action.ToLowerInvariant()] = new[] { HttpVerbs.Put };
		}

		public void Delete(string action)
		{
			_actions[action.ToLowerInvariant()] = new[] { HttpVerbs.Delete };
		}

		public void Head(string action)
		{
			_actions[action.ToLowerInvariant()] = new[] { HttpVerbs.Head };
		}
	}
}
