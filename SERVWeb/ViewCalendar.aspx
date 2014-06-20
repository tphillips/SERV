<%@ Page Language="C#" Inherits="SERVWeb.ViewCalendar" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">The Calendar</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">

<script language="JavaScript" src="js/Calendar.js"></script>
<br/>
<div id="entry" style="display:none">
	<small>
		<div class="row" id="calendar">
			
				<table class="table table-striped table-bordered table-condensed">
					
					<tr>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay1"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay2"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay3"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay4"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay5"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay6"></span></td>
						<td style="text-align:center; vertical-align:top; width:150px"><span id="titleDay7"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay1"></div></td>
						<td><div id="scheduledDay2"></div></td>
						<td><div id="scheduledDay3"></div></td>
						<td><div id="scheduledDay4"></div></td>
						<td><div id="scheduledDay5"></div></td>
						<td><div id="scheduledDay6"></div></td>
						<td><div id="scheduledDay7"></div></td>
					</tr>

					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay8"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay9"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay10"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay11"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay12"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay13"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay14"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay8"></div></td>
						<td><div id="scheduledDay9"></div></td>
						<td><div id="scheduledDay10"></div></td>
						<td><div id="scheduledDay11"></div></td>
						<td><div id="scheduledDay12"></div></td>
						<td><div id="scheduledDay13"></div></td>
						<td><div id="scheduledDay14"></div></td>
					</tr>

					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay15"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay16"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay17"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay18"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay19"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay20"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay21"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay15"></div></td>
						<td><div id="scheduledDay16"></div></td>
						<td><div id="scheduledDay17"></div></td>
						<td><div id="scheduledDay18"></div></td>
						<td><div id="scheduledDay19"></div></td>
						<td><div id="scheduledDay20"></div></td>
						<td><div id="scheduledDay21"></div></td>
					</tr>

					<tr>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay22"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay23"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay24"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay25"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay26"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay27"></span></td>
						<td style="text-align:center; vertical-align:top;"><span id="titleDay28"></span></td>
					</tr>
					<tr style="height:130px;">
						<td><div id="scheduledDay22"></div></td>
						<td><div id="scheduledDay23"></div></td>
						<td><div id="scheduledDay24"></div></td>
						<td><div id="scheduledDay25"></div></td>
						<td><div id="scheduledDay26"></div></td>
						<td><div id="scheduledDay27"></div></td>
						<td><div id="scheduledDay28"></div></td>
					</tr>

				</table>
			
		</div>
	</small>
</div>

<div id="calSlotClickedDialog" style="display:none; background-color:#fcfcfc" title="Your Shift">

</div>

<div id="volunteerClickedDialog" style="display:none; background-color:#fcfcfc" title="Volunteer">

</div>

<script>

	$(function() 
	{
		initWeeksCalendar();
		loadWeeksCalendar(<%=this.MemberId%>);
	});

</script>

</asp:Content>


