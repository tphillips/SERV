<%@ Page Language="C#" Inherits="SERVWeb.SMS" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>


<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">SMS</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<h3>Bulk SMS</h3>
<label>Send To:</label>
<div class="checkbox">
	<label>
		<input type="checkbox" id="chkEmergencyList" /> Emergency List
	</label>
	<label>
		<input type="checkbox" id="chkController" /> Controllers
	</label>
	<label>
		<input type="checkbox" id="chkFundraisers" /> Fundraisers
	</label>
	<label>
		<input type="checkbox" id="chkBlood" /> Blood Riders / Drivers
	</label>
	<label>
		<input type="checkbox" id="chkAA" /> Air Ambulance Riders / Drivers
	</label>
	<label>
		<input type="checkbox" id="chkMilk" /> Milk Riders / Drivers
	</label>
	<label>
		<input type="checkbox" id="chk4x4" /> 4x4 Owners
	</label>
	<label>
		<input type="checkbox" id="chkAll" /> EVERYBODY (CAUTION!!!!)
	</label>
</div>
<label>Custom numbers seperated by a comma:</label>
<asp:TextBox runat="server" id="txtCustomNumbers" />
<br/>
<label>Message:</label>
<asp:TextBox runat="server" id="txtSMS" textmode="multiline" cols=20 rows=3 maxLength="120" />

<br/><br/>
<asp:Button runat="server" id="cmdLogin" Text="Send" class="btn btn-primary btn-lg" onclick="cmdSendClick" />

<script>
	$("#loading").hide();
</script>

</asp:Content>
