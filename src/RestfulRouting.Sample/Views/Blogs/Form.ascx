<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Blog>" %>

<p>
	<label for="Author">Author</label>
	<%= Html.TextBox("Author") %>
	<%= Html.ValidationMessage("Author") %>
</p>