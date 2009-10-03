<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

	<p>
		<strong>Routes</strong><br />
		Index: <%= HttpUtility.HtmlEncode(Url.Action("index"))%><br />
		New: <%= HttpUtility.HtmlEncode(Url.Action("new"))%><br />
		Create: <%= HttpUtility.HtmlEncode(Url.Action("create"))%><br />
		Edit: <%= HttpUtility.HtmlEncode(Url.Action("edit", new { id = 1 }))%><br />
		Update: <%= HttpUtility.HtmlEncode(Url.Action("update", new { id = 1 }))%><br />
		Delete: <%= HttpUtility.HtmlEncode(Url.Action("delete", new { id = 1 }))%><br />
		Destroy: <%= HttpUtility.HtmlEncode(Url.Action("destroy", new { id = 1 }))%>
	</p>