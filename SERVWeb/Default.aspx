<%@ Page Language="C#" Inherits="SERVWeb.Default" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">System</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	<div class="hero-unit">
		<div class="row">
			<div class="span3">
				<img src="img/logo.png"/>
				<br/><br/>
			</div>
			<div class="span7">
				<h2>SERV SSL</h2>	
				<p>Welcome to the SERV system.  Please <a href="Login.aspx">login</a> to continue.</p>	
			</div>
		</div>
	</div>
	
	<script>
		$("#loading").hide();
	</script>
	
</asp:Content>
