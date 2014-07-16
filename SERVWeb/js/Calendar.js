
var userLevel = -1;
var MANAGE_CALENDAR_USER_LEVEL = 3;

var rosteringMemberId = -1;
var rosteringDay = -1;
var rosteringWeek = -1;
var rosteringRepeatInterval = -1;
var rosteringCalendarId = -1;

var defReq=0;
var rotaTagId=0;

var removingMemberId = -1;
var removingWeek = '';
var removingDay = -1;

var calMemberId = -1;
var calUserLevel = -1;
var swapRequestMemberId = -1;
var swapRequestCalendarId = -1;
var swapRequestShiftDate = -1;

var volunteerMemberId;
var volunteerCalendarId = -1;
var volunteerShiftDate = -1;
var volunteerCalendarName = "";

var showCalendarDays = 28;
var simpleCalendar = false;

function initCalendar(simple, days)
{
	showCalendarDays = days;
	simpleCalendar = simple;
	$("#calSlotDialog").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	$("#volunteerDialog").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	$("#addVolunteerDialog").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	if (!simpleCalendar)
	{
		listMembersWithTag("Rider,Driver,Blood,AA,Controller", null);
		$(".riders").autocomplete({ source: memberNames });
		listCalendarsForDroppdown("lstVolunteerCalendar");
		listCalendarsForDroppdown("lstAddVolunteerCalendar");
	}
}

function listCalendarBulletins()
{
	callServerSide(
		"Service/Service.asmx/GetNextXDaysCalendarBulletins", 
		"{'days':'7'}",
		function(json)
		{
			var toAppend = "<table class='table table-striped table-bordered table-condensed'><tr><th><span style='color:red'>Urgencies!</span> <small><a href='Calendar.aspx'>Click to help</a></small></th></tr>";
			for(var x = 0; x < json.d.length; x++)
			{
				var alert = json.d[x];
				if (alert.indexOf("no Hooleygan ") > -1)
				{
					alert = "<span style='color:red'>" + alert + "</span>";
				}
				if (alert.indexOf("no Night Controller") > -1)
				{
					alert = "<span style='color:red'>" + alert + "</span>";
				}
				if (alert.indexOf("today") > -1)
				{
					alert = "<span style='color:red'><strong>" + alert + "</strong></span>";
				}
				toAppend += "<tr><td>" + alert + "</td></tr>";
			}
			toAppend += "</table>";
			$("#calendarBulletins").empty();
			$("#calendarBulletins").append(toAppend);
			$("#calendarBulletins").slideDown();
		},
		function()
		{
		}
	);
}

function listCalendarsForDroppdown(dropDownId)
{
	callServerSide(
		"Service/Service.asmx/ListCalendars", 
		"{}",
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				$("#" + dropDownId).append('<li><a href="#" onclick="volunteerCalendarId = ' + json.d[x].CalendarID + '; volunteerCalendarName=\'' + json.d[x].Name + '\'; setVolunteerCalendarDisplay();">' + json.d[x].Name + '</a></li>');
			}
		},
		function()
		{
		}
	);
}

function setVolunteerCalendarDisplay()
{
	$(".lblVolunteerCalendar").text(volunteerCalendarName);
}

function calSlotClicked(slotMemberId, calendarId, calendarName, shiftDate, niceShiftDate, swapMarked)
{
	swapRequestMemberId = slotMemberId;
	swapRequestCalendarId = calendarId;
	swapRequestShiftDate = shiftDate;
	$("#swapDialogCalendarName").text(calendarName);
	$("#swapDialogShiftDate").text(niceShiftDate);
	$("#calSlotDialog").dialog('open');
}

function swapNeededClicked()
{
	callServerSide(
		"Service/Service.asmx/MarkShiftSwapNeeded",
		"{'calendarId' : '" + swapRequestCalendarId + "', 'memberId' : '" + swapRequestMemberId + "', 'shiftDate' : '" + swapRequestShiftDate + "'}",
		function(json)
		{
			$("#calSlotDialog").dialog('close');
			niceAlert('Thank you. You have been removed from that shift');
			loadCalendar(calMemberId, calUserLevel);
		},
		function()
		{
			niceAlert('Sorry, An error occured. Please try again or contact the tech team');
		}
	);
}

