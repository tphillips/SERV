<%@ Page Language="C#" Inherits="SERVWeb.SMS" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>


<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">SMS</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<script language="JavaScript" src="js/JS.js"></script>

<div id="entry">
<h3>Bulk SMS</h3>
<label>Send To:</label>
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
		<input type="checkbox" id="chkBlood" onchange="genTags();" /> Blood Riders / Drivers
	</label>
	<label>
		<input type="checkbox" id="chkAA" onchange="genTags();" /> Air Ambulance Riders / Drivers
	</label>
	<label>
		<input type="checkbox" id="chkMilk" onchange="genTags();" /> Milk Riders / Drivers
	</label>
	<label>
		<input type="checkbox" id="chk4x4" onchange="genTags();" /> 4x4 Owners
	</label>
</div>
<input type="hidden" id="txtTags" disabled width="200" />
<input type="hidden" id="txtNumbers" disabled width="200" />
<br/>
<label>Message to <strong><span id="lblCount">0</span></strong> people:</label>
<textarea type="text" id="txtSMS" textmode="multiline" cols="40" rows=3 maxlength="150" ></textarea>

<br/><br/>
<input type="button" id="cmdSend" value="Send" class="btn btn-primary btn-lg" onclick="sendSMSMessage($('#txtNumbers').val(), $('#txtSMS').val());"></input>
</div>
<script>
	$("#loading").slideUp();
	var smsCount = 0;
	
	function genTags()
	{
		var tags = "";
		$("#lblCount").text(0);
		if ($("#chkEmergencyList").prop('checked')) { tags += "EmergencyList,"; }
		if ($("#chkControllers").prop('checked')) { tags += "Controller,"; }
		if ($("#chkFundraisers").prop('checked')) { tags += "Fundraiser,"; }
		if ($("#chk4x4").prop('checked')) { tags += "4x4,"; }
		if ($("#chkBlood").prop('checked')) { tags += "Blood,"; }
		if ($("#chkAA").prop('checked')) { tags += "AA,"; }
		if ($("#chkWater").prop('checked')) { tags += "Water,"; }
		if ($("#chkMilk").prop('checked')) { tags += "Milk,"; }
		$("#txtTags").val(tags);
		$("#txtNumbers").val("");
		if (tags != "")
		{
			$("#loading").slideDown();
			var numbers = getNumbersForTags(tags, "txtNumbers");
		}
	}
	
</script>

</asp:Content>
