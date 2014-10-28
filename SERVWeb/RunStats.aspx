<%@ Page Language="C#" Inherits="SERVWeb.RunStats" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ OutputCache Duration="120" VaryByParam="None"%>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Run Stats</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<script>
	
	$(function() 
	{
		loaded();
	});
	
	</script>

</asp:Content>



