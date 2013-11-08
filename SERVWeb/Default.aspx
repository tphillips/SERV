<%@ Page Language="C#" Inherits="SERVWeb.Default" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	<div class="hero-unit">
		<h2>Do Something</h2>	
		<p >Nothing interesting on this page . . .</span></p>	
	</div>
	
	<script>
		$("#loading").hide();
		if ("<%=this.Success%>" == "yes") { $("#success").slideDown(); }
	</script>
	
</asp:Content>
