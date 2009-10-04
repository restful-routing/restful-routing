<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>New</h2>
	
	<%= Html.RestfulFormFor(Model) %>
		
		<% Html.RenderPartial("Form"); %>
		
		<div class="group">
			<input type="submit" value="Create" />
		</div>
	</form>
	
</asp:Content>
