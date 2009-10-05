using System.Web.Mvc;
using System.Web.Routing;

namespace RestfulRouting
{
	public class FormHelper
	{
		private UrlHelper _url;

		public FormHelper(UrlHelper url)
		{
			_url = url;
		}

		public string DeleteFormFor(object entity, params object[] associatedEntities)
		{
			var id = IdFor(entity);

			var dictionary = PopulateDictionaryWithAssociatedEntityIds(associatedEntities);

			dictionary.Add("id", id);

			var url = _url.Action("destroy", LowerCasePluralName(entity), dictionary);

			var form = FormTag(url) + OverrideTag("delete");

			return form;
		}

		public string FormFor(object entity, params object[] associatedEntities)
		{
			var id = IdFor(entity);
			
			string form;

			var dictionary = PopulateDictionaryWithAssociatedEntityIds(associatedEntities);

			if (string.IsNullOrEmpty(id) || id == "0" || id == "-1")
			{
				var url = _url.Action("create", LowerCasePluralName(entity), dictionary);

				form = FormTag(url);
			}
			else
			{
				dictionary.Add("id", id);

				var url = _url.Action("update", LowerCasePluralName(entity), dictionary);

				form = FormTag(url) + OverrideTag("put");
			}

			return form;
		}

		private static RouteValueDictionary PopulateDictionaryWithAssociatedEntityIds(object[] entities)
		{
			var dictionary = new RouteValueDictionary();
			foreach (var associatedEntity in entities)
			{
				dictionary.Add(associatedEntity.GetType().Name.ToLowerInvariant() + "Id", IdFor(associatedEntity));
			}
			return dictionary;
		}

		private static string FormTag(string url)
		{
			return string.Format("<form method=\"post\" action=\"{0}\">", url);
		}

		private static string OverrideTag(string method)
		{
			return string.Format("<input type=\"hidden\" name=\"_method\" value=\"{0}\" />", method);
		}

		private static string LowerCasePluralName(object entity)
		{
			var name = entity.GetType().Name.ToLowerInvariant();
			return Inflector.Net.Inflector.Pluralize(name);
		}

		private static string IdFor(object entity)
		{
			// TODO make this configurable
			var idProperty = entity.GetType().GetProperty("Id");

			var id = idProperty.GetValue(entity, null);

			return id.ToString();
		}
	}
}
