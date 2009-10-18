<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

	<p>
		Are you sure?
	</p>

	
	<%= Html.RestfulDeleteFormFor(Model) %>
	
		
		<div class="group">
			<input type="submit" value="Delete" />
		</div>
	</form>
	
</asp:Content>
