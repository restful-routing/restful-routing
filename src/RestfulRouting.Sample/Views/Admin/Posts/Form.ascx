<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Post>" %>

<p>
	<label for="Author">Title</label>
	<%= Html.TextBox("Title") %>
	<%= Html.ValidationMessage("Title") %>
</p>
<p>
	<label for="Slug">Slug</label>
	<%= Html.TextBox("Slug") %>
	<%= Html.ValidationMessage("Slug") %>
</p>
<p>
	<label for="Author">Body</label>
	<%= Html.TextArea("Body") %>
	<%= Html.ValidationMessage("Body") %>
</p>