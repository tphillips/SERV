<%@ Page Language="C#" Inherits="SERVWeb.Locations" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Locations</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<div id="entry" style="display:none">
		<h3>Location List</h3>
		<div class="row">
			<div class="span12">
				<div id="results">
				</div>
			</div>
		</div>
		<a type=button id="cmdAdd" class="btn btn-primary btn-lg" href="ViewLocation.aspx?new=yes">Add a New Location</a>
	</div>
	
	<script>
	
	$(function() 
	{
		if (<%=this.UserLevel%> < 4)
		{
			$("#cmdAdd").hide();
		}
		ListLocations(<%=this.UserLevel%>);
	});
	
	</script>

</asp:Content>


