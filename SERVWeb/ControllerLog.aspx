<%@ Page Language="C#" Inherits="SERVWeb.ControllerLog" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Controller Log</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<%=SERVGlobal.ControllerLogJSInclude()%>	

<div id="entry" style="display:none">

	<div class="alert" id="editWarn" style="display:none">
		<button type="button" class="close" data-dismiss="alert">&times;</button>
		<strong>CAUTION!</strong> You are <strong>editing an existing run</strong>!! Are you sure that is what you want?<br/>
		When you save your changes, the run will be assigned a new ID.  The old run ID will be deleted.</strong>
	</div>

	<div class="alert" id="readOnlyWarn" style="display:none">
		<button type="button" class="close" data-dismiss="alert">&times;</button>
		<strong>Read Only:</strong> You are unable to save changes to this run as you are not the controller who logged it, nor are you an administrator.</strong>
	</div>

	<h3>Controller Log</h3>

	<div class="row">
		<div class="span4 input-prepend">
			<button type="button" class="btn" onclick="" id="btnBloodRun" disabled>Controller</button>
			<input type="text" id="txtController" style="width:150px" class="controllers" placeholder="Choose the controller" /> 
		</div>
		<div class="span4 btn-group text-right" style="display:none">
			<button type="button" class="btn" onclick="showBloodPanel()" id="btnBloodRun">Blood Run / Other</button>
		    <button type="button" class="btn" onclick="showAAPanel()" id="btnAARun">Air Ambulance</button>
		</div>
	</div>
	<br/>
	
	<div class="row">

		<fieldset>

		<div id="AA" style="display:none">

			<div class="span4">

				<label>Rider / Driver:</label>
				<input type="text" id="txtAARider" class="riders" placeholder="Choose the rider / driver" />

				<label>Run Date:</label>
				<input type="text" id="txtAAShiftDate" class="date" />

				<label>Collect Time:</label>
				<input type="text" id="txtAAPickupTime" placeholder="HH:MM" />

				<label>Deliver Time:</label>
				<input type="text" id="txtAADeliverTime" placeholder="HH:MM" />

				<label>Returned Time:</label>
				<input type="text" id="txtAAReturnTime" placeholder="HH:MM" />

				<label>Vehicle:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled id="btnAAVehicle">Select the vehicle</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstAAVehicles">
					</ul>
				</div>
				<br/><br/>
			
			</div>

			<div class="span4">

				<label>Out:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="outBox1 --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnOutBox1" disabled>0
					</button><button type="button" class="btn" onclick="outBox1 ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="outBox2 --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnOutBox2" disabled>0
					</button><button type="button" class="btn" onclick="outBox2 ++; updateBoxCounts();">+</button>
				</div><br/><br/>

				<label>Back:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="inBox1 --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnInBox1" disabled>0
					</button><button type="button" class="btn" onclick="inBox1 ++; updateBoxCounts();">+</button>
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">RH</button>
					<button type="button" class="btn" onclick="inBox2 --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnInBox2" disabled>0
					</button><button type="button" class="btn" onclick="inBox2 ++; updateBoxCounts();">+</button>
				</div><br/><br/>

				<label>Notes:</label>
				<textarea id="txtAANotes" maxlength="599"></textarea>

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
				<input type="text" id="txtShiftDate" class="date" data-bind="value: vm.DutyDateString" placeholder="Select a Date"/>

				<label>Call Date &amp; Time:</label>
				<div class="input-append">
					<input type="text" id="txtCallDate" class="date" style="width:125px" data-bind="value: vm.CallDate" placeholder="Select a Date"/>
					<input type="text" class="add-on" id="txtCallTime" style="width:70px;background-color:white" class="time" placeholder="HH:MM" data-bind="value: vm.CallTime"/>
				</div>

				<label>Call From:</label>
				<input type="text" id="txtCaller" class="locations" placeholder="Type and Choose" onblur="callerSelected();"/>

				<label>Consignment Origin:</label>
				<input type="text" id="txtOrigin" class="locations" placeholder="Type and Choose" onblur="originSelected();"/>

				<label>Collected From:</label>
				<input type="text" id="txtPickup" class="locations" placeholder="Type and Choose" onblur="collectedFromSelected();"/>

				<label>Taken To:</label>
				<input type="text" id="txtDrop" class="locations" placeholder="Type and Choose" onblur="takenToSelected();"/>

				<label>Final Destination:</label>
				<input type="text" id="txtFinalDest" class="locations" placeholder="Type and Choose" />

			</div>

			<div class="span4">

				<label>Callers Number:</label>
				<div class="input-append">
					<input type="text" id="txtCallerNumber" style="width:125px" placeholder="Caller Telephone" data-bind="value: vm.CallerNumber" /> 
					<input type="text" class="add-on" style="width:70px;background-color:white" id="txtCallerExt" placeholder="Ext / Bleep" data-bind="value: vm.CallerExt" />
				</div>

				<label>Urgency:</label>
				<div class="btn-group">
					<button type="button" id="btnUrgency1" class="btn" onclick="urgency =1; updateUrgency();">1</button>
					<button type="button" id="btnUrgency2" disabled class="btn" onclick="urgency =2; updateUrgency();">2</button>
					<button type="button" id="btnUrgency3" class="btn" onclick="urgency =3; updateUrgency();">3</button>
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
				</div><br/><br/>
				<div class="btn-group">
					<button type="button" class="btn" disabled style="width:90px;">Milk</button>
					<button type="button" class="btn" onclick="milkBox --; updateBoxCounts();">-</button>
					<button type="button" class="btn" id="btnMilkBox" disabled>0
					</button><button type="button" class="btn" onclick="milkBox ++; updateBoxCounts();">+</button>
				</div>
				<br/><br/>

			</div>

			<div class="span4">

				<label>Rider / Driver:</label>
				<input type="text" id="txtRider" class="riders" placeholder="Choose the rider / driver" onblur="riderSelected()"/>

				<label>Vehicle:</label>
				<div class="btn-group">
					<button type="button" class="btn" disabled id="btnVehicle">Select the vehicle</button>
					<a class="btn dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a>
					<ul class="dropdown-menu" id="lstVehicles">
					</ul>
				</div>
				<br/><br/>

				<label>Pickup Date &amp; Time:</label>
				<div class="input-append">
					<input type="text" style="width:125px;" id="txtPickupDate" class="date" data-bind="value: vm.CollectDate" placeholder="Select a Date"/>
					<input type="text" class="add-on" style="width:70px;background-color:white" id="txtPickupTime" placeholder="HH:MM" data-bind="value: vm.CollectTime" />
				</div>

				<label>Delivery / Exchange Date &amp; Time:</label>
				<div class="input-append">
					<input type="text" style="width:125px;" id="txtDeliverDate" class="date" data-bind="value: vm.DeliverDate" placeholder="Select a Date"/>
					<input type="text" class="add-on" style="width:70px;background-color:white" id="txtDeliverTime" placeholder="HH:MM" data-bind="value: vm.DeliverTime"/>
				</div>

				<label>Home Safe Date &amp; Time:</label>
				<div class="input-append">
					<input type="text" style="width:125px;" id="txtHomeSafeDate" class="date" data-bind="value: vm.HomeSafeDate" placeholder="Select a Date"/>
					<input type="text" class="add-on" style="width:70px;background-color:white" id="txtHomeSafeTime" placeholder="HH:MM" data-bind="value: vm.HomeSafeTime"/>
				</div>

				<label>Notes:</label>
				<textarea id="txtNotes" maxlength="599" data-bind="value: vm.Notes"></textarea>
			</div>

		</div>

		</fieldset>

	</div>
	<hr/>
	<div class="row">
		<div class="span6">
			<button type="button" class="btn btn-primary btn-lg readOnlyHidden" id="cmdSave" onclick="saveRun()"><i class="icon-ok icon-white"></i> Save Run</button> 
		</div>
		<div class="span6 pull-right readOnlyHidden" style="text-align:right">
			<button type="button" class="btn btn-lg readOnlyHidden" id="cmdNotRun" onclick="saveNotRun()" style="display:none"><i class="icon-remove icon-red"></i> Not Completed</button>
		</div>
	</div>
</div>

<script>

	currentMemberID = <%=this.MemberId%>;
	runLogID = <%=this.RunLogID%>;
	userLevel = <%=this.UserLevel%>;

	function showCurrentController() 
	{
		$("#txtController").val("<%=this.MemberName%>");
	}

	initFeedback();

	if ('<%=this.ShowAA.ToString()%>' == 'False')
	{
		showBloodPanel();
	}
	else
	{
		showAAPanel();
	}

</script>

</asp:Content>


