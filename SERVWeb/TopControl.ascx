<%@ Control Language="C#" Inherits="SERVWeb.TopControl" %>
<nav class="navbar navbar-static-top" role="navigation">
	<div class="navbar-inner">
		<ul class="nav">
			<li><a href="/">Home</a></li>
			<asp:Literal runat="server" id="pnlNotLoggedIn">
			<li><a href="Login.aspx">Login</a></li>
			</asp:Literal>
			<asp:Literal runat="server" id="pnlLoggedIn">
			<li><a href="ViewMember.aspx?self=yes">Your Profile</a></li>
			<li><a href="Members.aspx">Members</a></li>
			<li><a href="Login.aspx?Logout=yes">Logout</a></li>
			</asp:Literal>
		</ul>
	</div>
</nav>