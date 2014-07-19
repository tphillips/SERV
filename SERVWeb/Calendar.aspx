<%@ Page Language="C#" Inherits="SERVWeb.Calendar" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">The Calendar</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<%=SERVGlobal.CalendarJSInclude()%>
<br/>


<div id="entry" style="display:none">
	<small>

		<div id="calendar">
			
				<div class="btn-group" style="position:absolute; top:8px;right:50%">
					<button type="button" class="btn btn-mini" onclick="window.location.href='Calendar.aspx?Page=<%=this.PageNum-1%>'"><i class="icon icon-chevron-left"></i></button>
					<button type="button" class="btn btn-mini" onclick="window.location.href='Calendar.aspx?Page=0'">Today</button>
					<button type="button" class="btn btn-mini" onclick="window.location.href='Calendar.aspx?Page=<%=this.PageNum+1%>'"><i class="icon icon-chevron-right"></i></button>
				</div>

				<table class="table table-striped table-bordered table-condensed">
					
					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay1"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay2"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay3"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay4"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay5"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay6"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay7"></span></td>
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

					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay15"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay16"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay17"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay18"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay19"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay20"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay21"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay15"></div></td>
						<td><div id="scheduledDay16"></div></td>
						<td><div id="scheduledDay17"></div></td>
						<td><div id="scheduledDay18"></div></td>
						<td><div id="scheduledDay19"></div></td>
						<td><div id="scheduledDay20"></div></td>
						<td><div id="scheduledDay21"></div></td>
					</tr>

					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay22"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay23"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay24"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay25"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay26"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay27"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay28"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay22"></div></td>
						<td><div id="scheduledDay23"></div></td>
						<td><div id="scheduledDay24"></div></td>
						<td><div id="scheduledDay25"></div></td>
						<td><div id="scheduledDay26"></div></td>
						<td><div id="scheduledDay27"></div></td>
						<td><div id="scheduledDay28"></div></td>
					</tr>
				</table>
			
		</div>
	</small>
	<div style="display:none">
		<p>Key:</p>
		<div class="row">
			<div class="span3">
				<p>
					<div class="calendarSlot calendarSlot1" style="">Blood</div>
					<div class="calendarSlot calendarSlot2" style="">AA Night</div>
					<div class="calendarSlot calendarSlot3" style="">Day Controller</div>
					<div class="calendarSlot calendarSlot4" style="">Night Controller</div>
					<div class="calendarSlot calendarSlot5" style="">AA Night 2</div>
					<div class="calendarSlot calendarSlot6" style="">AA Daytime</div>
					<div class="calendarSlot calendarSlot7" style="">Hooleygan</div>
				</p>
			</div>
			<div class="span9">
				<p>
					<i class="icon-star icon-green"></i> = Ad-Hoc / Member volunteered.<br/>
					<i class="icon-exclamation-sign icon-red"></i> = Swap needed, member cannot do shift. <br/>
					<i class="icon icon-calendar"></i> = Scheduled slot.<br/>
				</p>
			</div>
		</div>
		<br/>
	</div>

</div>

<div id="calSlotDialog" style="display:none; background-color:#fcfcfc" title="Your Shift">
	<p>We realise you have a life outside of SERV.</p>
	<p>If you cannot carry out your <strong><span id="swapDialogCalendarName">CAL TYPE</span></strong> shift on <strong><span id="swapDialogShiftDate">SHIFT DATE</span></strong>, please click "Swap Needed"</p>
	<br/>
	<input type="button" value="Swap Needed" class="btn btn-primary btn-lg" onclick="swapNeededClicked();"></input> <input type="button" value="Cancel" class="btn btn-lg" onclick="$('#calSlotDialog').dialog('close');"></input>
	<br/><br/>
</div>

<div id="volunteerDialog" style="display:none; background-color:#fcfcfc" title="Volunteer">
	<p>Are you free on the <strong><span id="volunteerDialogShiftDate">SHIFT DATE</span></strong>? We would really appreciate your time!</p>
	<label>Please select the type of shift you want:</label>
	<li class="dropdown">
		<a href="#" type="button" class="btn btn-lg dropdown-toggle" data-toggle="dropdown" id="cboVolunteerCalendar"><span id="lblVolunteerCalendar" class="lblVolunteerCalendar">Shift Type</span> <b class="caret"></b></a>
		<ul class="dropdown-menu" id="lstVolunteerCalendar">
		</ul>
	</li>
	<br/><br/><br/><br/><br/><br/><br/><br/>
	<input type="button" value="OK, Add Me!" class="btn btn-primary btn-lg" onclick="volunteerClicked();"></input> <input type="button" value="No" class="btn btn-lg" onclick="$('#volunteerDialog').dialog('close');"></input>
	<br/><br/>
</div>

<div id="addVolunteerDialog" style="display:none; background-color:#fcfcfc" title="Add a Volunteer">
	<p>You are adding a volunteer to the calendar on <strong><span id="addVolunteerDialogShiftDate">SHIFT DATE</span></strong>.</p>
	<label>Member:</label>
		<div class="input-append">
			<input style="width:250px" type="text" class="riders" id="txtFindMember" />
			<a style="width:10px" type=button class="btn btn-lg" disabled><i class="icon-search"></i></a>
		</div>
		<br/>
	<label>Please select the type of shift:</label>
	<li class="dropdown">
		<a href="#" type="button" class="btn btn-lg dropdown-toggle" data-toggle="dropdown" id="cboAddVolunteerCalendar"><span id="lblAddVolunteerCalendar" class="lblVolunteerCalendar">Shift Type</span> <b class="caret"></b></a>
		<ul class="dropdown-menu" id="lstAddVolunteerCalendar">
		</ul>
	</li>
	<br/><br/><br/><br/><br/><br/><br/><br/>
	<input type="button" value="Add" class="btn btn-primary btn-lg" onclick="addVolunteerClicked();"></input> <input type="button" value="Cancel" class="btn btn-lg" onclick="$('#addVolunteerDialog').dialog('close');"></input>
	<br/><br/>
</div>

<script>

	$(function() 
	{
		$("#container").removeClass("container");
		$("#container").addClass("fullScreenContainer");
		keepAlive();
		initCalendar(false, 28);
		calendarPage = <%=this.PageNum%>;
		loadCalendar(<%=this.MemberId%>, <%=this.UserLevel%>);
		//initFeedback();
	});

</script>

</asp:Content>


