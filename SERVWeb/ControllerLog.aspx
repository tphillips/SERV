<%@ Page Language="C#" Inherits="SERVWeb.ControllerLog" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Controller Log</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<script language="JavaScript" src="js/JS.js"></script>

<div id="entry" style="display:none">

	<h3>Controller Log</h3>

	<div class="row">
		
		<div class="span12">

			<div class="btn-group">
				<button type="button" class="btn" disabled id="btnController">Select The Controller's Name (Probably You!)</button>
				<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
				<ul class="dropdown-menu" id="lstControllers">
					<!-- dropdown menu links -->
				</ul>

			</div>

			<h4>What sort of run are you recording?</h4>
			<div class="btn-group" data-toggle="buttons-radio">
			    <button type="button" class="btn" onclick="showBloodPanel()">Blood Run</button>
			    <button type="button" class="btn" onclick="showAAPanel()">Air Ambulance</button>
			    <button type="button" class="btn" onclick="showMilkPanel()">Milk Run</button>
			    <button type="button" class="btn" onclick="showWaterPanel()">Water Run</button>
			    <button type="button" class="btn" onclick="showBloodPanel()">Other</button>
			</div>
			<br/><br/>

		</div>

		<fieldset>

		<div id="AA" style="display:none">

			<div class="span12">

				<label>Rider / Driver:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Select the Rider's Name</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br>

				<label>Run Date:</label>
				<input type="text" id="txtAAShiftDate" class="date" />

				<label>Collect Time:</label>
				<input type="text" id="txtAARunDate" placeholder="HH:MM" />

				<label>Deliver Time:</label>
				<input type="text" id="txtAADeliverTime" placeholder="HH:MM" />

				<label>Returned Time:</label>
				<input type="text" id="txtAAReturnTime" placeholder="HH:MM" />

				<label>Vehicle:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Select the vehicle</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br>
			
			</div>

		</div>

		<div id="Milk" style="display:none">
			<div class="span12">
			</div>
		</div>

		<div id="Water" style="display:none">
			<div class="span12">
			</div>
		</div>

		<div id="blood" style="display:none">

			<div class="span4">

				<label>Shift Start Date:</label>
				<input type="text" id="txtShiftDate" class="date" />

				<label>Call Date:</label>
				<input type="text" id="txtCallDate" class="date" />

				<label>Call Time:</label>
				<input type="text" id="txtCallTime" class="time" placeholder="HH:MM" />

				<label>Call From:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled id="btnCaller">Select who called SERV NOW</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstCallers">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br>

				<label>Collected From:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnPickup" disabled>Where we picked up the consignment</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstPickups">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br/>

				<label>Taken To:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnDrop" disabled>Where we took the consignment to</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstDrops">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br/>

				<label>Final Destination:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnFinalDest" disabled>The consignment's final destination</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstFinalDests">
						<!-- dropdown menu links -->
					</ul>
				</div>

			</div>

			<div class="span4">

				<label>Consignment Origin:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnOrigin" disabled>Where the consignment originated</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstOrigins">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br>

				<label>Urgency:</label>
				<div class="btn-group">
					<button type="button" class="btn" onclick="urgency --; updateUrgency();">-</button>
					<button type="button" class="btn" id="btnUrgency" disabled>2
					</button><button type="button" class="btn" onclick="urgency ++; updateUrgency();">+</button>
				</div><br/><br/>

				<label>Consignment:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">Blood</button>
					<button type="button" class="btn" onclick="bloodBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnBloodBox" disabled>0
					</button><button type="button" class="btn" onclick="bloodBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">Plasma</button>
					<button type="button" class="btn" onclick="plasmaBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnPlasmaBox" disabled>0
					</button><button type="button" class="btn" onclick="plasmaBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">Platelets</button>
					<button type="button" class="btn" onclick="plateletsBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnPlateletsBox" disabled>0
					</button><button type="button" class="btn" onclick="plateletsBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">Sample</button>
					<button type="button" class="btn" onclick="sampleBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnSampleBox" disabled>0
					</button><button type="button" class="btn" onclick="sampleBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">Package</button>
					<button type="button" class="btn" onclick="packageBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnPackageBox" disabled>0
					</button><button type="button" class="btn" onclick="packageBox ++; updateBoxCounts();">+</button>
				</div>
				<br/><br/>

			</div>

			<div class="span4">

				<label>Rider / Driver:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnRider" disabled>Allocated Rider's Name</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstRiders">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br/>

				<label>Vehicle:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled id="btnVehicle">Select the vehicle</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstVehicles">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br/>

				<label>Pickup Date:</label>
				<input type="text" id="txtPickupDate" class="date" />

				<label>Pickup Time:</label>
				<input type="text" id="txtPickupTime" placeholder="HH:MM" />

				<label>Delivery / Exchange Date:</label>
				<input type="text" id="txtDeliverDate" class="date" />

				<label>Delivery / Exchange Time:</label>
				<input type="text" id="txtDeliverTime" placeholder="HH:MM" />

			</div>

		</div>

		</fieldset>

	</div>
	<hr/>
	<button type=button class="btn btn-primary btn-lg" onclick="saveRun()">Save Run</button>

	<div id="alert" style="display:none" title="SERV">
		<p><span id="alertMessage">Default message</span></p>
	</div>

