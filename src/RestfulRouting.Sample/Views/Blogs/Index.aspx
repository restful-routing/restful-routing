<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Blog>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Blogs</h2>
	
	<%= Html.ActionLink("New", "New") %>
	
	<table>
		<thead>
			<tr>
				<th>Id</th><th>Author</th><th></th>
			</tr>
		</thead>
		<tbody>
			<% foreach (var blog in Model) { %>
			<tr>
				<td><%= blog.Id %></td>
				<td><%= blog.Author %></td>
				<td>
					<a href="<%= Url.Action("index", "posts", new { blogId = blog.Id }) %>">view posts</a>
					<%= Html.ActionLink("show", "show", new { id = blog.Id }) %>
					<%= Html.ActionLink("edit", "edit", new { id = blog.Id }) %>
					<%= Html.ActionLink("delete", "delete", new { id = blog.Id }) %>
				</td>
			</tr>
			<% } %>
		</tbody>
	</table>
	
</asp:Content>
