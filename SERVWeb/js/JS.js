var asyncRequests = true;
var enableFeedback = false;
var FullName = "";
var locations = new Array();
var locationNames = new Array();
var members = new Array();
var memberNames = new Array();
var controllers = new Array();
var controllerNames = new Array();
var greetings = new Array("Hi", "Greetings", "Well Hello", "Sup", "Wagwan", "Haai", "Hola", "Hej", "Konnichiwa", "Zdravstvujte", "Yo", "Howdy", "Hiya", "Good day to you", "Oh, Hi");
var wentVerbs = new Array("went to","visited","warped to","travelled to","piloted themselves to","stopped off at", "took some air at","helped out","parked up at","delivered to","took something to", "set sail for", "plotted a course to", "bimbled along to");

function initFeedback()
{
	$("#feedbackDialog").dialog({
		width:493,
		show: { effect: "clip", duration: 200 },
		hide: { effect: "clip", duration: 200 },
		autoOpen: false
	});
	$("#cmdFeedback").fadeIn(2000);
	//window.setTimeout('hideFeedbackButton()', 1500);
	//window.setTimeout('showFeedbackButton()', 1600);
	//window.setTimeout('hideFeedbackButton()', 2100);
}

function showFeedbackButton()
{

	/*
	$("#cmdFeedback").animate({
		right: "+=95",
		}, 500, function() {
		// Animation complete.
	});
	*/
}

function hideFeedbackButton()
{
	/*
	$("#cmdFeedback").animate({
		right: "-100",
		}, 500, function() {
		// Animation complete.
	});
	*/
}

function cmdSubmitFeedbackClicked()
{
	callServerSide(
		"Service/Service.asmx/SendFeedback", 
		"{'feedback':'" + $("#txtFeedback").val() + "'}",
		function(json)
		{
			$("#feedbackDialog").dialog('close');
			$("#txtFeedback").val("");
			niceAlert("Thanks, your feedback has been sent.");
		},
		function(json)
		{
			
		}
	);
}	

function showFeedbackForm()
{
	$("#feedbackDialog").dialog('open');
}

function filterKeys()
{
	if (event.charCode==13 || event.charCode == 39)
	{
		event.preventDefault();
		return false;
	}
}

function loadNewsBanner()
{
	$("#newsBannerText").load("NewsHeadlines.htm");
}

function setGreeting()
{
	$("#lblGreeting").text(greetings[Math.floor(Math.random()*greetings.length)]);
}

function getErrorImage()
{
	var num = Math.floor(Math.random()*5) + 1;
	return "/img/error/Wrong" + num + ".jpg";
}

function getNextShift()
{
	callServerSideGet(
		"Service/Service.asmx/GetNextShift", 
		function(json)
		{
			if (json.d != null)
			{
				$("#lblNextShiftDate").text("on " + json.d.EntryDateShortStringWithDay);
				if (json.d.IsToday)
				{
					$("#lblNextShiftDate").text("today");
				}
				$("#lblNextShiftType").text(json.d.CalendarName);
				$("#lblNextShift").slideDown();
			}
			else
			{
				$("#lblNoShift").slideDown();
			}
		},
		function(json)
		{
			
		}
	);
}

function listRecentRuns()
{
	callServerSideGet(
		"Service/Service.asmx/ListRecentRuns", 
		function(json)
		{
			var toAppend = "<table class='table table-striped table-bordered table-condensed'><tr><th>Recently</th></tr>";
			for(var x = 0; x < json.d.length; x++)
			{
				toAppend += "<tr><td>" + json.d[x].MemberName + " " + wentVerbs[Math.floor(Math.random()*wentVerbs.length)] + " " + json.d[x].DeliverToDestinationName + "</td></tr>";
			}
			toAppend += "</table>";
			$("#recentActivity").empty();
			$("#recentActivity").append(toAppend);
			$("#recentActivity").slideDown();
		},
		function(json)
		{
			$("#recentActivity").empty();
			$("#recentActivity").append($("#errorImg").html());
			$("#recentActivity").slideDown();
		},
		true
	);
}

