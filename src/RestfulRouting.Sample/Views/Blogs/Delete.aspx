<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <p>
        Are you sure?
    </p>

    
    <form action="<%= Url.Action("Destroy", new { id = Model.Id }) %>" method="post">
        <%= Html.DeleteOverrideTag() %>
    
        
        <div class="group">
            <input type="submit" value="Delete" />
        </div>
    </form>
    
</asp:Content>
