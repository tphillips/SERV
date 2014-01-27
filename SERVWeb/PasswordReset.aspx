<%@ Page Language="C#" Inherits="SERVWeb.PasswordReset" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Password Reset</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<h3>Reset Your Password</h3>
<p>Enter your email address and a new password will be sent to you.</p>
<p>When you press 'Reset' you will be directed to the login page.  Check your email and use the new password you are sent.<p/>
<label>Email Address:</label>
<asp:TextBox runat="server" id="txtEmail" />
<br/><br/>
<asp:Button runat="server" id="cmdChange" Text="Reset" class="btn btn-primary btn-lg" onclick="cmdResetClick"/>

<script>
	$("#loading").hide();
</script>

</asp:Content>


