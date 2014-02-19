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
						<li><a href="Home.aspx"><i class="icon-home"></i> Home</a></li>
						<li class="divider"></li>
						<li><a href="ChangePassword.aspx"><i class="icon-lock"></i> Change Password</a></li>
						<li><a href="Login.aspx?Logout=yes"><i class="icon-off"></i> Logout</a></li>
					</ul>
				</li>
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">View <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="ViewMember.aspx?self=yes"><i class="icon-user"></i> Your Profile</a></li>
						<li class="divider"></li>
						<li><a href="Members.aspx"><i class="icon-list"></i> Member List</a></li>
						<li><a href="Locations.aspx"><i class="icon-map-marker"></i> Locations List</a></li>
						<li class="divider"></li>
						<li class="dropdown-submenu">
							<a tabindex="-1" href="RunStats.aspx"><i class="icon-print"></i> Reports and Stats</a>
							<ul class="dropdown-menu">
								<li><a href="RunStats.aspx#runLog">Recent Runs</a></li>
								<li><a href="RunStats.aspx#todaysUsers">Todays Users</a></li>
								<li><a href="RunStats.aspx#top10">Top 10 Volunteers</a></li>
								<li><a href="RunStats.aspx#top10Controllers">Top 10 Volunteers</a></li>
								<li><a href="RunStats.aspx#boxesByProdByMonth">Stats</a></li>
							</ul>
						</li>
					</ul>
				</li>
			</asp:Literal>
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">Tools <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="OpsMap.aspx"><i class="icon-globe"></i> Operations Map</a></li>
						<asp:Literal runat="server" id="pnlPowerUser">
							<li class="divider"></li>
							<li><a href="ControllerLog.aspx"><i class="icon-edit"></i> Controller Logging</a></li>
							<li><a href="ControllerLog.aspx" target="_blank"><i class="icon-chevron-right"></i> Controller Logging (New Tab)</a></li>
							<li class="divider"></li>
							<li><a href="SMS.aspx"><i class="icon-envelope"></i> Bulk SMS</a></li>
						</asp:Literal>
					</ul>
				</li>
		</ul>
		<p class="navbar-text pull-right"><span id="lblLoginName"><asp:Literal runat="server" id="litLoginName"/></span></p>
	</div>
</nav>