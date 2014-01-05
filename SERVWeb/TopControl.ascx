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
						<li><a href="RunStats.aspx">Reports and Stats</a></li>
					</ul>
				</li>
			</asp:Literal>
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">Tools <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="OpsMap.aspx">Operations Map</a></li>
						<asp:Literal runat="server" id="pnlPowerUser">
							<li class="divider"></li>
							<li><a href="ControllerLog.aspx">Controller Logging</a></li>
							<li><a href="ControllerLog.aspx" target="_blank">Controller Logging (New Tab)</a></li>
							<li class="divider"></li>
							<li><a href="SMS.aspx">Bulk SMS</a></li>
						</asp:Literal>
					</ul>
				</li>
		</ul>
		<p class="navbar-text pull-right"><span id="lblLoginName"><asp:Literal runat="server" id="litLoginName"/></span></p>
	</div>
</nav>