function volunteerSlotClicked(calendarId, calendarName, shiftDate, niceShiftDate)
{
	volunteerCalendarId = -1;
	$("#lblVolunteerCalendar").text("Shift Type");
	volunteerMemberId = calMemberId;
	volunteerShiftDate = shiftDate;
	$("#volunteerDialogShiftDate").text(niceShiftDate);
	$("#volunteerDialog").dialog('open');
}

function addVolunteerSlotClicked(calendarId, calendarName, shiftDate, niceShiftDate)
{
	volunteerCalendarId = -1;
	$("#lblAddVolunteerCalendar").text("Shift Type");
	volunteerMemberId = -1;
	volunteerShiftDate = shiftDate;
	$("#addVolunteerDialogShiftDate").text(niceShiftDate);
	$("#addVolunteerDialog").dialog('open');
}

function volunteerClicked()
{
	if (volunteerCalendarId == -1)
	{
		niceAlert("Please choose a shift type");
		return;
	}
	callServerSide(
		"Service/Service.asmx/AddVolunteerToCalendar",
		"{'calendarId' : '" + volunteerCalendarId + "', 'memberId' : '" + volunteerMemberId + "', 'shiftDate' : '" + volunteerShiftDate + "'}",
		function(json)
		{
			$("#volunteerDialog").dialog('close');
			niceAlert('Thank you! You have been added to that shift');
			loadCalendar(calMemberId, calUserLevel);
		},
		function()
		{
			niceAlert('Sorry, An error occured. Please try again or contact the tech team');
		}
	);
}

function addVolunteerClicked()
{
	volunteerMemberId = getMemberId($("#txtFindMember").val());
	if (volunteerMemberId == 0)
	{
		niceAlert("Please choose a volunteer");
		return;
	}
	if (volunteerCalendarId == -1)
	{
		niceAlert("Please choose a shift type");
		return;
	}
	callServerSide(
		"Service/Service.asmx/AddVolunteerToCalendar",
		"{'calendarId' : '" + volunteerCalendarId + "', 'memberId' : '" + volunteerMemberId + "', 'shiftDate' : '" + volunteerShiftDate + "'}",
		function(json)
		{
			$("#txtFindMember").val("");
			$("#addVolunteerDialog").dialog('close');
			niceAlert('The member has been added to that shift');
			loadCalendar(calMemberId, calUserLevel);
		},
		function()
		{
			niceAlert('Sorry, An error occured. Please try again or contact the tech team');
		}
	);
}

