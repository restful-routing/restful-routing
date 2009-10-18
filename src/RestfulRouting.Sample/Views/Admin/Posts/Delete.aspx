<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Post</h2>

	<p>
		Are you sure?
	</p>

	
	
	<%= Html.RestfulDeleteFormFor(Model, Model.Blog) %>
		
		<div class="group">
			<input type="submit" value="Delete" />
		</div>
	</form>
	
</asp:Content>
