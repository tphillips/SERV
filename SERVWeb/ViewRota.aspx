<%@ Page Language="C#" Inherits="SERVWeb.ViewRota" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Rota</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<%=SERVGlobal.CalendarJSInclude()%>	

	<div id="entry" style="display:none">

		<h3><span class="calendarName"></span> Calendar Rota</h3>

		<p>Use this page to view &amp; manage the rota for the <span class="calendarName"></span> calendar.</p>
	
		<div class="alert">
			<button type="button" class="close" data-dismiss="alert">&times;</button>
			Please note, managing the rota is not the same as managing the calendar. 
			The calendar is built from the rota. To add exceptions, remove a member from an individual shift or to add an ad-hoc volunteer to an individual shift, please use the calendar manager.
		</div>

		<p></p>
			
		<div class="alert alert-success">
			<button type="button" class="close" data-dismiss="alert">&times;</button>
			The calendar was last generated from this rota on <strong><span id="lblLastGenerated"></span></strong> up to: <strong><span id="lblGeneratedTo"></span></strong>.  
			<a id="cmdGenerate" class="pull-right" href="#" onclick="generateCalendars();">Generate Now</a> This is a <span id="lblCalendarType"></span> rota <span id="lblSimpleDesc">with a 
			<span id="lblIncrement"></span> day rotation</span>. <a href="#" id="cmdShowProps" onclick="$('#divProperties').slideDown();">Click to edit rota properties</a>
		</div>

		<div class="well" style="display:none" id="divProperties">
			<div class="row">
				<div class="span12">
					
					<label>Calendar Name:</label>
					<input type="text" id="txtCalendarName"/>

					<label>Sort Order (where on the calendar do these slots appear):</label>
					<input type="text" style="width:30px;" id="txtSortOrder"/>

					<label>Tag required for volunteer (also controls who alert emails are sent to):</label>
					<div class="btn-group" data-toggle="buttons-radio">
					    <button type="button" class="btn" onclick="rotaTagId=7;" id="radTag7">Blood volunteer</button>
					    <button type="button" class="btn" onclick="rotaTagId=8;" id="radTag8">Air ambulance volunteer</button>
					    <button type="button" class="btn" onclick="rotaTagId=3;" id="radTag3">Controller</button>
					</div>
					<br/><br/>
				
					<label>Default nightly requirement (controls help alerts):</label>
					<div class="btn-group" data-toggle="buttons-radio">
						<button type="button" class="btn" onclick="defReq=0;" id="radReq0">0</button>
					    <button type="button" class="btn" onclick="defReq=1;" id="radReq1">1</button>
					    <button type="button" class="btn" onclick="defReq=2;" id="radReq2">2</button>
					    <button type="button" class="btn" onclick="defReq=3;" id="radReq3">3</button>
					    <button type="button" class="btn" onclick="defReq=4;" id="radReq4">4</button>
					    <button type="button" class="btn" onclick="defReq=5;" id="radReq5">5</button>
					</div><br/><br/>
				
					<p><input class="btn" type="button" onclick="cmdSaveRotaPropsClicked();" value="Save"></p>
				</div>
			</div>
		</div>

		<div class="row" style="display:none">
			<div class="span4">
				<label>Calendar Name:</label>
				<input type="text" id="txtCalendarName" class="" placeholder="" />
			</div>
			<div class="span4">
				<label>Calendar Type:</label>
				<div class="btn-group" data-toggle="buttons-radio">
				    <button type="button" class="btn" onclick="" id="btnSimple">Simple</button>
				    <button type="button" class="btn" onclick="" id="btnComplex">Complex</button>
				</div><br/><br/>
			</div>
			<div class="span4">
				<label>Rotation:</label>
				<div class="input-append">
					<input type="text" id="txtRotation" class="" placeholder="" style="width:40px" />
					<span class="add-on">days</span>
				</div>
			</div>
		</div>

		<div class="row">
			<div class="span6">
				<h5>Week A <span class="calendarName"></span> Rota</h5>
			</div>
		</div>
		<small>
		<div class="row" id="rotaA">
			<div class="span6">
				<table class="table table-striped table-bordered table-condensed">
					<thead>
						<tr>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay1"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay2"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay3"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay4"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay5"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay6"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekADay7"></span></th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td>
								<div id="rosteredWeekADay1"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',1);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekADay2"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',2);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekADay3"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',3);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekADay4"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',4);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekADay5"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',5);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekADay6"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',6);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekADay7"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('A',7);">+ Roster New</a></div>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
		</small>

		<div class="row">
			<div class="span6">
				<h5>Week B <span class="calendarName"></span> Rota</h5>
			</div>
		</div>
		<small>
		<div class="row" id="rotaB">
			<div class="span6">
				<table class="table table-striped table-bordered table-condensed">
					<thead>
						<tr>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay1"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay2"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay3"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay4"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay5"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay6"></span></th>
							<th style="text-align:center; vertical-align:top;"><span id="titleWeekBDay7"></span></th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td>
								<div id="rosteredWeekBDay1"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',1);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekBDay2"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',2);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekBDay3"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',3);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekBDay4"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',4);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekBDay5"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',5);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekBDay6"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',6);">+ Roster New</a></div>
							</td>
							<td>
								<div id="rosteredWeekBDay7"></div>
								<div class="calendarSlot rosterNew"><a href="#" onclick="startRosterVolunteer('B',7);">+ Roster New</a></div>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
		</small>
					
	</div>

	<div id="memberSearch" style="display:none; background-color:#fcfcfc" title="Rosta a Member">
	<br/>
		<label>Member:</label>
		<div class="input-append">
			<input style="width:250px" type="text" class="riders" id="txtFindMember" />
			<a style="width:10px" type=button class="btn btn-lg" disabled><i class="icon-search"></i></a>
		</div>
		<br/>
		<label>Duty Day:</label>
		<li class="dropdown">
			<a href="#" type="button" class="btn btn-lg dropdown-toggle" data-toggle="dropdown" id="cboDay"><span id="lblRosteringDay">Day of Week</span> <b class="caret"></b></a>
			<ul class="dropdown-menu">
				<li><a href="#" onclick="rosteringDay = 1; setRosterDialogDay();">Monday</a></li>
				<li><a href="#" onclick="rosteringDay = 2; setRosterDialogDay();">Tuesday</a></li>
				<li><a href="#" onclick="rosteringDay = 3; setRosterDialogDay();">Wednesday</a></li>
				<li><a href="#" onclick="rosteringDay = 4; setRosterDialogDay();">Thursday</a></li>
				<li><a href="#" onclick="rosteringDay = 5; setRosterDialogDay();">Friday</a></li>
				<li><a href="#" onclick="rosteringDay = 6; setRosterDialogDay();">Saturday</a></li>
				<li><a href="#" onclick="rosteringDay = 7; setRosterDialogDay();">Sunday</a></li>
			</ul>
		</li>
		<br/>
		<label>Week:</label>
		<div class="btn-group" data-toggle="buttons-radio">
		    <button type="button" class="btn" onclick="rosteringWeek = 'A'; setRosterDialogWeek();" id="btnWeekA">A</button>
		    <button type="button" class="btn" onclick="rosteringWeek = 'B'; setRosterDialogWeek();" id="btnWeekB">B</button>
		</div><br/><br/>

		<label>Repeats every:</label>
		<div class="input-append">
			<input type="text" id="txtRosteringRotation" class="" placeholder="" style="width:40px" value="" disabled/>
			<span class="add-on">days</span>
		</div><br/>

		<br/><br/>
		<input style="width:70px" type="button" value="Roster" class="btn btn-primary btn-lg" onclick="rosterVolunteer();"></input>
		<br/><br/>
	</div>

	<div id="removeSlot" style="display:none; background-color:#fcfcfc" title="Remove a Rota Slot">
		<p>Do you really want to remove "<span id="lblRemoveMember"></span>" from day <span id="lblRemoveNight"></span> week <span id="lblRemoveWeek"></span>?</p>
		<br/>
		<input type="button" value="Yes" class="btn btn-primary btn-lg" onclick="removeRotaSlot();"></input> <input type="button" value="No" class="btn btn-lg" onclick="$('#removeSlot').dialog('close');"></input>
		<br/>
	</div>

	<script>

	$(function() 
	{
		rosteringCalendarId = <%=this.CalendarId%>;
		userLevel = <%=this.UserLevel%>;
		initViewRota(userLevel, rosteringCalendarId);
	});

	</script>

</asp:Content>


