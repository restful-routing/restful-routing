using System;
using System.Web.Mvc;
using RestfulRouting.Mappings;

namespace RestfulRouting
{
	public class MemberAction
	{
		private Mapping _mapping;

		public MemberAction(Mapping mapping)
		{
			this._mapping = mapping;
		}

		public void Get(string action)
		{
			_mapping.Members[action.ToLowerInvariant()] = new[] { HttpVerbs.Get };
		}

		public void Post(string action)
		{
			_mapping.Members[action.ToLowerInvariant()] = new[] { HttpVerbs.Post };
		}

		public void Put(string action)
		{
			_mapping.Members[action.ToLowerInvariant()] = new[] { HttpVerbs.Put };
		}

		public void Delete(string action)
		{
			_mapping.Members[action.ToLowerInvariant()] = new[] { HttpVerbs.Delete };
		}

		public void Head(string action)
		{
			_mapping.Members[action.ToLowerInvariant()] = new[] { HttpVerbs.Head };
		}
	}
}
