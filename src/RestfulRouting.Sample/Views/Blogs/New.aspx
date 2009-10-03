<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>New</h2>
	
	<form action="<%= Url.Action("Create") %>" method="post">
		
		<% Html.RenderPartial("Form"); %>
		
		<div class="group">
			<input type="submit" value="Create" />
		</div>
	</form>
	
	<% Html.RenderPartial("Routes"); %>
	
</asp:Content>
