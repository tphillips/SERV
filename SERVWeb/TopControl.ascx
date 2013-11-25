<%@ Control Language="C#" Inherits="SERVWeb.TopControl" %>
<nav class="navbar navbar-static-top" role="navigation">
	<div class="navbar-inner">
		<ul class="nav">
			<asp:Literal runat="server" id="pnlNotLoggedIn">
			<li><a href="/">Home</a></li>
			<li><a href="Login.aspx">Login</a></li>
			</asp:Literal>
			<asp:Literal runat="server" id="pnlLoggedIn">
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">SERV <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="Home.aspx">Home</a></li>
						<li class="divider"></li>
						<li><a href="ChangePassword.aspx">Change Password</a></li>
						<li><a href="Login.aspx?Logout=yes">Logout</a></li>
					</ul>
				</li>
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">View <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="ViewMember.aspx?self=yes">Your Profile</a></li>
						<li class="divider"></li>
						<li><a href="Members.aspx">Member List</a></li>
						<li><a href="Locations.aspx">Locations List</a></li>
						<li class="divider"></li>
						<li><a href="RunStats.aspx">Run Statistics</a></li>
					</ul>
				</li>
			</asp:Literal>
			<asp:Literal runat="server" id="pnlPowerUser">
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">Tools <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="SMS.aspx">Bulk SMS</a></li>
						<li class="divider"></li>
					</ul>
				</li>
			</asp:Literal>
		</ul>
		<p class="navbar-text pull-right"><asp:Literal runat="server" id="litLoginName"/></p>
	</div>
</nav>