var asyncRequests = true;
var FullName = "";

function getCalInfo(cal, onResult)
{
	$.ajax(
	{
		url : cal,
		dataType: "text",
		success : function (data) {
			var res = "";
			data = data.replace(/       /g, "\n");
			var parts = data.split("\n");
			var first = true;
			for (i = 0; i < parts.length; i ++) 
			{
				var part = $.trim(parts[i]); 
				if (part != "")
				{
					part = part.replace(/       /g, "<br/>");
					part = part.replace(/!/g, "<span style='color:red'>");
					part = part.replace(/\*/g, "<span style='color:blue'>");
					part = part.replace(/\Controller 1/g, "<span style='color:blue'>Controller 1");
					part = part.replace(/\Night 1/g, "<span style='color:Purple'>Night 1");
					part = part.replace(/\Night 2/g, "<span style='color:Orange'>Night 2");
					part = part.replace(/\Day 1/g, "<span style='color:Green'>Day 1");
					part = part.replace(FullName, "<span style='font-size:medium; font-weight:bold; text-style:flash'>" + FullName);
					if (first)
					{
						first = false;
						res = "<h4><i class=\"icon-calendar\"> </i> " + part + "</h4><ul>";
					}
					else
					{
				    	res = res + "<li>" + part +  "</li>";
				    }
			    }
			}
			res = res + "</ul>";
		    onResult(res);
		}
	});
}

function showCals()
{
	getCalInfo("bloodCalendarInclude0.htm", 
		function(res)
		{
			$('#pnlBloodCal').html(res);
		}
	);

	getCalInfo("bloodCalendarInclude1.htm", 
		function(res)
		{
			$('#pnlBloodCal1').html(res);
		}
	);

	getCalInfo("bloodCalendarInclude2.htm", 
		function(res)
		{
			$('#pnlBloodCal2').html(res);
		}
	);

	getCalInfo("bloodCalendarInclude3.htm", 
		function(res)
		{
			$('#pnlBloodCal3').html(res);
		}
	);

	getCalInfo("aaCalendarInclude0.htm", 
		function(res)
		{
			$('#pnlAACal').html(res);
		}
	);

	getCalInfo("aaCalendarInclude1.htm", 
		function(res)
		{
			$('#pnlAACal1').html(res);
		}
	);

	getCalInfo("aaCalendarInclude2.htm", 
		function(res)
		{
			$('#pnlAACal2').html(res);
		}
	);

	getCalInfo("aaCalendarInclude3.htm", 
		function(res)
		{
			$('#pnlAACal3').html(res);
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
			}
			
			$("#loading").slideUp();
			$("#entry").slideDown();
			
		},
		function()
		{
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
		'"ChangeOver":"' + ($("#chkChangeOver").prop('checked')? "1" : "0") + '","BloodBank":"' + ($("#chkBloodBank").prop('checked')? "1" : "0") + '", "Enabled":"1"}}';
}

function sendSMSMessage(numbers, message)
{
	$("#loading").slideDown();
	$("#entry").slideUp();
	callServerSide(
		"Service/Service.asmx/SendSMSMessage", 
		"{'numbers':'" + numbers + "', 'message':'"+ message + "'}",
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
	callServerSide(
		"Service/Service.asmx/ListMembersWithTags", 
		"{'tagsCsv':'" + tag + "'}",
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
		}
	);
}

function listControllers(callBack)
{
	callServerSide(
		"Service/Service.asmx/ListMembersWithTags", 
		"{'tagsCsv':'controller'}",
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
		}
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
	callServerSide(
		"Service/Service.asmx/ListLocations", 
		"{}",
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
		}
	);
}

function writeVehicleTypes(target, onClick)
{
	callServerSide(
		"Service/Service.asmx/ListVehicleTypes", 
		"{}",
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
		}
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
			'<thead><tr><th></th><th>First Name</th><th>Last Name</th><th>Mobile</th><th>Town</th></tr></thead><tbody>';
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
					"<td>" + json.d[x].MobileNumber + "</td>" + 
					"<td>" + json.d[x].Town + "</td>" + 
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
	callServerSide(
		"Service/Service.asmx/ListLocations", 
		"{}",
		function(json)
		{
			var append = '<table class="table table-striped table-bordered table-condensed">' +
			'<thead><tr><th></th><th>Location</th><th>Bloodbank</th><th>Handover</th><th>Hospital</th><th>Lat</th><th>Lng</th></tr></thead><tbody>';
			for(var x = 0; x < json.d.length; x++)
			{
				var name ='<a href="ViewLocation.aspx?locationId=' + json.d[x].LocationID + '">' + json.d[x].LocationName + '</a>';
				var row="<tr><td>" + (x + 1) + "</td><td>" + name + "</td>" +
					"<td>" + (json.d[x].BloodBank ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].ChangeOver ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].Hospital ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].Lat ? json.d[x].Lat.substr(0,8) : "???") + "</td>" + 
					"<td>" + (json.d[x].Lng ? json.d[x].Lng.substr(0,8) : "???") + "</td>" + 
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

function callServerSide(url, data, success, error)
{
	$.ajax({
		type: "POST",
		url: url,
		data: data,
		async: asyncRequests,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: success,
		error: error
	});
}
