<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Packager.aspx.cs" Inherits="Pintle.Packager.sitecore.admin.Packager" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8">
	<title>Sitecore Package Generator</title>
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
	<meta name="description" content="">
	<meta name="author" content="">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">
	<link rel="stylesheet" href="https://getbootstrap.com/docs/3.3/examples/jumbotron-narrow/jumbotron-narrow.css">
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
	<style>
		@media (min-width: 768px) {
			.container { max-width: 830px; }
		}
		.toggle {
			background-color: #eee;
			padding: 5px 15px 5px 10px;
			border-bottom: solid 4px #ffffff;
		}
		.toggle:hover {
			background-color: #e2e2e2;
			cursor: pointer;
		}
	</style>

	<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
	<!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

<body>

	<div class="container">
		<div class="jumbotron">
			<h2>Sitecore Package Generator</h2>
			<p>Find list of your pre-configured packages below</p>
		</div>

		<% foreach (var package in this.ConfiguredPackages)
				{%>
		<div class="row marketing">
			<div class="col-lg-12">
				<h4>Package: <%=package.Metadata.PackageName%> <%=package.Metadata.Version%> by <%=package.Metadata.Author%> (<%=package.Name %>)</h4>
				<div class="toggle" data-toggle="metadata">
					<b>Metadata</b><span class="caret"></span>
					<%--<div class="toggle-icons">
						<span aria-hidden="true">&times;</span>
					</div>--%>
				</div>
				<div id="metadata">
					<table class="table table-striped table-condensed">
					<tbody >
						<tr>
							<td>Package name</td>
							<td><%=package.Metadata.PackageName%></td>
						</tr>
						<tr>
							<td>Author</td>
							<td><%=package.Metadata.Author%></td>
						</tr>
						<tr>
							<td>Version</td>
							<td><%=package.Metadata.Version%></td>
						</tr>
						<tr>
							<td>Publisher</td>
							<td><%=package.Metadata.Publisher%></td>
						</tr>
						<tr>
							<td>Comment</td>
							<td><%=package.Metadata.Comment%></td>
						</tr>
						<tr>
							<td>License</td>
							<td><%=package.Metadata.License%></td>
						</tr>
						<tr>
							<td>Package ID</td>
							<td><%=package.Metadata.PackageId%></td>
						</tr>
						<tr>
							<td>Revision</td>
							<td><%=package.Metadata.Revision%></td>
						</tr>
					</tbody>
				</table>
				</div>
				<% if (package.Items.Any()) { %>
					<div class="toggle" data-toggle="items">
						<b><%=package.Items.Count%> Items</b>
						<span class="caret"></span>
						<%--<div class="toggle-icons">
							<span aria-hidden="true">&times;</span>
						</div>--%>
					</div>
					<div id="items" style="display: none;">
						<table class="table table-striped table-condensed">
							<tbody>
							<% foreach (var item in package.Items) { %>
								<tr>
									<td><%=item.Database%></td>
									<td><%=item.Path%></td>
									<td><%=item.IncludeChildren ? "children" : "" %></td>
								</tr>
							<% } %>
							</tbody>
						</table>
					</div>
				<% } %>
				<% if (package.Files.Any()) { %>
					<div class="toggle" data-toggle="files">
						<b><%=package.Files.Count%> Files</b><span class="caret"></span>
						<%--<div class="toggle-icons">
							<span aria-hidden="true">&times;</span>
						</div>--%>
					</div>
					<div id="files" style="display: none;">
					<table  class="table table-striped table-condensed">
						<tbody>
						<% foreach (var file in package.Files) { %>
							<tr>
								<td><%=file.Path%></td>
							</tr>
						<% } %>
						</tbody>
					</table>
					</div>
				<% } %>
				<h3>Parameters</h3>
				<br/>
				<form>
					<input type="hidden" name="packageName" value="<%= package.Name%>" />
					<% foreach (var param in package.Parameters) {%>
					<div class="form-group">
						<label for="<%=param.Value.Name%>"><%=param.Value.DisplayName%> <span style="font-size: smaller"><%=param.Value.Required ? "(required)" : "" %></span></label>
						<% if (param.Value.HtmlType == "textarea")
								{%>
						<textarea class="form-control" name="<%=param.Value.Name%>" id="<%=param.Value.Name%>" placeholder="<%=param.Value.DisplayName%>"><%=param.Value.DefaultValue%></textarea>
						<% }
								else
								{ %>
						<input type="<%=param.Value.HtmlType %>" class="form-control" name="<%=param.Value.Name%>" id="<%=param.Value.Name%>" placeholder="<%=param.Value.DisplayName%>" value="<%=param.Value.DefaultValue%>">
						<% } %>
					</div>
					<% } %>
					<div class="buttons">
						<button type="button" class="btn btn-default btn-generate">Generate package</button>
						<%--<a href="#" class="btn btn-success btn-download">Download Hawkeye v.0.0.1 Sitecore.NET 9.0.2 (rev. 180604).zip</a>--%>
					</div>
				</form>
			</div>
		</div>
		<hr />
		<% } %>

		<footer class="footer">
			<p>&copy; 2019 Pintle ApS</p>
		</footer>

	</div>
	<!-- /container -->

	<!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
	<script src="https://getbootstrap.com/docs/3.3/assets/js/ie10-viewport-bug-workaround.js"></script>

</body>

<script>
	$(function () {

		$(".toggle").on("click", function() { slide(this); });
		$(".btn").on("click", function (e) {
			var applyFilters = $(this).attr("data-apply-filter") === "true";
			var action = $(this).attr("data-action");
			var type = $(this).attr("data-type");

			if (action === "undefined") {
				action = "";
			}

			if (type === "undefined") {
				type = "";
			}

			var url = window.location.href + getJoiner() + "task=" + command;
			$.get(url, function () { });
		});
	});

	function slide(e) {
		var toToggle = $(e).attr("data-toggle");
		$("#" + toToggle).slideToggle();
	}

	function disableAllButtons(button) {
		$(".btn-active").attr("disabled", "disabled");
	}

	function enableAllButtons() {
		$(".btn-active").removeAttr("disabled");
	}

	function getJoiner() {
		var joiner = "?";
		if (window.location.href.indexOf("?") > -1) {
			joiner = "&";
		}

		return joiner;
	}
</script>
</html>
