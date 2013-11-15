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
				<input type="text" id="txtCallTime" placeholder="HH:MM" />

				<label>Call From:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled id="btnCaller">Select who called SERV NOW</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstCallers">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br>

				<label>Rider / Driver:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnRider" disabled>Allocated Rider's Name</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstRiders">
						<!-- dropdown menu links -->
					</ul>
				</div>

			</div>

			<div class="span4">

				<label>Consignment:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Blood</button><button type="button" class="btn">-</button><button type="button" class="btn" disabled>0</button><button type="button" class="btn">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Plasma</button><button type="button" class="btn">-</button><button type="button" class="btn" disabled>0</button><button type="button" class="btn">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Platelets</button><button type="button" class="btn">-</button><button type="button" class="btn" disabled>0</button><button type="button" class="btn">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Sample</button><button type="button" class="btn">-</button><button type="button" class="btn" disabled>0</button><button type="button" class="btn">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Package</button><button type="button" class="btn">-</button><button type="button" class="btn" disabled>0</button><button type="button" class="btn">+</button>
				</div>
				<br/><br/>

				<label>Vehicle:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled>Select the vehicle</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu">
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

				<label>Collected From:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnPickup" disabled>Where we picked up the consignment</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstPickups">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br/>

				<label>Pickup Time:</label>
				<input type="text" id="txtPickupTime" placeholder="HH:MM" />

				<label>Taken To:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnDrop" disabled>Where we took the consignment to</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstDrops">
						<!-- dropdown menu links -->
					</ul>
				</div>
				<br/><br/>

				<label>Delivery Time:</label>
				<input type="text" id="txtDeliverTime" placeholder="HH:MM" />

				<label>Final Destination:</label>
				<div class="btn-group">
					<button type="button" class="btn" id="btnFinalDest" disabled>The consignment's final destination</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstFinalDests">
						<!-- dropdown menu links -->
					</ul>
				</div>

			</div>

		</div>

		</fieldset>

	</div>
	<hr/>
	<button type=button class="btn btn-primary btn-lg" onclick="SaveMember()">Save</button>

</div>

<script>

    $(function() 
	{
		$(".date").datepicker();
		listControllers();
		listRiders();
		listLocations();
		$("#loading").slideUp();
		$("#entry").slideDown();
	});

	function showBloodPanel()
	{
		$("#blood").slideUp();
		$("#AA").slideUp();
		$("#Water").slideUp();
		$("#Milk").slideUp();
		$("#blood").slideDown();
	}

	function showAAPanel()
	{
		$("#blood").slideUp();
		$("#AA").slideUp();
		$("#Water").slideUp();
		$("#Milk").slideUp();
		$("#AA").slideDown();
	}

	function showMilkPanel()
	{
		$("#blood").slideUp();
		$("#AA").slideUp();
		$("#Water").slideUp();
		$("#Milk").slideUp();
		$("#Milk").slideDown();
	}

	function showWaterPanel()
	{
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
		writeLocations("lstOrigins", originSelected);
		writeLocations("lstPickups", pickupSelected);
		writeLocations("lstDrops", dropSelected);
		writeLocations("lstFinalDests", finalDestSelected);
	}

	function callerSelected(locationId, locationName)
	{
		$("#btnCaller").text(locationName);
	}

	function originSelected(locationId, locationName)
	{
		$("#btnOrigin").text(locationName);
	}

	function pickupSelected(locationId, locationName)
	{
		$("#btnPickup").text(locationName);
	}

	function dropSelected(locationId, locationName)
	{
		$("#btnDrop").text(locationName);
	}

	function finalDestSelected(locationId, locationName)
	{
		$("#btnFinalDest").text(locationName);
	}

	function controllerSelected(memberId, firstName, lastName)
	{
		$("#btnController").text(firstName + " " + lastName);
	}

	function riderSelected(memberId, firstName, lastName)
	{
		$("#btnRider").text(firstName + " " + lastName);
	}

</script>

</asp:Content>


