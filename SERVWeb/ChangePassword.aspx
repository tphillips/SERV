<%@ Page Language="C#" Inherits="SERVWeb.ChangePassword" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>


<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Change Password</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<h3>Change Your Password</h3>
<label>Email Address:</label>
<asp:TextBox runat="server" id="txtEmail" />

<label>Old Password:</label>
<asp:TextBox runat="server" id="txtOldPassword" TextMode="password"/>
<label>New Password:</label>
<asp:TextBox runat="server" id="txtNewPassword" TextMode="password"/>
<label>New Password:</label>
<asp:TextBox runat="server" id="txtNewPassword2" TextMode="password"/>

<br/><br/>
<asp:Button runat="server" id="cmdChange" Text="Change Password" class="btn btn-primary btn-lg readOnlyHidden" onclick="cmdChangeClick"/>

<script>
	_loaded();
</script>

</asp:Content>