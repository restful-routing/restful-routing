using System;
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
			var form = "";
			var dictionary = new RouteValueDictionary { };
			
			AddAssociatedEntitiesToDictionary(associatedEntities, dictionary);

			dictionary.Add("id", id);

			var url = _url.Action("destroy", LowerCasePluralName(entity), dictionary);

			form = string.Format("<form method=\"post\" action=\"{0}\">{1}", url, OverrideTag("delete"));

			return form;
		}

		public string FormFor(object entity, params object[] associatedEntities)
		{
			var id = IdFor(entity);
			var form = "";
			var dictionary = new RouteValueDictionary {};
			AddAssociatedEntitiesToDictionary(associatedEntities, dictionary);

			if (string.IsNullOrEmpty(id) || id == "0" || id == "-1")
			{
				var url = _url.Action("create", LowerCasePluralName(entity), dictionary);
				form = string.Format("<form method=\"post\" action=\"{0}\">", url);
			}
			else
			{
				dictionary.Add("id", id);
				var url = _url.Action("update", LowerCasePluralName(entity), dictionary);
				form = string.Format("<form method=\"post\" action=\"{0}\">{1}", url, OverrideTag("put"));
			}

			return form;
		}

		private void AddAssociatedEntitiesToDictionary(object[] entities, RouteValueDictionary dictionary)
		{
			foreach (var associatedEntity in entities)
			{
				dictionary.Add(associatedEntity.GetType().Name.ToLowerInvariant() + "Id", IdFor(associatedEntity));
			}
		}

		private string OverrideTag(string method)
		{
			return string.Format("<input type=\"hidden\" name=\"_method\" value=\"{0}\" />", method);
		}

		private string LowerCasePluralName(object entity)
		{
			var name = entity.GetType().Name.ToLowerInvariant();
			return Inflector.Net.Inflector.Pluralize(name);
		}

		private string IdFor(object entity)
		{
			// TODO make this configurable
			var idProperty = entity.GetType().GetProperty("Id");

			var id = idProperty.GetValue(entity, null);
			if (id == null)
				return null;
			return id.ToString();
		}
	}
}
