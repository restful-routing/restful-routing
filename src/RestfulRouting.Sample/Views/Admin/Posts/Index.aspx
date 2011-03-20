<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Post>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Posts Admin</h2>
    
    <%= Html.ActionLink("New", "New") %>
    
    <table>
        <thead>
            <tr>
                <th>Id</th><th>Title</th><th></th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var post in Model) { %>
            <tr>
                <td><%= post.Id %></td>
                <td><%= post.Title %></td>
                <td>
                    <%= Html.ActionLink("show", "show", new { id = post.Id }) %>
                    <%= Html.ActionLink("edit", "edit", new { id = post.Id }) %>
                    <%= Html.ActionLink("delete", "delete", new { id = post.Id }) %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
    
</asp:Content>
