<%@ Page Language="C#" Inherits="SERVWeb.BloodBoxBingo" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Blood Box Bingo!</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<img src="img/Brb.png" width="300" style="position:absolute; left:40px;"/>
<div id="bingo">
	<div id="bingoCard">
		<div class="bingoHeader"><span>Blood</span><span>Platelets</span><span>Plasma</span><span>Sample</span></div>
		<div class="bingoRow"><div class="bingoHosp">St Thomas'</div><div id="StThomasBlood" class="bingoBox"></div><div id="StThomasPlatelets" class="bingoBox"></div><div id="" class="bingoBox bingoBoxBlank"></div><div id="" class="bingoBox bingoBoxBlank"></div></div>
		<div class="bingoRow"><div class="bingoHosp">Frimley Park</div><div id="FrimleyBlood" class="bingoBox"></div><div id="FrimleyPlatelets" class="bingoBox"></div><div id="" class="bingoBox bingoBoxBlank"></div><div id="" class="bingoBox bingoBoxBlank"></div></div>
		<div class="bingoRow"><div class="bingoHosp">St Peter's</div><div id="StPetersBlood" class="bingoBox"></div><div id="StPetersPlatelets" class="bingoBox"></div><div id="" class="bingoBox bingoBoxBlank"></div><div id="" class="bingoBox bingoBoxBlank"></div></div>
		<div class="bingoRow"><div class="bingoHosp">Royal Surrey</div><div id="RoyalSurreyBlood" class="bingoBox"></div><div id="RoyalSurreyPlatelets" class="bingoBox"></div><div id="" class="bingoBox bingoBoxBlank"></div><div id="" class="bingoBox bingoBoxBlank"></div></div>
		<div class="bingoRow"><div class="bingoHosp">Hooley</div><div id="HooleyBlood" class="bingoBox"></div><div id="HooleyPlatelets" class="bingoBox"></div><div id="HooleyPlasma" class="bingoBox"></div><div id="" class="bingoBox bingoBoxBlank"></div></div>
		<div class="bingoRow"><div class="bingoHosp">Tooting</div><div id="" class="bingoBox bingoBoxBlank"></div><div id="" class="bingoBox bingoBoxBlank"></div><div id="" class="bingoBox bingoBoxBlank"></div><div id="TootingSample" class="bingoBox"></div></div>
	</div>
</div>

<script>
	_loaded();
	$("#container").removeClass("container");
	$("#container").addClass("fullScreenContainer_noReally");
	getMemberUniqueRuns();
	function getMemberUniqueRuns()
	{
		callServerSideGet(
			"Service/Service.asmx/GetMemberUniqueRuns", 
			function(json)
			{
				var done = 0;
				var max = 12;
				for(var x = 0; x < json.d.length; x++)
				{
					if (json.d[x] == "St Thomas'-Blood") { $("#StThomasBlood").addClass("bingoBoxDone"); done++;}
					if (json.d[x] == "St Thomas'-Platelets") { $("#StThomasPlatelets").addClass("bingoBoxDone"); done++;}

					if (json.d[x] == "Frimley Park-Blood") { $("#FrimleyBlood").addClass("bingoBoxDone"); done++;}
					if (json.d[x] == "Frimley Park-Platelets") { $("#FrimleyPlatelets").addClass("bingoBoxDone"); done++;}

					if (json.d[x] == "St Peter's, Chertsey-Blood") { $("#StPetersBlood").addClass("bingoBoxDone"); done++;}
					if (json.d[x] == "St Peter's, Chertsey-Platelets") { $("#StPetersPlatelets").addClass("bingoBoxDone"); done++;}

					if (json.d[x] == "Royal Surrey County, Guildford-Blood") { $("#RoyalSurreyBlood").addClass("bingoBoxDone"); done++;}
					if (json.d[x] == "Royal Surrey County, Guildford-Platelets") { $("#RoyalSurreyPlatelets").addClass("bingoBoxDone"); done++;}

					if (json.d[x] == "Hooley-Blood") { $("#HooleyBlood").addClass("bingoBoxDone"); done++;}
					if (json.d[x] == "Hooley-Platelets") { $("#HooleyPlatelets").addClass("bingoBoxDone"); done++;}
					if (json.d[x] == "Hooley-Plasma") { $("#HooleyPlasma").addClass("bingoBoxDone"); done++;}

					if (json.d[x] == "NBS Tooting-Sample") { $("#TootingSample").addClass("bingoBoxDone"); done++;}
				}
				if (done == 0)
				{
					niceAlert("You don't have any stamps yet, better get cracking!!");
				}
				else if (done < (max / 2))
				{
					niceAlert("You have " + done + " of a possible " + max + " stamps!");
				}
				else if (done < max)
				{
					niceAlert("You have " + done + " of a possible " + max + " stamps, getting there!!");
				}
				else if (done == max)
				{
					niceAlert("You have all " + max + " stamps! BINGO!");
				}
			},
			function()
			{
				
			},
			true
		);
	}


</script>
</asp:Content>
