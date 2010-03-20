<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<RouteDebugViewModel>" %>
<%@ Import Namespace="RestfulRouting.Sample.Controllers"%>
<%@ Import Namespace="RestfulRouting"%>

<!DOCTYPE HTML>
<html lang="en-GB">
<head>
	<meta charset="UTF-8">
	<title>Restful Routing</title>
	<style>
	  * { margin: 0; padding: 0; }
	  body {
	    font: 16px Helvetica,Arial,sans-serif;
	  }
    #container {
      background: #fff;
      padding: 50px;
    }
    h1 {
      font-size: 48px;
    }
    h1, h2 {
      margin: 0 0 6px 0;
      text-transform: uppercase;
    }
    p {
      margin: 0 0 20px 0;
    }
    p, h1, h2, h3, li { line-height: 1.6em; }
    a {
      color: #0E718F;
    }
    a:hover {
      text-decoration: none;
      color: #fff;
      background-color: #0E718F;
    }
    td, th {
      padding: 2px 4px;
      border-bottom: #666;
      text-align: left;
    }
	</style>
</head>
<body>
  <div id="container">
    <h1>Routes</h1>
    <table>
      <thead>
        <tr>
			<th>HttpMethods</th>
			<th>Area</th>
			<th>Path</th>
			<th>Endpoint</th>
			<th>Namespaces</th>
        </tr>
      </thead>
      <tbody>
        <% foreach (var routeInfo in Model.RouteInfos) { %>
        <tr>
		  <td><%= routeInfo.HttpMethod %></td>
		  <td><%= routeInfo.Area %></td>
		  <td><a href="<%= routeInfo.Path %>" target="_blank"><%= routeInfo.Path %></a></td>
		  <td><%= routeInfo.Endpoint %></td>
		  <td><%= routeInfo.Namespaces %></td>
        </tr>
        <% } %>
      </tbody>
    </table>
  </div>
</body>
</html>