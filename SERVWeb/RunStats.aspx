<%@ Page Language="C#" Inherits="SERVWeb.RunStats" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ OutputCache Duration="120" VaryByParam="None"%>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Run Stats</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

	<script language="JavaScript" src="js/JS.js"></script>
	
	<div id="entry" style="display:none">
		<div class="row">
			<div class="span3">
				<h3>Top 10 Volunteers</h3>
				<p>Based on number of runs done this year. Ordered by name.</p>  
				<asp:DataGrid runat="server" id="dgTop10" CssClass="table table-striped table-bordered table-condensed"/>
			</div>
			<div class="span9">
				<h3>Recent Runs</h3>
				<p>This data is currently imported from the "Raw" Google run log the controllers fill in.  
				Some controllers do this more quickly than others, so runs may not appear for a few days.  
				The data is also prone to errors, you can spot them where the blank fields are.</p>  
				<p>This will all be resolved when the controllers start to use the new controller logging tool coming soon in this system.</p>
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