function loadCalendar(memberId, userLevel)
{
	calMemberId = memberId;
	calUserLevel = userLevel;
	$("#entry").slideUp();
	$("#loading").slideDown();
	for (var x = 0; x < showCalendarDays; x++)
	{
		$("#scheduledDay" + (x + 1)).empty();
	}
	callServerSide(
		"Service/Service.asmx/ListSpansCaledarEntries",
		"{'days':" + showCalendarDays + "}",
		function(json)
		{
			for(var day = 0; day < showCalendarDays; day++)
			{
				if (json.d[day].length > 0)
				{
					var today = false;
					$("#titleDay" + (day + 1)).text(json.d[day][0].EntryDateShortStringWithDay);
					if (json.d[day][0].IsToday)
					{
						today=true;
						$("#titleDay" + (day + 1)).addClass('today');
					}
					for (var sched = 0; sched < json.d[day].length; sched++)
					{
						var slotMemberId = json.d[day][sched].MemberID;
						var calendarId = json.d[day][sched].CalendarID;
						var calendarName = json.d[day][sched].CalendarName;
						var memberName = json.d[day][sched].MemberName;
						var shiftDate = json.d[day][sched].EntryDateClrString;
						var niceShiftDate = json.d[day][sched].EntryDateShortStringWithDay;
						var swapMarked = json.d[day][sched].CoverNeeded;
						var adHoc = json.d[day][sched].AdHoc;
						var strikeout = "";
						var strong = "";
						var italic = "";
						if (slotMemberId == memberId) { strong = "<strong>"; italic="<i>" }
						var icon = "<i class='icon icon-calendar'></i> ";
						if (calendarName == null) { calendarName = ""; }
						if (adHoc) { icon = "<i class='icon icon-heart icon-star icon-green'></i> ";}
						if (swapMarked) { strikeout="text-decoration: line-through"; icon = "<i class='icon icon-exclamation-sign icon-red'></i> ";}
						if (calendarName == "") { icon = ""; }
						var toAppend = '<div title="' + calendarName + '" class="calendarSlot calendarSlot' + calendarId + '" style="width:150px">' + icon +
									strong + italic + '<a style="' + strikeout + '" href="#" onclick="' +
									'calSlotClicked(' + slotMemberId + ', ' + calendarId + ', \'' + calendarName + '\', \'' + shiftDate + '\', \'' + niceShiftDate + '\', ' + swapMarked + ');' + 
									'">' + memberName + '</a></i></strong>' + 
								'</div>';
						if ((slotMemberId != memberId && userLevel < MANAGE_CALENDAR_USER_LEVEL) || simpleCalendar)
						{
							var toAppend = '<div title="' + calendarName + '" class="calendarSlot calendarSlot' + calendarId + '" style="width:150px; ' + strikeout + '">' + icon + strong + italic + memberName  + '</i></strong></div>';
						}
						$("#scheduledDay" + (day + 1)).append(toAppend);
					}
					if (!simpleCalendar)
					{
						if (userLevel < MANAGE_CALENDAR_USER_LEVEL)
						{
							toAppend = '<div class="calendarSlot" style="width:150px">' +
											'<a href="#" onclick="' +
											'volunteerSlotClicked(' + calendarId + ', \'' + calendarName + '\', \'' + shiftDate + '\', \'' + niceShiftDate + '\');' + 
											'"><i class="icon-plus-sign icon-green"></i> Volunteer!</a>' + 
										'</div>';
						}
						else
						{
							toAppend = '<div class="calendarSlot" style="width:150px">' +
											'<a href="#" onclick="addVolunteerSlotClicked(' + calendarId + ', \'' + calendarName + '\', \'' + shiftDate + '\', \'' + niceShiftDate + '\');"><i class="icon-plus-sign icon-green"></i> Add Volunteer</a>' + 
										'</div>';
						}
						$("#scheduledDay" + (day + 1)).append(toAppend);
					}
				}
			}
			$("#loading").slideUp();
			$("#entry").slideDown();
			$("#calendar").slideDown();
			$("#calendar").slideDown();
		},
		function()
		{
			$("#error").slideDown();
		}
	);
}

function initViewRota(userlevel, calendarId) 
{
	userLevel = userlevel;
	if (userLevel < MANAGE_CALENDAR_USER_LEVEL)
	{
		$(".rosterNew").hide();
		$("#cmdGenerate").hide();
		$("#cmdShowProps").hide();
	}
	if (userLevel < 4)
	{
		$("#cmdGenerate").hide();
	}
	$("#memberSearch").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	$("#removeSlot").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	listMembersWithTag("Rider,Driver,Blood,AA,Controller", null);
	$(".riders").autocomplete({ source: memberNames });
	$("#exCal").datepicker({ dateFormat: 'dd M yy' });
	renderWeekATitles();
	renderWeekBTitles();
	showViewRota(calendarId);
	listRosteredVolunteers(calendarId);
}

function generateCalendars()
{
	callServerSide(
		"Service/Service.asmx/GenerateCalendar",
		"{}",
		function(json)
		{
			niceAlert("The Calendars are Generating");
		},
		function()
		{
			$("#error").slideDown();
		}
	);
}

function listRosteredVolunteers(calendarId)
{
	clearRosterSlots();
	callServerSide(
		"Service/Service.asmx/ListRosteredVolunteers", 
		"{'calendarId':'" + calendarId + "'}",
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				var week = json.d[x].Week;
				var day = json.d[x].DayNo;
				var member = json.d[x].MemberName;
				var memId = json.d[x].MemberID;
				var toAppend = '<div id="rosteredMemberWeek' + week + 'Day' + day + 'MemberID' + memId + '" class="calendarSlot">' +
									'<a href="#" onclick="startRemoveRotaSlot(\'' + week + '\', ' + day + ', ' + memId + ')">' + member + '</a>' + 
								'</div>';
				$("#rosteredWeek" + week + "Day" + day).append(toAppend);
			}
			$("#loading").slideUp();
			$("#entry").slideDown();
		},
		function()
		{
		}
	);
	}

