<%@ Page Language="C#" Inherits="SERVWeb.Members" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Members</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<div id="entry" style="display:none">
		<h3>Member List</h3>
		<div class="row">
			<div class="span4 offset8 checkbox">
				<p class="pull-right"><input type="checkbox" id="chkInactive" onchange="search()">Show members who have left</input></p>
			</div>
			<div class="span12">
				<div id="results">
				</div>
			</div>
		</div>
		<a type=button id="cmdAdd" class="btn btn-primary btn-lg" href="ViewMember.aspx?new=yes"><i class="icon-plus icon-white"></i> Add a New Member</a>
	</div>
	
	<script>
	
	$(function() 
	{
		if (<%=this.UserLevel%> < 4)
		{
			$("#cmdAdd").hide();
		}
		search();
	});

	function search()
	{
		$("#entry").hide();
		$("#loading").slideDown();
		SearchMembers(<%=this.UserLevel%>, "", !$("#chkInactive").prop("checked"));
	}
	
	</script>

</asp:Content>
