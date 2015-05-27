<%@ Control Language="C#" Inherits="SERVWeb.TopControl" %>
<%@ Import Namespace="SERVWeb" %>

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
						<li><a href="Forum.aspx"><i class="icon-comment"></i> The Forum</a></li>
						<li class="divider"></li>
						<li><a href="ChangePassword.aspx"><i class="icon-lock"></i> Change Password</a></li>
						<li><a href="Login.aspx?Logout=yes"><i class="icon-off"></i> Logout</a></li>
					</ul>
				</li>
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">View <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="ViewMember.aspx?self=yes"><i class="icon-user"></i> Your Profile</a></li>
						<li><a href="Calendar.aspx"><i class="icon-calendar"></i> The Calendar</a></li>
						<li class="divider"></li>
						<li><a href="Members.aspx"><i class="icon-list"></i> Member List</a></li>
						<li><a href="Locations.aspx"><i class="icon-map-marker"></i> Locations List</a></li>
						<li><a href="Calendars.aspx"><i class="icon-calendar"></i> Calendar Rota List</a></li>
						<li class="divider"></li>
						<li class="dropdown-submenu">
							<a tabindex="-1" href="RunStats.aspx"><i class="icon-print"></i> Reports and Stats</a>
							<ul class="dropdown-menu">
								<li><a href="RunStats.aspx#runLog">Recent Runs</a></li>
								<li><a href="RunStats.aspx#boxesByProdByMonth">Delivery &amp; Call Stats</a></li>
								<li><a href="RunStats.aspx#lastMonth">Month's Stats</a></li>
								<li class="divider"></li>
								<li><a href="RunStats.aspx#top10">Top 10 Volunteers</a></li>
								<li><a href="RunStats.aspx#top10Controllers">Top 10 Controllers</a></li>
								<li class="divider"></li>
								<li><a href="RunStats.aspx#todaysUsers">Today's Users</a></li>
								<li><a href="RunStats.aspx#newMembers">New Members</a></li>
								<li><a href="RunStats.aspx#membersNotOnRota">Members Not on Rota</a></li>
								<li><a href="RunStats.aspx#activeNoLogin">Active Member No Login</a></li>
								<li><a href="RunStats.aspx#lastRunByMember">Latest Run by Member</a></li>
								<li><a href="RunStats.aspx#adQualMembers">AdQual Members</a></li>
								<li><a href="RunStats.aspx#lastGdpDates">Last GDP Dates</a></li>
								<li><a href="RunStats.aspx#riderAssessmentDates">Rider Assessment Dates</a></li>
								<li class="divider"></li>
								<li><a href="RecentRuns.aspx">Run Log</a></li>
							</ul>
						</li>
						<li class="divider"></li>
						<li><a href="Forum.aspx"><i class="icon-comment"></i> The Forum</a></li>
						<li class="divider"></li>
						<li><a href="BloodBoxBingo.aspx"><i class="icon-ok-circle"></i> Blood Box Bingo!</a></li>
					</ul>
				</li>
				<li class="dropdown">
					<a href="#" class="dropdown-toggle" data-toggle="dropdown">Tools <b class="caret"></b></a>
					<ul class="dropdown-menu">
						<li><a href="OpsMap.aspx"><i class="icon-globe"></i> Operations Map</a></li>
						<li class="divider"></li>
						<li><a href="ControllerLog.aspx"><i class="icon-edit"></i> Controller Logging</a></li>
						<li><a href="ControllerLog.aspx" target="_blank"><i class="icon-chevron-right"></i> Controller Logging (New Tab)</a></li>
						<li><a href="TakeControl.aspx"><i class="icon-random"></i> Take Control of SERV NOW</a></li>
						<li class="divider"></li>
						<li><a href="SMS.aspx"><i class="icon-envelope"></i> Bulk SMS</a></li>
						<li class="dropdown-submenu">
							<a tabindex="-1" href="RunStats.aspx"><i class="icon-bullhorn"></i> Push Notifications</a>
							<ul class="dropdown-menu">
								<div style="padding: 10px;">
								<a class="pushbullet-subscribe-widget" data-channel="servssl_calendar" data-widget="button" data-size="small"></a><script type="text/javascript">(function(){var a=document.createElement('script');a.type='text/javascript';a.async=true;a.src='https://widget.pushbullet.com/embed.js';var b=document.getElementsByTagName('script')[0];b.parentNode.insertBefore(a,b);})();</script>
								<a class="pushbullet-subscribe-widget" data-channel="servssl_controllers" data-widget="button" data-size="small"></a><script type="text/javascript">(function(){var a=document.createElement('script');a.type='text/javascript';a.async=true;a.src='https://widget.pushbullet.com/embed.js';var b=document.getElementsByTagName('script')[0];b.parentNode.insertBefore(a,b);})();</script>
								<a class="pushbullet-subscribe-widget" data-channel="servssl" data-widget="button" data-size="small"></a><script type="text/javascript">(function(){var a=document.createElement('script');a.type='text/javascript';a.async=true;a.src='https://widget.pushbullet.com/embed.js';var b=document.getElementsByTagName('script')[0];b.parentNode.insertBefore(a,b);})();</script>
								</div>
							</ul>
						</li>
					</ul>
				</li>
			</asp:Literal>
		</ul>
		<p class="navbar-text pull-right"><span id="lblLoginName"><asp:Literal runat="server" id="litLoginName"/></span></p>
		<span class="newsBanner pull-right" id="newsBanner" runat="server"><m--arquee><span id="newsBannerText"></span></m--arquee></span>
	</div>
</nav>