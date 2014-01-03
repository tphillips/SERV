<%@ Page Language="C#" Inherits="SERVWeb.RunStats" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ OutputCache Duration="120" VaryByParam="None"%>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Run Stats</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<div id="entry" style="display:none">
		<p>Jump To: <a href="#todaysUsers">Todays Users</a> | <a href="#top10">Top 10 Volunteers</a> | <a href="#runLog">2014 Runs</a> | <a href="#recentRuns">2013 Runs</a> | <a href="#averageRuns">Average Runs Per Day</a> | <a href="#activeNoLogin">Active Member - No Login</a> | <a href="#callsPerHour">Call Per Hour Heatmap</a></p>

		<div class="row">
			<div class="span12">
				<a id="runLog"></a>
				<h3>2014 Runs</h3>
				<p>Real time information from the controller log.</p>  
				<asp:DataGrid runat="server" id="dgRunLog" CssClass="table table-striped table-bordered table-condensed"/>
			</div>
		</div>

		<div class="row">
			<div class="span3">

				<a id="todaysUsers"></a>
				<h3>Todays Users</h3>
				<p>Users who have logged on today.</p>  
				<asp:DataGrid runat="server" id="dgTodaysUsers" CssClass="table table-striped table-bordered table-condensed"/>

				<a id="top10"></a>
				<h3>Top 10 Volunteers</h3>
				<p>Based on number of runs done in 2013. Ordered by name.</p>  
				<asp:DataGrid runat="server" id="dgTop10" CssClass="table table-striped table-bordered table-condensed"/>

				<a id="callsPerHour"></a>
				<h3>Calls / Hour Heatmap</h3>
				<p>Over the last 240 days of 2013.  The bigger the logo, the busier the hour on average.</p>  
				<asp:GridView runat="server" id="dgHeatMap" CssClass="table table-striped table-bordered table-condensed" AutoGenerateColumns="false">
				<Columns>
					<asp:BoundField HeaderText="Day" DataField="Day" />
					<asp:BoundField HeaderText="Hour" DataField="Hour" />
					<asp:TemplateField HeaderText="Calls">
					<ItemTemplate>
						<img src="img/logo.png" width="<%# Eval("Calls", "{0}") %>"/>
					</ItemTemplate>
					</asp:TemplateField>
				</Columns>
				</asp:GridView>

				<a id="averageRuns"></a>
				<h3>Average Runs Per Day</h3>
				<p>Over the last 240 days of 2013.</p>  
				<asp:DataGrid runat="server" id="dgAvgPerDay" CssClass="table table-striped table-bordered table-condensed"/>

			</div>
			<div class="span9">

				<a id="recentRuns"></a>
				<h3>2013 Runs</h3>
				<p>This data is currently imported from the "Raw" Google run log the controllers fill in.  
				Some controllers do this more quickly than others, so runs may not appear for a few days.  
				The data is also prone to errors, you can spot them where the blank fields are.</p>  
				<p>This will all be resolved when the controllers start to use the new controller logging tool coming soon in this system.</p>
				<asp:DataGrid runat="server" id="dgReport" CssClass="table table-striped table-bordered table-condensed"/>

				<a id="activeNoLogin"></a>
				<h3>Active Member - No Login</h3>
				<p>This report shows members who have done a run since May 13 but not yet logged into the new system.</p>  
				<asp:DataGrid runat="server" id="dgRunNoLogin" CssClass="table table-striped table-bordered table-condensed"/>
				<asp:Label runat="server" id="lblRunNoLogin" />
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



