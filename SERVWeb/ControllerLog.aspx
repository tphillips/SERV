<%@ Page Language="C#" Inherits="SERVWeb.ControllerLog" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Controller Log</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<script language="JavaScript" src="js/ControllerLog.js"></script>

<div id="entry" style="display:none">

	<h3>Controller Log</h3>

	<div class="row">
		
		<div class="span12">

			<input type="text" id="txtController" class="controllers" placeholder="Choose the controller" />

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

			<div class="span6">

				<label>Rider / Driver:</label>
				<input type="text" id="txtAaRider" class="riders" placeholder="Choose the rider / driver" />

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

			<div class="span6">

				<label>Out:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="bloodBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnBloodBox" disabled>0
					</button><button type="button" class="btn" onclick="bloodBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="bloodBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnBloodBox" disabled>0
					</button><button type="button" class="btn" onclick="bloodBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>

				<label>Back:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="bloodBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnBloodBox" disabled>0
					</button><button type="button" class="btn" onclick="bloodBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="bloodBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnBloodBox" disabled>0
					</button><button type="button" class="btn" onclick="bloodBox ++; updateBoxCounts();">+</button>
				</div><br/><br/>

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
				<input type="text" id="txtCaller" class="locations" placeholder="Type and Choose" onblur="callerSelected();"/>

				<label>Collected From:</label>
				<input type="text" id="txtPickup" class="locations" placeholder="Type and Choose" onblur="collectedFromSelected();"/>

				<label>Taken To:</label>
				<input type="text" id="txtDrop" class="locations" placeholder="Type and Choose" onblur="takenToSelected();"/>

				<label>Final Destination:</label>
				<input type="text" id="txtFinalDest" class="locations" placeholder="Type and Choose" />

			</div>

			<div class="span4">

				<label>Consignment Origin:</label>
				<input type="text" id="txtOrigin" class="locations" placeholder="Type and Choose" />

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
				<input type="text" id="txtRider" class="riders" placeholder="Choose the rider / driver" />

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

</div>

<script>
	function showCurrentController() 
	{
		$("#txtController").val("<%=this.MemberName%>");
	}
</script>

</asp:Content>


