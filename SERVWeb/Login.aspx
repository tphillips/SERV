<%@ Page Language="C#" Inherits="SERVWeb.Login" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>


<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Login</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<div class="well">
<h3>Login</h3>
<label>Email Address:</label>
<asp:TextBox runat="server" id="txtEmail" />

<label>Password:</label>
<asp:TextBox runat="server" id="txtPassword" TextMode="password"/>

<br/><br/>
<asp:Button runat="server" id="cmdLogin" Text="Login" class="btn btn-primary btn-lg" onclick="cmdLoginClick"/>

</div>
<script>
	_loaded();
</script>

<asp:Literal runat="server" id="litServerClient"></asp:Literal>

</asp:Content>