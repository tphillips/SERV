// Controller logging support js

// Product type constants
var BLOOD			= 1;
var PLATELETS		= 2;
var PLASMA			= 3;
var SAMPLE			= 4;
var HUMAN_MILK		= 5;
var WATER_SAMPLE	= 6;
var RH1			 	= 7;
var RH2			 	= 8;
var RH3			 	= 9;
var RH4			 	= 10;
var RH5			 	= 11;
var RH6			 	= 12;
var RH7			 	= 13;
var RH8			 	= 14;
var DRUGS			= 15;
var PACKAGE			= 16;
var OTHER			= 17;

var DEFAULT_URGENCY = 2;
var MAX_URGENCY = 3;
var MIN_URGENCY = 1;

var bloodBox = 0;
var plasmaBox = 0;
var plateletsBox = 0
var sampleBox = 0;
var packageBox = 0;

var urgency = DEFAULT_URGENCY;

var runType = "";

var controllerId = 0;
var riderId = 0;
var callerLocationId = 0;
var originLocationId = 0;
var pickupLocationId = 0;
var dropLocationId = 0;
var finalLocationId = 0;
var vehicleId = 0;

var locations = new Array();
var locationNames = new Array();
var members = new Array();
var memberNames = new Array();
var controllers = new Array();
var controllerNames = new Array();

$(function() 
{
	$(".date").datepicker({ dateFormat: 'dd M yy' });
	$( ".locations" ).autocomplete({ source: locationNames });
	$( ".controllers" ).autocomplete({ source: controllerNames });
	$( ".riders" ).autocomplete({ source: memberNames });
	listControllers(null);
	listMembersWithTag("Rider,Driver,Blood", null);
	listLocations(null);
	listVehicleTypes();
	$("#loading").slideUp();
	$("#entry").slideDown();
});

function productCsv()
{
	var ret = "";
	if (bloodBox > 0)
	{
		for (var x = 0; x < bloodBox; x++)
		{
			ret += BLOOD + ",";
		}
	}
	if (plasmaBox > 0)
	{
		for (var x = 0; x < plasmaBox; x++)
		{
			ret += PLASMA + ",";
		}
	}
	if (plateletsBox > 0)
	{
		for (var x = 0; x < plateletsBox; x++)
		{
			ret += PLATELETS + ",";
		}
	}
	if (sampleBox > 0)
	{
		for (var x = 0; x < sampleBox; x++)
		{
			ret += SMAPLE + ",";
		}
	}
	if (packageBox > 0)
	{
		for (var x = 0; x < packageBox; x++)
		{
			ret += PACKAGE + ",";
		}
	}
	return ret;
}

function saveRun()
{
	if (validate())
	{
		if (runType == "blood")
		{
			saveBloodRun();
		}
	}
}

