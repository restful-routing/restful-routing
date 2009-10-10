using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace RestfulRouting
{
	public class RouteConfiguration : ICloneable
	{
		static RouteConfiguration()
		{
			Default = () => new RouteConfiguration();
		}

		public RouteConfiguration()
		{
			InitializeDefaults();
		}

		private void InitializeDefaults()
		{
			Index = "index";
			Show = "show";
			New = "new";
			Create = "create";
			Edit = "edit";
			Update = "update";
			Delete = "delete";
			Destroy = "destroy";

			MemberRoutes = new Dictionary<string, HttpVerbs[]>();
			CollectionRoutes = new Dictionary<string, HttpVerbs[]>();
		}

		public string IdValidationRegEx { get; set; }

		public string As { get; set; }

		public string PathPrefix { get; set; }

		public bool Shallow { get; set; }

		public static Func<RouteConfiguration> Default { get; set; }

		object ICloneable.Clone()
		{
			return Clone();
		}

		public RouteConfiguration Clone()
		{
			return (RouteConfiguration)MemberwiseClone();
		}

		public string Index { get; set; }

		public string Show { get; set; }

		public string Create { get; set; }

		public string Update { get; set; }

		public string Destroy { get; set; }

		public string New { get; set; }

		public string Edit { get; set; }

		public string Delete { get; set; }

		private static string GetActionName<TController>(Expression<Func<TController, object>> actionExpression)
		{
			var body = ((MethodCallExpression)actionExpression.Body);
			var actionName = body.Method.Name.ToLowerInvariant();
			return actionName;
		}

		public void AddMemberRoute<TController>(Expression<Func<TController, object>> actionExpression, params HttpVerbs[] verbs)
		{
			if (verbs.Count() == 0)
				verbs = new[] { HttpVerbs.Get };
			AddMemberRoute(GetActionName(actionExpression), verbs);
		}

		public void AddCollectionRoute<TController>(Expression<Func<TController, object>> actionExpression, params HttpVerbs[] verbs)
		{
			if (verbs.Count() == 0)
				verbs = new[] { HttpVerbs.Get };
			AddCollectionRoute(GetActionName(actionExpression), verbs);
		}

		public void AddMemberRoute(string name, params HttpVerbs[] verbs)
		{
			MemberRoutes[name] = verbs;
		}

		public void AddCollectionRoute(string name, params HttpVerbs[] verbs)
		{
			CollectionRoutes[name] = verbs;
		}

		public IDictionary<string, HttpVerbs[]> MemberRoutes { get; private set; }

		public IDictionary<string, HttpVerbs[]> CollectionRoutes { get; private set; }

		public string[] GetCollectionVerbArray(string member)
		{
			return CollectionRoutes[member].Select(x => x.ToString().ToUpperInvariant()).ToArray();
		}

		public string[] GetMemberVerbArray(string member)
		{
			return MemberRoutes[member].Select(x => x.ToString().ToUpperInvariant()).ToArray();
		}
	}
}