function rosterVolunteer()
{
	$("#error").slideUp();
	rosteringMemberId = getMemberId($("#txtFindMember").val());
	if (rosteringMemberId == 0) { niceAlert('Please choose a valid member'); return; }
	//$("#loading").slideDown();
	callServerSide(
		"Service/Service.asmx/RosterVolunteer", 
		"{'calendarId':'" + rosteringCalendarId + "','memberId':'" + rosteringMemberId + "','rosteringWeek':'" + rosteringWeek + "','rosteringDay':'" + rosteringDay + "'}",
		function(json)
		{
			$("#loading").slideUp();
			if (json.d == true)
			{
				$("#txtFindMember").val("");
				$("#memberSearch").dialog('close');
				listRosteredVolunteers(rosteringCalendarId);
			}
			else
			{
				$("#error").slideDown();
			}
		},
		function()
		{
			$("#error").slideDown();
		}
	);
}

function listRosteredVolunteers(calendarId)
{
	clearRosterSlots();
	callServerSide(
		"Service/Service.asmx/ListRosteredVolunteers", 
		"{'calendarId':'" + calendarId + "'}",
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				var week = json.d[x].Week;
				var day = json.d[x].DayNo;
				var member = json.d[x].MemberName;
				var memId = json.d[x].MemberID;
				var toAppend = '<div id="rosteredMemberWeek' + week + 'Day' + day + 'MemberID' + memId + '" class="calendarSlot">' +
									'<a href="#" onclick="startRemoveRotaSlot(\'' + week + '\', ' + day + ', ' + memId + ')">' + member + '</a>' + 
								'</div>';
				$("#rosteredWeek" + week + "Day" + day).append(toAppend);
			}
			$("#loading").slideUp();
			$("#entry").slideDown();
		},
		function()
		{
		}
	);
}

function startRemoveRotaSlot(week, day, memId)
{
	removingMemberId = memId;
	removingWeek = week;
	removingDay = day;
	$("#lblRemoveMember").text(getMemberName(removingMemberId));
	$("#lblRemoveNight").text(removingDay);
	$("#lblRemoveWeek").text(removingWeek);
	$("#removeSlot").dialog('open');
}

function removeRotaSlot()
{
	clearRosterSlots();
	callServerSide(
		"Service/Service.asmx/RemoveRotaSlot", 
		"{'calendarId':'" + rosteringCalendarId + "','memberId':'" + removingMemberId + "','rosteringWeek':'" + removingWeek + "','rosteringDay':'" + removingDay + "'}",
		function(json)
		{
			$('#removeSlot').dialog('close');
			listRosteredVolunteers(rosteringCalendarId);
		},
		function()
		{
			$("#error").slideDown();
		}
	);
}


function clearRosterSlots()
{
	for (var x = 0; x < 7; x++)
	{
		$("#rosteredWeekADay" + (x+1)).empty();
		$("#rosteredWeekBDay" + (x+1)).empty();
	}
}

function showViewRota(calendarId)
{
	$("#radTag7").removeClass("active");
	$("#radTag8").removeClass("active");
	$("#radTag3").removeClass("active");
	$("#radReq0").removeClass("active");
	$("#radReq1").removeClass("active");
	$("#radReq2").removeClass("active");
	$("#radReq3").removeClass("active");
	$("#radReq4").removeClass("active");
	$("#radReq5").removeClass("active");
	callServerSide(
		"Service/Service.asmx/GetCalendar", 
		"{'calendarId':'" + calendarId + "'}",
		function(json)
		{
			$(".calendarName").text(json.d.Name);
			$("#txtCalendarName").val(json.d.Name);
			$("#txtSortOrder").val(json.d.SortOrder);
			$("#radTag" + json.d.RequiredTagID).addClass("active");
			rotaTagId = json.d.RequiredTagID;
			$("#radReq" + json.d.DefaultRequirement).addClass("active");
			defReq = json.d.DefaultRequirement;
			$("#lblLastGenerated").text(json.d.LastGeneratedString);
			$("#lblGeneratedTo").text(json.d.GeneratedUpToString);
			if (json.d.SimpleCalendar)
			{
				$("#btnSimple").attr('disabled', true);
				$("#lblCalendarType").text("Simple");
				$("#lblIncrement").text(json.d.SimpleDaysIncrement);
				$("#txtRotation").val(json.d.SimpleDaysIncrement);
				$("#txtRosteringRotation").val(json.d.SimpleDaysIncrement);
				rosteringRepeatInterval = json.d.SimpleDaysIncrement;
			}
			else
			{
				$("#btnComplex").attr('disabled', true);
				$("#lblCalendarType").text("Complex");
				$("#lblSimpleDesc").hide();
			}
		},
		function()
		{
		}
	);
}