function ImpersonateMember(memberId)
{
	$("#loading").slideDown();
	callServerSide(
		"Service/Service.asmx/Impersonate", 
		"{'memberId':" + memberId + "}",
		function(json)
		{
			window.location.href="Home.aspx";
		},
		function(json)
		{
			niceAlert("Something went wrong.  You may not have sufficient privileges to view other member's details.  If you believe you should, please contact Admin.");
			$("#loading").slideUp();
		}
	);
}

function takeControl(overrideNum)
{
	$("#loading").slideDown();
	$("#entry").slideUp();
	callServerSide(
		"Service/Service.asmx/TakeControl", 
		"{'overrideNumber':'" + $("#txtNumber").val() + "'}",
		function(json)
		{
			if (json.d == true)
			{
				$("#success").slideDown();
				$("#loading").slideUp();
			}
			else
			{
				niceAlert("Something went wrong!");
				$("#loading").slideUp();
			}
		},
		function(json)
		{
			niceAlert("Something went wrong!");
			$("#loading").slideUp();
		}
	);
}

function DisplayMember(memberId)
{
	callServerSide(
		"Service/Service.asmx/GetMember", 
		"{'memberId':" + memberId + "}",
		function(json)
		{
			if (json.d != null)
			{
				$("#lblTitle").text(json.d.FirstName + " " + json.d.LastName);
				$("#txtFirstName").val(json.d.FirstName);
				$("#txtLastName").val(json.d.LastName);
				$("#txtEmail").val(json.d.EmailAddress);
				$("#txtMobile").val(json.d.MobileNumber);
				$("#txtHomePhone").val(json.d.HomeNumber);
				$("#txtBirthYear").val(json.d.BirthYear);
				
				$("#txtAddress1").val(json.d.Address1);
				$("#txtAddress2").val(json.d.Address2);
				$("#txtAddress3").val(json.d.Address3);
				$("#txtTown").val(json.d.Town);
				$("#txtCounty").val(json.d.County);
				$("#txtPostCode").val(json.d.PostCode);
				$("#lnkPostCode").attr("href", "http://maps.google.com/maps?saddr=" + json.d.PostCode);
				
				$("#txtOccupation").val(json.d.Occupation);
				$("#txtNOK").val(json.d.NextOfKin);
				$("#txtNOKAddress").val(json.d.NextOfKinAddress);
				$("#txtNOKPhone").val(json.d.NextOfKinPhone);
				
				$("#cboUserLevel").val(json.d.UserLevelID);
				$("#txtJoinDate").val(json.d.JoinDateString);
				$("#txtLeaveDate").val(json.d.LeaveDateString);
				$("#txtAssessmentDate").val(json.d.RiderAssesmentPassDateString);
				$("#txtAdQualDate").val(json.d.AdQualPassDateString);
				$("#txtGMPDate").val(json.d.LastGDPGMPDateString);
				$("#txtAdQualType").val(json.d.AdQualType);
				$("#txtNotes").val(json.d.Notes);
				
				for(var x = 0; x < json.d.Tags.length; x++)
				{
					if (json.d.Tags[x].TagName == "Rider") { $("#chkRider").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Driver") { $("#chkDriver").prop('checked', true); }
					if (json.d.Tags[x].TagName == "4x4") { $("#chk4x4").prop('checked', true); }
					if (json.d.Tags[x].TagName == "EmergencyList") { $("#chkEmergencyList").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Fundraiser") { $("#chkFundraiser").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Blood") { $("#chkBlood").prop('checked', true); }
					if (json.d.Tags[x].TagName == "AA") { $("#chkAA").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Milk") { $("#chkMilk").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Water") { $("#chkWater").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Controller") { $("#chkController").prop('checked', true); }
					if (json.d.Tags[x].TagName == "OnRota") { $("#chkOnRota").prop('checked', true); }
					if (json.d.Tags[x].TagName == "Committee") { $("#chkCommittee").prop('checked', true); }
				}
				
				$("#loading").slideUp();
				$("#entry").slideDown();
			}
			else
			{
				$("#loading").slideUp();
				$("#error").slideDown();
			}
		},
		function(json)
		{
			niceAlert("Something went wrong trying to show this member to you.  You may not have sufficient privileges to view other member's details.  If you believe you should, please contact Admin.");
			$("#loading").slideUp();
		}
	);
}

function DisplayLocation(locationId)
{
	callServerSide(
		"Service/Service.asmx/GetLocation", 
		"{'locationId':" + locationId + "}",
		function(json)
		{
			$("#lblTitle").text(json.d.LocationName);
			$("#txtLocationName").val(json.d.LocationName);
			$("#txtLat").val(json.d.Lat);
			$("#txtLng").val(json.d.Lng);
			$("#chkHospital").prop('checked', json.d.Hospital == 1);
			$("#chkChangeOver").prop('checked', json.d.ChangeOver == 1);
			$("#chkBloodBank").prop('checked', json.d.BloodBank == 1);
			$("#chkInNetwork").prop('checked', json.d.InNetwork == 1);
			$("#loading").slideUp();
			$("#entry").slideDown();

			initializeMap();
		},
		function()
		{
		}
	);
}

function SaveLocation(locationId)
{
	$("#loading").slideDown();
	$("#entry").slideUp();
	var json = JsonifyLocationFromForm(locationId);
	callServerSide(
		"Service/Service.asmx/SaveLocation", 
		json,
		function(json)
		{
			$("#loading").slideUp();
			$("#success").slideDown();
		},
		function()
		{
			$("#loading").slideUp();
			$("#error").slideDown();
		}
	);
}

function JsonifyLocationFromForm(locationId)
{
	return '{"location":{"LocationID":' + locationId + ',' + 
		'"LocationName":"' + $("#txtLocationName").val() + '","Hospital":"' + ($("#chkHospital").prop('checked') ? "1" : "0") + '",' + 
		'"Lat":"' + $("#txtLat").val() + '","Lng":"' + $("#txtLng").val() + '",' + 
		'"ChangeOver":"' + ($("#chkChangeOver").prop('checked')? "1" : "0") + '",' +
		'"BloodBank":"' + ($("#chkBloodBank").prop('checked')? "1" : "0") + '",' + 
		'"InNetwork":"' + ($("#chkInNetwork").prop('checked')? "1" : "0") + '", "Enabled":"1"}}';
}

function sendSMSMessage(numbers, message)
{
	$("#loading").slideDown();
	$("#entry").slideUp();
	callServerSide(
		"Service/Service.asmx/SendSMSMessage", 
		"{'numbers':'" + numbers + "', 'message':'"+ message + "', 'fromServ':'" + fromServ + "'}",
		function(json)
		{
			$("#loading").slideUp();
			$("#success").slideDown();
		},
		function()
		{
		}
	);
}

function getNumbersForTags(tags, target)
{
	callServerSide(
		"Service/Service.asmx/ListMobileNumbersWithTags", 
		"{'tagsCsv':'" + tags + "'}",
		function(json)
		{
			var nums = "";
			for(var x = 0; x < json.d.length; x++)
			{
				nums += json.d[x] + ",";
			}
			$("#" + target).val(nums);
			smsCount = json.d.length;
			$("#loading").slideUp();
			$("#lblCount").text(smsCount);
		},
		function()
		{
		}
	);
}


function listMembersWithTag(tag, callBack)
{
	callServerSideGet(
		"Service/Service.asmx/ListMembersWithTags?tagsCsv='" + tag + "'", 
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				members[members.length] = json.d[x];
				memberNames[memberNames.length] = json.d[x].LastName + ' ' + json.d[x].FirstName;
			}
			if (callBack != null) { callBack(); }
		},
		function()
		{
		},
		true
	);
}

function getMemberId(memberName)
{
	for(var x = 0; x < members.length; x++)
	{
		if ((members[x].LastName + ' ' + members[x].FirstName) == memberName)
		{
			return members[x].MemberID;
		}
	}
	return 0;
}

function getControllerId(controllerName)
{
	for(var x = 0; x < controllers.length; x++)
	{
		if ((controllers[x].LastName + ' ' + controllers[x].FirstName) == controllerName)
		{
			return controllers[x].MemberID;
		}
	}
	return 0;
}

function getControllerName(controllerId)
{
	for(var x = 0; x < controllers.length; x++)
	{
		if (controllers[x].MemberID == controllerId)
		{
			return controllers[x].LastName + ' ' + controllers[x].FirstName;
		}
	}
	return 0;
}

function getMemberName(memberId)
{
	for(var x = 0; x < members.length; x++)
	{
		if (members[x].MemberID == memberId)
		{
			return members[x].LastName + ' ' + members[x].FirstName;
		}
	}
	return "";
}

function getLocationId(locationName)
{
	for(var x = 0; x < locations.length; x++)
	{
		if (locations[x].LocationName == locationName)
		{
			return locations[x].LocationID;
		}
	}
	return 0;
}

function getLocationName(locationId)
{
	for(var x = 0; x < locations.length; x++)
	{
		if (locations[x].LocationID == locationId)
		{
			return locations[x].LocationName;
		}
	}
	return 0;
}

function GetSMSCreditCount(targetElementName)
{
	callServerSide(
		"Service/Service.asmx/GetSMSCreditCount", 
		"{}",
		function(json)
		{
			$("#" + targetElementName).text(json.d);
		},
		function()
		{
		}
	);
}

function listControllers(callBack)
{
	callServerSideGet(
		"Service/Service.asmx/ListMembersWithTags?tagsCsv='controller'", 
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				controllers[controllers.length] = json.d[x];
				controllerNames[controllerNames.length] = json.d[x].LastName + ' ' + json.d[x].FirstName;
			}
			if (callBack != null) { callBack(); }
		},
		function()
		{
		},
		true
	);
}

