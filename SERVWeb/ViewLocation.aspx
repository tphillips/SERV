<%@ Page Language="C#" Inherits="SERVWeb.ViewLocation" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

	<div id="entry" style="display:none">
		<h3>View / Edit Location</h3>
		<div class="row">
			<div class="span12">
				Coming soon.
			</div>
		</div>

	</div>

	<script>
	
	$(function() 
	{
		$("#loading").slideUp();
		$("#entry").slideDown();
	});
	
	</script>

</asp:Content>


