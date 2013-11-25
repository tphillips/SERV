<%@ Page Language="C#" Inherits="SERVWeb.RunStats" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Run Stats</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

	<script language="JavaScript" src="js/JS.js"></script>
	
	<div id="entry" style="display:none">
		<h3>Recent Runs</h3>
		<p>This data is currently imported from the "Raw" Google run log the controllers fill in.  
		Some controllers do this more quickly than others, so runs may not appear for a few days.  
		The data is also prone to errors.</p>  
		<p>This will all be resolved when the controllers start to use the new controller logging tool coming soon in this system.</p>
		<div class="row">
			<div class="span12">
				<asp:DataGrid runat="server" id="dgReport" CssClass="table table-striped table-bordered table-condensed"/>
			</div>
		</div>
	</div>
	
	<script>
	
	$(function() 
	{
		$("#loading").slideUp();
		$("#entry").slideDown();
		//ShowRunStats();
	});
	
	</script>

</asp:Content>



