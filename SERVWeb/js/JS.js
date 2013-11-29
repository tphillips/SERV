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
	callServerSide(
		"Service/Service.asmx/ListLocations", 
		"{}",
		function(json)
		{
			for(var x = 0; x < json.d.length; x++)
			{
				$("#" + target).append("<li id=\"" + target + x + "\"><a>" + json.d[x].LocationName + "</a></li>");
				$("#" + target + x).click({param1: json.d[x]}, function(event) {
					onClick(event.data.param1.LocationID, event.data.param1.LocationName);
				});
			}
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
			'<thead><tr><th></th><th>Location</th><th>Blood Bank</th><th>Change Over</th><th>Hospital</th><th>Lat</th><th>Lng</th></tr></thead><tbody>';
			for(var x = 0; x < json.d.length; x++)
			{
				var name = json.d[x].LocationName
				if (userLevel >= 3)
				{
					name = '<a href="ViewLocation.aspx?locationId=' + json.d[x].LocationID + '">' + name + '</a>';
				}
				var row="<tr><td>" + (x + 1) + "</td><td>" + name + "</td>" +
					"<td>" + (json.d[x].BloodBank ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].ChangeOver ? "X" : "") + "</td>" + 
					"<td>" + (json.d[x].Hospital ? "X" : "") + "</td>" + 
					"<td>" + json.d[x].Lat + "</td>" + 
					"<td>" + json.d[x].Lng + "</td>" + 
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

function isValidTime(val)
{
	if (val.length != 5){ return false; }
	return true;
}

function niceAlert(msg)
{
	alert(msg);
	/*
	$("#alertMessage").text(msg);
	$("#alert").dialog({
	  autoOpen: true,
	  show: { effect: "slide", duration: 200 },
	  hide: { effect: "slide", duration: 200 }
	});
*/
}

function callServerSide(url, data, success, error)
{
	$.ajax({
		type: "POST",
		url: url,
		data: data,
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		success: success,
		error: error
	});
}
