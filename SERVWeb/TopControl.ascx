<%@ Control Language="C#" Inherits="SERVWeb.TopControl" %>
<nav class="navbar navbar-static-top" role="navigation">
	<div class="navbar-inner">
		<ul class="nav">
			<asp:Literal runat="server" id="pnlNotLoggedIn">
			<li><a href="/">Home</a></li>
			<li><a href="Login.aspx">Login</a></li>
			</asp:Literal>
			<asp:Literal runat="server" id="pnlLoggedIn">
			<li><a href="Home.aspx">Home</a></li>
			<li><a href="ViewMember.aspx?self=yes">Your Profile</a></li>
			<li><a href="Members.aspx">Member List</a></li>
			<li><a href="http://servssl.org.uk/members/">The Forum</a></li>
			<li><a href="ChangePassword.aspx">Change Password</a></li>
			<!---<li><a href="Login.aspx?Logout=yes">Logout</a></li>-->
			</asp:Literal>
			<asp:Literal runat="server" id="pnlPowerUser">
			<li><a href="SMS.aspx">Bulk SMS</a></li>
			</asp:Literal>
		</ul>
		<p class="navbar-text pull-right"><asp:Literal runat="server" id="litLoginName"/></p>
	</div>
</nav>