function cmdSaveRotaPropsClicked()
{
	saveRotaProps(rosteringCalendarId);
}

function saveRotaProps(calendarId)
{
	$("#loading").slideDown();
	callServerSide(
		"Service/Service.asmx/SaveCalendarProps", 
		"{'calendarId':'" + calendarId + "', 'calendarName':'" + $("#txtCalendarName").val() + "', 'sortOrder':'" + $("#txtSortOrder").val() + "', 'requiredTagId':'" + rotaTagId + "', 'defaultRequirement':'" + defReq + "'}",
		function(json)
		{
			$("#loading").slideUp();
			$("#divProperties").slideUp();
		},
		function()
		{
			$("#loading").slideUp();
			$("#error").slideDown();
		}
	);
}

function startRosterVolunteer(week, day)
{
	rosteringDay = day;
	rosteringWeek = week;
	setRosterDialogDay();
	setRosterDialogWeek();
	$('#memberSearch').dialog('open');
}

function setRosterDialogDay()
{
	switch(rosteringDay)
	{
		case 1: $("#lblRosteringDay").text("Monday"); break;
		case 2: $("#lblRosteringDay").text("Tuesday"); break;
		case 3: $("#lblRosteringDay").text("Wednesday"); break;
		case 4: $("#lblRosteringDay").text("Thursday"); break;
		case 5: $("#lblRosteringDay").text("Friday"); break;
		case 6: $("#lblRosteringDay").text("Saturday"); break;
		case 7: $("#lblRosteringDay").text("Sunday"); break;
	}
}

function setRosterDialogWeek()
{
	if (rosteringWeek == 'A')
	{
		$("#btnWeekA").attr('disabled', true);
		$("#btnWeekB").attr('disabled', false);
	}
	else
	{
		$("#btnWeekA").attr('disabled', false);
		$("#btnWeekB").attr('disabled', true);
	}
}


function listCalendars(userLevel)
{
	callServerSide(
		"Service/Service.asmx/ListCalendars", 
		"{}",
		function(json)
		{
			var append = '<table class="table table-striped table-bordered table-condensed">' +
			'<thead><tr><th></th><th>Calendar</th><th>Last Generated</th><th>Generated Up To</th></tr></thead><tbody>';
			for(var x = 0; x < json.d.length; x++)
			{
				var name ='<a href="ViewRota.aspx?calendarId=' + json.d[x].CalendarID + '">' + json.d[x].Name + '</a>';
				var row="<tr><td>" + (x + 1) + "</td><td>" + name + "</td>" +
					"<td>" + json.d[x].LastGeneratedString + "</td>" + 
					"<td>" + json.d[x].GeneratedUpToString + "</td>" + 
					"</tr>"
				append += row;
			}
			append += "</tbody></table>";
			$("#results").append(append);
			$("#loading").slideUp();
			$("#entry").slideDown();
		},
		function()
		{
		}
	);
}

function renderWeekATitles()
{
	callServerSide(
		"Service/Service.asmx/GetCurrentWeekADateStrings", 
		"{'format':'ddd dd MMMM'}",
		function(json)
		{
			for (var x = 0; x < 7; x++)
			{
				$("#titleWeekADay" + (x+1)).text(json.d[x]);
			}
		},
		function()
		{
		}
	);
}

function renderWeekBTitles()
{
	callServerSide(
		"Service/Service.asmx/GetCurrentWeekBDateStrings", 
		"{'format':'ddd dd MMMM'}",
		function(json)
		{
			for (var x = 0; x < 7; x++)
			{
				$("#titleWeekBDay" + (x+1)).text(json.d[x]);
			}
		},
		function()
		{
		}
	);
}

