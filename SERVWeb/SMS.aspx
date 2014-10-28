<%@ Page Language="C#" Inherits="SERVWeb.SMS" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">SMS</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<div id="entry">
<h3>Bulk SMS</h3>
<p>Credits remaining: <strong><span id="lblCredits">...</span></strong></p>
<label>Send From:</label>

<div class="btn-group" data-toggle="buttons-radio">
    <button type="button" class="btn active" onclick="fromServ = true;" id="btnSERVSSL">SERVSSL</button>
    <button type="button" class="btn" onclick="fromServ = false;" id="btnYou">You</button>
</div>
<br/><br/>

</div>
<label>Send To <small>(This is now an intersection, so AND combination. Each member must have ALL the selected tags)</small>:</label>
<div class="checkbox">
	<label>
		<input type="checkbox" id="chkEmergencyList" onchange="genTags();" /> Emergency List
	</label>
	<label>
		<input type="checkbox" id="chkControllers" onchange="genTags();" /> Controllers
	</label>
	<label>
		<input type="checkbox" id="chkFundraisers" onchange="genTags();" /> Fundraisers
	</label>
	<label>
		<input type="checkbox" id="chkRiders" onchange="genTags();" /> Riders
	</label>
	<label>
		<input type="checkbox" id="chkDrivers" onchange="genTags();" /> Drivers
	</label>
	<label>
		<input type="checkbox" id="chk4x4" onchange="genTags();" /> 4x4 Owners
	</label>
	<label>
		<input type="checkbox" id="chkBlood" onchange="genTags();" /> Blood Volunteers
	</label>
	<label>
		<input type="checkbox" id="chkAA" onchange="genTags();" /> Air Ambulance Volunteers
	</label>
	<label>
		<input type="checkbox" id="chkMilk" onchange="genTags();" /> Milk Volunteers
	</label>

</div>
<input type="hidden" id="txtTags" disabled width="200" />
<input type="hidden" id="txtNumbers" disabled width="200" />
<br/>
<label>Message to <strong><span id="lblCount">0</span></strong> people:</label>
<textarea type="text" id="txtSMS" textmode="multiline" cols="40" rows=3 maxlength="150" onkeypress="filterKeys()" ></textarea>

<br/><br/>
<input type="button" id="cmdSend" value="Send" class="btn btn-primary btn-lg" onclick="sendSMSMessage($('#txtNumbers').val(), $('#txtSMS').val());"></input>
</div>
<script>
	loaded();
	var smsCount = 0;
	var fromServ = true;

	GetSMSCreditCount("lblCredits");

	function genTags()
	{
		var tags = "";
		$("#lblCount").text(0);
		if ($("#chkEmergencyList").prop('checked')) { tags += "EmergencyList,"; }
		if ($("#chkControllers").prop('checked')) { tags += "Controller,"; }
		if ($("#chkFundraisers").prop('checked')) { tags += "Fundraiser,"; }
		if ($("#chk4x4").prop('checked')) { tags += "4x4,"; }
		if ($("#chkRiders").prop('checked')) { tags += "Rider,"; }
		if ($("#chkDrivers").prop('checked')) { tags += "Driver,"; }
		if ($("#chkBlood").prop('checked')) { tags += "Blood,"; }
		if ($("#chkAA").prop('checked')) { tags += "AA,"; }
		if ($("#chkWater").prop('checked')) { tags += "Water,"; }
		if ($("#chkMilk").prop('checked')) { tags += "Milk,"; }
		$("#txtTags").val(tags);
		$("#txtNumbers").val("");
		if (tags != "")
		{
			loading();
			var numbers = getNumbersForTags(tags, "txtNumbers");
		}
	}
	
</script>

</asp:Content>
