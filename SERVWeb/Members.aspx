<%@ Page Language="C#" Inherits="SERVWeb.Members" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Members</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

	<script language="JavaScript" src="js/JS.js"></script>
	
	<div id="entry" style="display:none">
		<h3>Member List</h3>
		<div id="results">
		
		</div>
		
		<a type=button id="cmdAdd" class="btn btn-primary btn-lg" href="ViewMember.aspx?new=yes">Add a New Member</a>
	</div>
	
	<script>
	
	$(function() 
	{
		if (<%=this.UserLevel%> < 4)
		{
			$("#cmdAdd").hide();
		}
		SearchMembers(<%=this.UserLevel%>, "");
	});
	
	</script>

</asp:Content>
