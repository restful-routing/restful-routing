<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

	<p>
		<strong>Routes</strong>
	</p>
	<table>
		<thead>
			<tr>
				<th>Action</th><th>Url Helper</th><th>Output url</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>Index</td>
				<td><%= HttpUtility.HtmlEncode("Url.Action(\"index\")") %></td>
				<td><%= HttpUtility.HtmlEncode(Url.Action("index"))%></td>
			</tr>
			<tr>
				<td>New</td>
				<td><%= HttpUtility.HtmlEncode("Url.Action(\"new\")") %></td>
				<td><%= HttpUtility.HtmlEncode(Url.Action("new"))%></td>
			</tr>
			<tr>
				<td>Create</td>
				<td><%= HttpUtility.HtmlEncode("Url.Action(\"create\")") %></td>
				<td><%= HttpUtility.HtmlEncode(Url.Action("create"))%></td>
			</tr>
			<tr>
				<td>Edit</td>
				<td><%= HttpUtility.HtmlEncode("Url.Action(\"edit\", new { id = 1 })") %></td>
				<td><%= HttpUtility.HtmlEncode(Url.Action("edit", new { id = 1 }))%></td>
			</tr>
			<tr>
				<td>Update</td>
				<td><%= HttpUtility.HtmlEncode("Url.Action(\"update\"), new { id = 1 }")%></td>
				<td><%= HttpUtility.HtmlEncode(Url.Action("update", new { id = 1 }))%></td>
			</tr>
			<tr>
				<td>Destroy</td>
				<td><%= HttpUtility.HtmlEncode("Url.Action(\"destroy\"), new { id = 1 }")%></td>
				<td><%= HttpUtility.HtmlEncode(Url.Action("destroy", new { id = 1 }))%></td>
			</tr>
		</tbody>
	</table>