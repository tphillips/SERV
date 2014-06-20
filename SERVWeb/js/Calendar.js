
var userLevel = -1;

var rosteringMemberId = -1;
var rosteringDay = -1;
var rosteringWeek = -1;
var rosteringRepeatInterval = -1;
var rosteringCalendarId = -1;

var removingMemberId = -1;
var removingWeek = '';
var removingDay = -1;

function initWeeksCalendar()
{
	$("#calSlotClickedDialog").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	$("#volunteerClickedDialog").dialog({
		width:340,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
}

function calSlotClicked()
{
	$("#calSlotClickedDialog").dialog('open');
}

function volunteerSlotClicked()
{
	$("#volunteerClickedDialog").dialog('open');
}

function loadWeeksCalendar(memberId)
{
	callServerSide(
		"Service/Service.asmx/ListSpansCaledarEntries",
		"{'days':28}",
		function(json)
		{
			for(var day = 0; day < 28; day++)
			{
				if (json.d[day].length > 0)
				{
					$("#titleDay" + (day + 1)).text(json.d[day][0].EntryDateShortStringWithDay);
					for (var sched = 0; sched < json.d[day].length; sched++)
					{
						var toAppend = '<div class="calendarSlot calendarSlot' + json.d[day][sched].CalendarID + '" style="width:150px">' +
									'<a href="#" onclick="calSlotClicked()"><i>' + json.d[day][sched].MemberName + '</i></a>' + 
								'</div>';
						if (json.d[day][sched].MemberID != memberId)
						{
							var toAppend = '<div class="calendarSlot calendarSlot' + json.d[day][sched].CalendarID + '" style="width:150px">' +
									 json.d[day][sched].MemberName  + 
								'</div>';
						}
						$("#scheduledDay" + (day + 1)).append(toAppend);
						toAppend = '<div class="calendarSlot" style="width:150px">' +
									'<a href="#" onclick="volunteerSlotClicked()"><i class="icon-plus-sign icon-green"></i> Volunteer!</a>' + 
								'</div>';
						$("#scheduledDay" + (day + 1)).append(toAppend);
					}
				}
			}
			$("#loading").slideUp();
			$("#entry").slideDown();
		},
		function()
		{
			$("#error").slideDown();
		}
	);
}

function initViewCalendar(userlevel, calendarId) 
{
	userLevel = userlevel;
	if (userLevel < 3)
	{
		$(".rosterNew").hide();
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
	showViewCalendar(calendarId);
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

function showViewCalendar(calendarId)
{
	callServerSide(
		"Service/Service.asmx/GetCalendar", 
		"{'calendarId':'" + calendarId + "'}",
		function(json)
		{
			$(".calendarName").text(json.d.Name);
			$("#txtCalendarName").val(json.d.Name);
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

