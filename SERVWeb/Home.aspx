<%@ Page Language="C#" Inherits="SERVWeb.Home" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">System</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<div class="hero-unit">
		<h2>Hey <%=this.Username%>,</h2>	
		<p>Now that you have logged in, please make sure you <a href="ChangePassword.aspx">change your password</a>.  
		You can then <a href="ViewMember.aspx?self=yes">view and edit your profile</a> to make sure it is all correct, or take a look at the <a href="Members.aspx">Members List</a>, <a href="OpsMap.aspx">Operations Map</a> or <a href="RunStats.aspx">Stat Reports</a>.</p>	
		<p>Don't use Internet Explorer to view this site. It's a terrible browser and you will be FAR better off using Chrome or Firefox.</p>
	</div>

	<div class="row">
		<div class="span12">
		<h3><a href="https://www.google.com/calendar/embed?src=serv.surrey%40gmail.com&ctz=Europe/London" target="_blank">Blood Calendar</a></h3>
		</div>
	</div>
	<div class="row">
		<div class="span3">
			<div id="pnlBloodCal" class="calDay calToday">...
			</div>
		</div>
		<div class="span3">
			<div id="pnlBloodCal1" class="calDay">...
			</div>
		</div>
		<div class="span3">
			<div id="pnlBloodCal2" class="calDay">...
			</div>
		</div>
		<div class="span3">
			<div id="pnlBloodCal3" class="calDay">...
			</div>
		</div>
	</div>

	<div class="row">
		<div class="span12">
		<h3><a href="https://www.google.com/calendar/embed?src=k31pom7k4lhv3eq619iffm50ss%40group.calendar.google.com&ctz=Europe/London" target="_blank">AA Calendar</a></h3>
		</div>
	</div>
	<div class="row">
		<div class="span3">
			<div id="pnlAACal" class="calDay calToday">...
			</div>
		</div>
		<div class="span3">
			<div id="pnlAACal1" class="calDay">...
			</div>
		</div>
		<div class="span3">
			<div id="pnlAACal2" class="calDay">...
			</div>
		</div>
		<div class="span3">
			<div id="pnlAACal3" class="calDay">...
			</div>
		</div>
	</div>
	<br/>
	<div class="row">
		
		<div class="span12">
			<h3>Version 1.4.8.3</h3>
			<h4>Recent Changes</h4>
			<ul>
				<li>Added Show Member on Map to ops map, to allow controllers to show the members that they have on shift on the map</li>
			</ul>
			<h5>Previously</h5>
			<ul>
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
				<li>Subscribing ALL members to the emergency list.  To unsubscribe, opt out by unticking the "Emergency Contact List" in you profile in the membership system. This involves logging into the membership system ;)</li>
				<li>More business logic controlling unsubscribing from the emergency list</li>
				<li>Added ability for controllers to log runs that were turned down.  All reports adjusted to not show runs that were not competed</li>
				<li>Fixed a location editing permissions issue - Thanks Duncan</li>
				<li>Added the ability to view / edit blood runs from a new report, visible under "View" > "Reports and Stats" > "Recent Runs (Controller View)".  
				Anybody can view a run's details, controllers can edit the runs that they created and administrators can edit all runs.  Currently AA runs can only be viewed.  
				Editing a run will assign the run a new ID.</li>
				<li>Darren's very own shiny report on the stats page ;)</li>
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
				<li>Urgency on controller logging is now 3 buttons (Just for Geoff ;))</li>
				<li>Warning when modifying a location</li>
				<li>Permission issues fixed around location addition and editing - committee level required</li>
				<li>Added a new menu item in the toolbar to open the controller logging sheet in a new tab</li>
				<li>Added a release coming warning</li>
				<li>2014 Run log report</li>
				<li>Version increment for Controller logging go live</li>
				<li>Async requests off for $.ajax();</li>
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
			<p>Found an issue? PM <a target="_blank" href="http://servssl.org.uk/members/index.php?/user/29-tristan-phillips/">Tris</p>
		</div>

	</div>
	
	<script>
		$("#loading").hide();
		FullName = "<%=this.FullName%>";
		if ("<%=this.Success%>" == "yes") { $("#success").slideDown(); window.setTimeout('$("#success").slideUp()',4000); }
		if ("<%=this.Message%>" != "") { niceAlert("<%=this.Message%>"); }
		showCals();
	</script>
	
</asp:Content>