function writeMembersWithTagAsListItems(tag, target, onClick)
{
	callServerSide(
		"Service/Service.asmx/ListMembersWithTags", 
		"{'tagsCsv':'" + tag + "'}",
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				$("#" + target).append("<li id=\"" + target + x + "\"><a>" + json.d[x].FirstName + " " + json.d[x].LastName + "</a></li>");
				$("#" + target + x).click({param1: json.d[x]}, function(event) {
					onClick(event.data.param1.MemberID, event.data.param1.FirstName, event.data.param1.LastName);
				});
			}
		},
		function()
		{
		}
	);
}

function writeLocations(target, onClick)
{
	for(var x = 0; x < locations.length; x++)
	{
		$("#" + target).append("<li id=\"" + target + x + "\"><a>" + locations[x].LocationName + "</a></li>");
		$("#" + target + x).click({param1: locations[x]}, function(event) {
			onClick(event.data.param1.LocationID, event.data.param1.LocationName, event.data.param1.Hospital, event.data.param1.ChangeOver, event.data.param1.BloodBank);
		});
	}
}

function listLocations(callBack)
{
	callServerSideGet(
		"Service/Service.asmx/ListLocations", 
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				locations[locations.length] = json.d[x];
				locationNames[locationNames.length] = json.d[x].LocationName;
			}
			if (callBack != null) { callBack(); }
		},
		function()
		{
		},
		true
	);
}

