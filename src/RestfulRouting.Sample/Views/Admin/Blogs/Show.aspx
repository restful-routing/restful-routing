<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Show</h2>

	<p>
		Id: <%= Model.Id %>
	</p>
	<p>
		Author: <%= Model.Author %>
	</p>
	<p>
		<%= Html.ActionLink("edit", "edit", new { id = Model.Id }) %>
		<%= Html.ActionLink("delete", "delete", new { id = Model.Id }) %>
	</p>
	
</asp:Content>
