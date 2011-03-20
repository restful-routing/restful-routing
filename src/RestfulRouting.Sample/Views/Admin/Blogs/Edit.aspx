<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>
    
    <form action="<%= Url.Action("Update", new { id = Model.Id }) %>" method="post">
        <%= Html.PutOverrideTag() %>
        
        <% Html.RenderPartial("Form"); %> 
        
        <div class="group">
            <input type="submit" value="Update" />
        </div>
    </form>
    
</asp:Content>