function writeVehicleTypes(target, onClick)
{
	callServerSideGet(
		"Service/Service.asmx/ListVehicleTypes", 
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				$("#" + target).append("<li id=\"" + target + x + "\"><a>" + json.d[x].VehicleTypeName + "</a></li>");
				$("#" + target + x).click({param1: json.d[x]}, function(event) {
					onClick(event.data.param1.VehicleTypeID, event.data.param1.VehicleTypeName);
				});
			}
		},
		function()
		{
		},
		true
	);
}

function SearchMembers(userLevel, search, onlyActive)
{
	$("#results").text("");
	callServerSide(
		"Service/Service.asmx/SearchMembers", 
		"{'search':'" + search + "', 'onlyActive':" + onlyActive + "}",
		function(json)
		{
			var append = '<table class="table table-striped table-bordered table-condensed">' +
			'<thead><tr><th></th><th>First Name</th><th>Last Name</th><th>Mobile</th><th>Town</th><th>Tags</th><th>User Level</th></tr></thead><tbody>';
			for(var x = 0; x < json.d.length; x++)
			{
				var name = json.d[x].LastName
				if (userLevel >= 3)
				{
					name = '<a href="ViewMember.aspx?memberId=' + json.d[x].MemberID + '">' + name + '</a>';
				}
				var row="<tr><td>" + (x + 1) + "</td><td>" + json.d[x].FirstName + "</td>" +
					"<td>" + name + "</td>" + 
					//"<td>" + json.d[x].EmailAddress + "</td>" + 
					"<td nowrap>" + json.d[x].MobileNumber + "</td>" + 
					"<td>" + json.d[x].Town + "</td>" + 
					"<td nowrap><small>" + json.d[x].TagsText + "</small></td>" +
					"<td>" + json.d[x].UserLevelName + "</td>" +
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

function ListLocations(userLevel)
{
	callServerSideGet(
		"Service/Service.asmx/ListLocations", 
		function(json)
		{
			var append = '<table class="table table-striped table-bordered table-condensed">' +
			'<thead><tr><th></th><th>Location</th><th>Bloodbank</th><th>Handover</th><th>Hospital</th><th>Lat</th><th>Lng</th><th>In Network</th></tr></thead><tbody>';
			for(var x = 0; x < json.d.length; x++)
			{
				var name ='<a href="ViewLocation.aspx?locationId=' + json.d[x].LocationID + '">' + json.d[x].LocationName + '</a>';
				var row="<tr><td>" + (x + 1) + "</td><td>" + name + "</td>" +
					"<td>" + (json.d[x].BloodBank ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].ChangeOver ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].Hospital ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].Lat ? json.d[x].Lat.substr(0,8) : "???") + "</td>" + 
					"<td>" + (json.d[x].Lng ? json.d[x].Lng.substr(0,8) : "???") + "</td>" + 
					"<td>" + (json.d[x].InNetwork ? "X" : "") + "</td>" + 
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
		},
		false
	);
}

function addMemberTag(memberId, tag)
{
	$("#loading").slideDown();
	callServerSide(
		"Service/Service.asmx/TagMember", 
		"{'memberId':" + memberId + ", 'tagName': '" + tag + "'}",
		function(json)
		{
			$("#loading").slideUp();
		},
		function()
		{
			$("#loading").slideUp();
			$("#entry").slideUp();
			$("#error").slideDown();
		}
	);
}

function setMemberUserLevel(memberId, userLevel)
{
	$("#loading").slideDown();
	callServerSide(
		"Service/Service.asmx/SetMemberUserLevel", 
		"{'memberId':" + memberId + ", 'userLevelId': " + userLevel + "}",
		function(json)
		{
			$("#loading").slideUp();
		},
		function()
		{
			$("#loading").slideUp();
			$("#entry").slideUp();
			$("#error").slideDown();
		}
	);
}

function removeMemberTag(memberId, tag)
{
	$("#loading").slideDown();
	callServerSide(
		"Service/Service.asmx/UnTagMember", 
		"{'memberId':" + memberId + ", 'tagName': '" + tag + "'}",
		function(json)
		{
			$("#loading").slideUp();
		},
		function()
		{
			$("#loading").slideUp();
			$("#entry").slideUp();
			$("#error").slideDown();
		}
	);
}

function SaveBasicMember(memberId)
{
	$("#loading").slideDown();
	$("#entry").slideUp();
	var json = JsonifyBasicMemberFromForm(memberId);
	callServerSide(
		"Service/Service.asmx/SaveMember", 
		json,
		function(json)
		{
			$("#loading").slideUp();
			$("#success").slideDown();
		},
		function()
		{
			$("#loading").slideUp();
			$("#error").slideDown();
		}
	);
}

function JsonifyBasicMemberFromForm(memberId)
{
	return '{"member":{"MemberID":' + memberId + ',' + 
		'"FirstName":"' + $("#txtFirstName").val() + '","LastName":"' + $("#txtLastName").val() + '",' + 
		'"EmailAddress":"' + $("#txtEmail").val() + '","MobileNumber":"' + $("#txtMobile").val() + '",' + 
		'"HomeNumber":"' + $("#txtHomePhone").val() + '","Occupation":"' + $("#txtOccupation").val() + '",' +
		'"Address1":"' + $("#txtAddress1").val() + '","Address2":"' + $("#txtAddress2").val() + '",' +
		'"Address3":"' + $("#txtAddress3").val() + '",' + 
		'"Town":"' + $("#txtTown").val() + '","County":"' + $("#txtCounty").val() + '",' + 
		'"PostCode":"' + $("#txtPostCode").val() + '",' + 
		'"BirthYear":' + $("#txtBirthYear").val() + ',"NextOfKin":"' + $("#txtNOK").val() + '",' + 
		'"NextOfKinAddress":"' + $("#txtNOKAddress").val() + '",' +
		'"NextOfKinPhone":"' + $("#txtNOKPhone").val() + '", ' +
		'"JoinDateString":"' + $("#txtJoinDate").val() + '", ' +
		'"LeaveDateString":"' + $("#txtLeaveDate").val() + '", ' +
		'"RiderAssesmentPassDateString":"' + $("#txtAssessmentDate").val() + '", ' +
		'"AdQualPassDateString":"' + $("#txtAdQualDate").val() + '", ' +
		'"LastGDPGMPDateString":"' + $("#txtGMPDate").val() + '", ' +
		'"AdQualType":"' + $("#txtAdQualType").val() + '", ' +
		'"Notes":"' + $("#txtNotes").val() + '"}}';
}

function register()
{
	if(validateRegistrationForm())
	{
		$("#loading").slideDown();
		$("#entry").slideUp();
		var json = JsonifyBasicMemberFromForm(-1);
		callServerSide(
			"Service/Service.asmx/Register", 
			json,
			function(json)
			{
				var memberId = json.d;
				if (memberId > 0)
				{
					if ($("#chkRider").prop('checked') == true)
					{
						callServerSide("Service/Service.asmx/TagMember", "{'memberId':" + memberId + ", 'tagName': 'Rider'}", function(json){}, function(){} );
					}
					if ($("#chkDriver").prop('checked') == true)
					{
						callServerSide("Service/Service.asmx/TagMember", "{'memberId':" + memberId + ", 'tagName': 'Driver'}", function(json){}, function(){} );
					}
					if ($("#chk4x4").prop('checked') == true)
					{
						callServerSide("Service/Service.asmx/TagMember", "{'memberId':" + memberId + ", 'tagName': '4x4'}", function(json){}, function(){} );
					}
					if ($("#chkEmergencyList").prop('checked') == true)
					{
						callServerSide("Service/Service.asmx/TagMember", "{'memberId':" + memberId + ", 'tagName': 'EmergencyList'}", function(json){}, function(){} );
					}
					if ($("#chkFundraiser").prop('checked') == true)
					{
						callServerSide("Service/Service.asmx/TagMember", "{'memberId':" + memberId + ", 'tagName': 'Fundraiser'}", function(json){}, function(){} );
					}
					$("#loading").slideUp();
					$("#success").slideDown();
					niceAlert("Thank you " + $("#txtFirstName").val() + ". Someone will be in touch with you very soon to arrange the next steps.  The system will send you your password by email now.");
				}
				else
				{
					$("#loading").slideUp();
					$("#error").slideDown();
					$("#entry").slideDown();
					if (memberId == -1)
					{
						niceAlert("We already have a member with that email address!");
					}
				}
			},
			function()
			{
				$("#loading").slideUp();
				$("#error").slideDown();
				$("#entry").slideDown();
			}
		);
	}
}

function validateRegistrationForm()
{
	$("#error").slideUp();
	if ($("#chkAgree").prop('checked') != true)
	{
		niceAlert("Please confirm that your vehicle is safe & road legal and that you have read and understood the membership code of practice");
		return false;
	}
	if (
		$("#txtFirstName").val() == "" || $("#txtLastname").val() == "" ||
		$("#txtEmail").val() == "" || $("#txtMobile").val() == "" || $("#txtAddress1").val() == "" ||
		$("#txtPostCode").val() == "" || $("#txtTown").val() == "" || $("#txtBirthYear").val() == ""
	)
	{
		niceAlert("Please fill in all the fields");
		return false;
	}
	if ($("#txtMobile").val().substring(0,2) != "07")
	{
		niceAlert("Please enter a valid mobile number");
		return false;
	}
	if ($("#txtEmail").val().indexOf("@") == -1 || $("#txtEmail").val().indexOf(".") == -1)
	{
		niceAlert("Please enter a valid email address");
		return false;
	}
	return true;
}

function keepAlive()
{
	callServerSide(
		"Service/Service.asmx/KeepAlive", 
		"{}",
		function(json)
		{
			$("#icoSessionStatus").removeClass("icon-red");
			$("#icoSessionStatus").removeClass("icon-green");
			window.setTimeout('$("#icoSessionStatus").addClass("icon-green");',500);
			window.setTimeout("keepAlive()", 20000);
		},
		promptForLogin
	);
}

function promptForLogin()
{
	$("#icoSessionStatus").removeClass("icon-green");
	$("#icoSessionStatus").addClass("icon-red");
	niceAlert("Session error!! This should not happen, but it did.  Open a new tab and login to the SERV system again.  If you are attempting to make a sumbission, for example logging a run, you NEED to log back in before that will work.  There is no need to close this tab and lose your work.");
	window.setTimeout("keepAlive()", 40000);
}

function isValidTime(val)
{
	if (val.length != 5){ return false; }
	if (val.indexOf(":") == -1 && val.indexOf(".") == -1) { return false; }
	if (val.indexOf("-") != -1) { return false; }
	if (val.indexOf("+") != -1) { return false; }
	return true;
}

function niceAlert(msg)
{
	$("#alertMessage").text(msg);
	$("#alert").dialog({
		modal: true,
		dialogClass: "no-close",
		autoOpen: true,
		show: { effect: "shake", duration: 100 },
		hide: { effect: "slide", duration: 100 },
		buttons: 
		[
			{
			  text: "OK",
			  click: function() {
			    $( this ).dialog( "close" );
			  }
			}
		]
	});
}

function callServerSide(url, data, success, error, allowCache)
{
	allowCache = (typeof allowCache === "undefined") ? false : allowCache;
	$.ajax({
		type: "POST",
		url: url,
		data: data,
		async: asyncRequests,
		cache: allowCache,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: success,
		error: error
	});
}

function callServerSideGet(url, success, error, allowCache)
{
	allowCache = (typeof allowCache === "undefined") ? false : allowCache;
	$.ajax({
		type: "GET",
		url: url,
		async: asyncRequests,
		cache: allowCache,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: success,
		error: error
	});
}

function onMasterLoaded()
{
	var readOnly=false;
	if(readOnly)
	{
		$("#pnlReadOnly").slideDown();
		$(".readOnlyHidden").hide();
	}
}