function saveBloodRun()
{
	var json = 
		"{" +
			"'callDateTime':'" + $("#txtCallDate").val() + " " + $("#txtCallTime").val() + 
			"', 'callFromLocationId':'" + callerLocationId + 
			"', 'collectDateTime':'" + $("#txtPickupDate").val() + " " + $("#txtPickupTime").val() + 
			"', 'collectionLocationId':'" + pickupLocationId + 
			"', 'controllerMemberId':'" + controllerId + 
			"', 'deliverDateTime':'" + $("#txtDeliverDate").val() + " " + $("#txtDeliverTime").val() + 
			"', 'deliverToLocationId':'" + dropLocationId + 
			"', 'dutyDate':'" + $("#txtShiftDate").val() + 
			"', 'finalDestinationLocationId':'" + finalLocationId + 
			"', 'originLocationId':'" + originLocationId + 
			"', 'riderMemberId':'" + riderId + 
			"', 'urgency':'" + urgency + 
			"', 'vehicleTypeId':'" + vehicleId + 
			"', 'productIdCsv':'" + productCsv() + 
		"'}";
	$("#loading").slideDown();
	$("#entry").slideUp();
	// Hit it
	callServerSide(
		"Service/Service.asmx/LogRun", 
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

function validate()
{

	if (runType == "")
	{
		niceAlert("You need to choose a Run Type");	return false;
	}
	controllerId = getControllerId($("#txtController").val())
	if (controllerId == 0)
	{
		niceAlert("You need to choose a Controller.  You MUST choose an item from the list or type it exactly."); return false;
	}
	if (runType=="blood")
	{
		riderId = getMemberId($("#txtRider").val())
		if (riderId == 0)
		{
			niceAlert("You need to choose a Rider.  You MUST choose an item from the list or type it exactly."); return false;
		}
		if (vehicleId == 0)
		{
			niceAlert("What did the rider / driver travel on or in?"); return false;
		}
		if ($("#txtShiftDate").val() == "") 
		{
			niceAlert("What shift date are you logging against?"); return false;
		}
		if ($("#txtCallDate").val() == "") 
		{
			niceAlert("What date did the call come in?"); return false;
		}
		if ($("#txtDeliverDate").val() == "") 
		{
			niceAlert("What date did we deliver?"); return false;
		}
		if ($("#txtPickupDate").val() == "") 
		{
			niceAlert("What date did we pickup?"); return false;
		}
		if ($("#txtCallTime").val() == "") 
		{
			niceAlert("What time did the call come in?"); return false;
		}
		if (bloodBox + plasmaBox + plateletsBox + sampleBox + packageBox == 0)
		{
			niceAlert("What did the rider / driver carry?"); return false;
		}
		callerLocationId = getLocationId($("#txtCaller").val());
		if (callerLocationId == 0)
		{
			niceAlert("Who called SERV NOW?  You MUST choose an item from the list or type it exactly."); return false;
		}
		originLocationId = getLocationId($("#txtOrigin").val());
		if (originLocationId == 0)
		{
			niceAlert("What was the consignments origin? (This may not be where we collected from)  You MUST choose an item from the list or type it exactly."); return false;
		}
		pickupLocationId = getLocationId($("#txtPickup").val());
		if (pickupLocationId == 0)
		{
			niceAlert("Where did we pickup from?  You MUST choose an item from the list or type it exactly."); return false;
		}
		dropLocationId = getLocationId($("#txtDrop").val());
		if (dropLocationId == 0)
		{
			niceAlert("Where did we drop off?  You MUST choose an item from the list or type it exactly."); return false;
		}
		finalLocationId = getLocationId($("#txtFinalDest").val());
		if (finalLocationId == 0)
		{
			niceAlert("What was the consignments final destination? (This may not be where we dropped off).  You MUST choose an item from the list or type it exactly."); return false;
		}
		if (!isValidTime($("#txtCallTime").val()))
		{
			niceAlert("Please use 24 hour HH:MM time formats (Call Time)"); return false;
		}
		if (!isValidTime($("#txtPickupTime").val()))
		{
			niceAlert("Please use 24 hour HH:MM time formats (Pickup Time)"); return false;
		}
		if (!isValidTime($("#txtDeliverTime").val()))
		{
			niceAlert("Please use 24 hour HH:MM time formats (Deliver Time)"); return false;
		}
	}
	return true;
}

function updateUrgency()
{
	if (urgency < MIN_URGENCY) { urgency = MIN_URGENCY; }
	if (urgency > MAX_URGENCY) { urgency = MAX_URGENCY; }
	$("#btnUrgency").text(urgency);
}

function updateBoxCounts()
{
	if (bloodBox < 0) { bloodBox = 0; }
	if (plasmaBox < 0) { plasmaBox = 0; }
	if (plateletsBox < 0) { plateletsBox = 0; }
	if (sampleBox < 0) { sampleBox = 0; }
	if (packageBox < 0) { packageBox = 0; }
	$("#btnBloodBox").text(bloodBox);
	$("#btnPlasmaBox").text(plasmaBox);
	$("#btnPlateletsBox").text(plateletsBox);
	$("#btnSampleBox").text(sampleBox);
	$("#btnPackageBox").text(packageBox);
}

function showBloodPanel()
{
	runType="blood";
	$("#blood").slideUp();
	$("#AA").slideUp();
	$("#Water").slideUp();
	$("#Milk").slideUp();
	$("#blood").slideDown();
}

function showAAPanel()
{
	runType="aa";
	$("#blood").slideUp();
	$("#AA").slideUp();
	$("#Water").slideUp();
	$("#Milk").slideUp();
	$("#AA").slideDown();
}

function showMilkPanel()
{
	runType="milk";
	$("#blood").slideUp();
	$("#AA").slideUp();
	$("#Water").slideUp();
	$("#Milk").slideUp();
	$("#Milk").slideDown();
}

function showWaterPanel()
{
	runType="water";
	$("#blood").slideUp();
	$("#AA").slideUp();
	$("#Water").slideUp();
	$("#Milk").slideUp();
	$("#Water").slideDown();
}

function listVehicleTypes()
{
	writeVehicleTypes("lstVehicles", vehicleSelected);
}

function vehicleSelected(vehicleTypeId, vehicleType)
{
	$("#btnVehicle").text(vehicleType);
	vehicleId = vehicleTypeId;
}
	
