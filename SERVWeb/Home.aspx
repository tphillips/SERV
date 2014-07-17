<%@ Page Language="C#" Inherits="SERVWeb.Home" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">System</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

	<%=SERVGlobal.CalendarJSInclude()%>	

	<div class="row">
		<div class="span12">
			<div class="hero-unit">
				<h2><span id="lblGreeting">Hey</span> <%=this.Username%>!</h2>
				<span id="lblNextShift" style="display:none"><p>Your next duty is a <strong><span id="lblNextShiftType">?</span></strong> shift <strong><span id="lblNextShiftDate">?</span></strong>.  Feeling generous? Why not sign up for a few more? Click <a href="Calendar.aspx">here</a> to view <a href="Calendar.aspx">the Calendar</a>.</p></span>
				<span id="lblNoShift" style="display:none; color:red"><p><strong>You don't appear to have any upcoming shifts? Why not sign up for a few now? Click <a href="Calendar.aspx">here</a> to view <a href="Calendar.aspx">the Calendar.</a></strong></p></span>
				<!---<p><span style="color:red">New:</span>  You can now view the <a href="Forum.aspx">forum from the system</a>.</p>-->
				<p><small>Don't use Internet Explorer to view this site. It's a terrible browser and you will be FAR better off using Chrome or Firefox.</small></p>
			</div>
		</div>
	</div>
	<div class="row">
		<div class="span4">
			<div id="calendarBulletins" style="display:">
				<img src="img/spinnerLarge.gif" width="30" />
			</div>
		</div>
		<div class="span4">
			<div id="recentActivity" style="display:">
				<img src="img/spinnerLarge.gif" width="30" />
			</div>
		</div>
		<div class="span4 pull-right">
			<a class="twitter-timeline" width="400px" href="https://twitter.com/SERV_SSL" data-widget-id="484637303671771137" data-tweet-limit="2">Tweets by @SERV_SSL</a>
			<script>!function(d,s,id){var js,fjs=d.getElementsByTagName(s)[0],p=/^http:/.test(d.location)?'http':'https';if(!d.getElementById(id)){js=d.createElement(s);js.id=id;js.src=p+"://platform.twitter.com/widgets.js";fjs.parentNode.insertBefore(js,fjs);}}(document,"script","twitter-wjs");</script>
		</div>
	</div>

	<div class="row">
		<small>
			<div class="span12" id="calendar" style="display:none">
				<h3>The Calendar &nbsp;&nbsp;&nbsp;<a href="Calendar.aspx"><small>Click here to see more, request swaps or volunteer</small></a></h3>
				<table class="table table-striped table-bordered table-condensed">
					<tr>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay1"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay2"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay3"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay4"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay5"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay6"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay7"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay1"></div></td>
						<td><div id="scheduledDay2"></div></td>
						<td><div id="scheduledDay3"></div></td>
						<td><div id="scheduledDay4"></div></td>
						<td><div id="scheduledDay5"></div></td>
						<td><div id="scheduledDay6"></div></td>
						<td><div id="scheduledDay7"></div></td>
					</tr>

					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay8"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay9"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay10"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay11"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay12"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay13"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay14"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay8"></div></td>
						<td><div id="scheduledDay9"></div></td>
						<td><div id="scheduledDay10"></div></td>
						<td><div id="scheduledDay11"></div></td>
						<td><div id="scheduledDay12"></div></td>
						<td><div id="scheduledDay13"></div></td>
						<td><div id="scheduledDay14"></div></td>
					</tr>
				</table>

				<p>
					<i class="icon-star icon-green"></i> = Ad-Hoc / Member volunteered.<br/>
					<i class="icon-exclamation-sign icon-red"></i> = Swap needed, member cannot do shift. <br/>
					<i class="icon icon-calendar"></i> = Scheduled slot.<br/>
				</p>

			</div>
		</small>
	
	</div>

	<br/>
	<div class="row">
		
		<div class="span12">
			<h4>Version <%=SERVGlobal.SERVVersion%></h4>
			<h5><a href="#" onclick="$('#changeLog').slideDown();">Recent Changes</a></h5>
			<div id="changeLog" style="display:none">
				<ul>
					<li>Calendar paging, you can now view/edit the calendar into the future and the past</li>
				</ul>
				<h5>Previously</h5>
				<ul>
					<li>Performance tuning</li>
					<li>Calendar Urgencies</li>
					<li>Calendar properties editing</li>
					<li>Favicon</li>
					<li>In Network flag for locations</li>
					<li>Removed calendar from take control</li>
					<li>Allow controllers to send SMS from their number if required</li>
					<li>Feedback button</li>
					<li>Additional reports around the new calendar</li>
					<li>Session keep alive on the calendar page</li>
					<li>Minor bug fixes</li>
					<li>Forum in the system!  Click View > The Forum, or SERV > The forum.  One less place to visit.  The system is now your one stop SERV shop.</li>
					<li>Cache safe JS minification and build script</li>
					<li>Verbs</li>	
					<li>New home screen layout</li>
					<li>Adding key to home calendar</li>
					<li>Caching mods</li>
					<li>Calendar Sorting</li>
					<li>Much, much more efficient calendar generation code</li>
					<li>Calendar email notifications for volunteering, being volunteered and shift swaps</li>
					<li>Promoting the calendar work so far.  The new calendar system is in TESTING ONLY and you should continue to use the google calendar</li>
					<li>More calendar work</li>
					<li>Background release including calendar work - hidden from sight</li>
					<li>Minor fixes</li>
					<li>New user registration form</li>
					<li>Bug fix on ShowMember on OpsMap</li>
					<li>A major new feature: Route planning on the ops map</li>
					<li>Disabled future dates on the controller logging date selectors</li>
					<li>Added a notes field on the ops map, use by clicking Map > Show / Hide Controller Notes Box</li>
					<li>Added Show Member on Map to ops map, to allow controllers to show the members that they have on shift on the map</li>
					<li>Added calendar links</li>
					<li>Added controller signon tool</li>
					<li>Added remaining credit count to SMS screen</li>
					<li>Fixing SMS form character filtering and encoding - Don't use ' or return (You will not be allowed to type them).  Feel free to use other ascii characters.  Unicode is not allowed</li>
					<li>Adding AQL sms messaging provider</li>
					<li>Removing certain chars from sms messages</li> 
					<li>Added join date to a few of the reports where it would be useful</li>
					<li>Adding a Non AdQual report </li>
					<li>Fixed a bug where newly added members do not have the join date set</li>
					<li>Impersonation</li>
					<li>Hiding admin only fields by default on membership screen for the sake of simplicity</li>
					<li>Adding user level editing to membership screen</li>
					<li>Only return active members in ListMembersWithTags()</li>
					<li>Lots of new reports</li>
					<li>Adding Committee membership tag</li>
					<li>Showing tags on membership list</li>
					<li>Adding OnRota tag</li>
					<li>Subscribing ALL members to the emergency list.  To unsubscribe, opt out by unticking the "Emergency Contact List" in you profile in the membership system. This involves logging into the membership system</li>
					<li>More business logic controlling unsubscribing from the emergency list</li>
					<li>Added ability for controllers to log runs that were turned down.  All reports adjusted to not show runs that were not competed</li>
					<li>Fixed a location editing permissions issue - Thanks Duncan</li>
					<li>Added the ability to view / edit blood runs from a new report, visible under "View" > "Reports and Stats" > "Recent Runs (Controller View)".  
					Anybody can view a run's details, controllers can edit the runs that they created and administrators can edit all runs.  Currently AA runs can only be viewed.  
					Editing a run will assign the run a new ID.</li>
					<li>Darren's very own shiny report on the stats page</li>
					<li>Brand new reports!!</li>
					<li>Password resetting</li>
					<li>Adding more calendar days highlighting current users name where shown</li>
					<li>Icons</li>
					<li>Added the new top 10 riders report for 2014 onwards, driven by the new controller logging form</li>
					<li>Added a hover over description of the various states of the session keep alive icon.</li>
					<li>Added calendar info to home screen.</li>
					<li>Added a session status icon which will go red if the user session is terminated for any reason, 
						on the controller log a message prompting re-login is shown</li>
					<li>Tweaks to ops map</li>
					<li>Controllers can now use .'s in times</li>
					<li>Controllers can now leave home safe date and time blank</li>
					<li>Urgency on controller logging is now 3 buttons (Just for Geoff)</li>
					<li>Warning when modifying a location</li>
					<li>Permission issues fixed around location addition and editing - committee level required</li>
					<li>Added a new menu item in the toolbar to open the controller logging sheet in a new tab</li>
					<li>Added a release coming warning</li>
					<li>2014 Run log report</li>
					<li>Version increment for Controller logging go live</li>
					<li>Async requests off for $.ajax()</li>
					<li>Raw run log import - make less process intensive (circa 50%) with more logging</li>
					<li>Html tweaks to controller log</li>
					<li>Controller log auto scrolling on selecting Blood Run (for OpsMap compatibility)</li>
					<li>Priority + now priority number-- (Requested by GS, makes sense)</li>
					<li>Modified permissions around vewing a locations details</li>
					<li>Bug fix on controller logging around free format notes</li>
					<li>Added home safe time to blood controllers log form</li>
					Adding some denormalized data to RunLog records for ease of reporting
					<li>Operations Map, with loads of cool features (it has it's own menu at the bottom of the screen)</li>
					<li>Air Ambulance Controller Logging! The controller logging code is now complete, let testing begin</li>
					<li>Show all locations on a map in the list locations screen</li>
					<li>Fixing an issue with multibox runs</li>
					<li>More intelligence around handovers in logging</li>
					<li>More AA run work</li>
					<li>Added a google map link on member profile page</li>
					<li>Controller logging tab renames</li>
					<li>Bug fix on controller form where blood box count could not be incremented.  (Good to see people are testing it then!)</li>
					<li>New Report</li>
					<li>Notes entry on the controller form</li>
					<li>Bug fixes to the controller log when dealing with samples</li>
					<li>Controller logging intelligence</li>
					<li>Controller log title shows selected rider and destination to ease multi-tab use</li>
					<li>Controller log now has a keepalive to prevent session timeout if multi logging in realtime</li>
					<li>Another report added</li>
					<li>Fixing niceAlert()</li>
					<li>Adding controller log to power user menu</li>
					<li>Adding authentication to controller log</li>
					<li>Adding generic messaging to home.aspx</li>
					<li>Moving generic js include to master</li>
					<li>Login feedback</li>
					<li>Updating jQueryUI custom build for dialog()</li>
					<li>Added a report to show members who are activley running but have not logged into the system.</li>
					<li>Threaded sending mails when sending out membership status mails</li>
					<li>Made membership status mails more efficient</li>
					<li>Controller form now using type-ahead, try it out</li>
					<li>Adding and Editing locations</li>
					<li>Membership Email dev work</li>
					<li>Added show inactive members check on member list and hide leavers by default</li>
					<li>Allow committee members to update members GMP date and Assessment date</li>
					<li>Added some logging around member updates</li>
					<li>Top 10 Volunteers</li>
					<li>Time fixes to raw log import</li>
					<li>Recent Runs List</li>
					<li>Locations List</li>
					<li>New menu structure</li>
					<li>More mobile friendly, try it!</li>
					<li>Bulk SMS is here</li>
					<li>Loads of work on the controller logging.  We may get to the new system for logging before the end of the month</li>
					<li>The batch loader for the old logs kept in Google is working well</li>
					<li>Title changes. The change password screen said "Login" . . . Doh</li>
				</ul>
			
				<h3>Coming Soon</h3>
				<ul>
					<li>Who knows . . .</li>
				</ul>
				<h3>Known Issues</h3>
				<ul>
					<li>Do <strong>not</strong> use IE.  Use Chrome or Firefox.</li>
				</ul>
				<p>Found an issue? PM <a target="_blank" href="http://servssl.org.uk/members/index.php?/user/29-tristan-phillips/">Tris</a></p>
			</div>
		</div>
			
	</div>
	<br/>

	<div class="row">
		<div class="span12">
			
		</div>
	</div>

	<script>
		$("#loading").hide();
		$("#loading").empty();
		loadNewsBanner();
		FullName = "<%=this.FullName%>";
		setGreeting();
		getNextShift();
		if ("<%=this.Success%>" == "yes") { $("#success").slideDown(); window.setTimeout('$("#success").slideUp()',4000); }
		if ("<%=this.Message%>" != "") { niceAlert("<%=this.Message%>"); }
		initCalendar(true, 14);
		loadCalendar(<%=this.MemberId%>, <%=this.UserLevel%>);
		listCalendarBulletins();
		listRecentRuns();
		initFeedback();
	</script>
	
</asp:Content>