</div>

<script>

	var bloodBox = 0;
	var plasmaBox = 0;
	var plateletsBox=0
	var sampleBox=0;
	var packageBox=0;

	var urgency=2;

	var runType = "";
	
	var controllerId=0;
	var riderId=0;
	var callerLocationId=0;
	var originLocationId=0;
	var pickupLocationId=0;
	var dropLocationId=0;
	var finalLocationId=0;
	var vehicleId=0;

    $(function() 
	{
		$(".date").datepicker({ dateFormat: 'dd M yy' });
		listControllers();
		listRiders();
		listLocations();
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
				ret += "1,";
			}
		}
		if (plasmaBox > 0)
		{
			for (var x = 0; x < plasmaBox; x++)
			{
				ret += "2,";
			}
		}
		if (plateletsBox > 0)
		{
			for (var x = 0; x < plateletsBox; x++)
			{
				ret += "3,";
			}
		}
		if (sampleBox > 0)
		{
			for (var x = 0; x < sampleBox; x++)
			{
				ret += "4,";
			}
		}
		if (packageBox > 0)
		{
			for (var x = 0; x < packageBox; x++)
			{
				ret += "5,";
			}
		}
		return ret;
	}

	function saveRun()
	{
		if (validate())
		{
			var json = "{'callDateTime':'" + $("#txtCallDate").val() + " " + $("#txtCallTime").val() + 
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
				"', 'productIdCsv':'" + productCsv() + "'}";
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
	}

	function validate()
	{
		if (runType == "")
		{
			niceAlert("You need to choose a Run Type");	return false;
		}
		if (controllerId == 0)
		{
			niceAlert("You need to choose a Controller"); return false;
		}
		if (riderId == 0)
		{
			niceAlert("You need to choose a Rider"); return false;
		}
		if (vehicleId == 0)
		{
			niceAlert("What did the rider / driver travel on or in?"); return false;
		}
		if (runType=="blood")
		{
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
			if (callerLocationId == 0)
			{
				niceAlert("Who called SERV NOW?"); return false;
			}
			if (originLocationId == 0)
			{
				niceAlert("What was the consignments origin? (This may not be where we collected from)"); return false;
			}
			if (pickupLocationId == 0)
			{
				niceAlert("Where did we pickup from?"); return false;
			}
			if (dropLocationId == 0)
			{
				niceAlert("Where did we drop off?"); return false;
			}
			if (finalLocationId == 0)
			{
				niceAlert("What was the consignments final destination? (This may not be where we dropped off)"); return false;
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
		if (urgency < 1) { urgency = 1; }
		if (urgency > 3) { urgency = 3; }
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

	function listControllers()
	{
		writeMembersWithTagAsListItems("Controller", "lstControllers", controllerSelected);
	}

	function listRiders()
	{
		writeMembersWithTagAsListItems("Rider,Driver,Blood", "lstRiders", riderSelected);
	}

	function listLocations()
	{
		writeLocations("lstCallers", callerSelected);
		// relying on jQuery ajax caching here . . .
		writeLocations("lstOrigins", originSelected);
		writeLocations("lstPickups", pickupSelected);
		writeLocations("lstDrops", dropSelected);
		writeLocations("lstFinalDests", finalDestSelected);
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

	function callerSelected(locationId, locationName)
	{
		$("#btnCaller").text(locationName);
		callerLocationId = locationId;
	}

	function originSelected(locationId, locationName)
	{
		$("#btnOrigin").text(locationName);
		originLocationId = locationId;
	}

	function pickupSelected(locationId, locationName)
	{
		$("#btnPickup").text(locationName);
		pickupLocationId = locationId;
	}

	function dropSelected(locationId, locationName)
	{
		$("#btnDrop").text(locationName);
		dropLocationId = locationId;
	}

	function finalDestSelected(locationId, locationName)
	{
		$("#btnFinalDest").text(locationName);
		finalLocationId = locationId;
	}

	function controllerSelected(memberId, firstName, lastName)
	{
		$("#btnController").text(firstName + " " + lastName);
		controllerId = memberId;
	}

	function riderSelected(memberId, firstName, lastName)
	{
		$("#btnRider").text(firstName + " " + lastName);
		riderId = memberId;
	}

</script>

</asp:Content>


