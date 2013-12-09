<%@ Page Language="C#" Inherits="SERVWeb.ViewMember" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Member</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<div id="entry" style="display:none">
		<h3><span id="lblTitle"></span> <span id="memNav" style="display:none"><a class="btn" href="ViewMember.aspx?memberId=<%=this.MemberId-1%>"><</a> <a class="btn" href="ViewMember.aspx?memberId=<%=this.MemberId+1%>">></a></span></h3>
		<button type=button class="btn btn-primary btn-lg" onclick="SaveMember()">Save</button>
		<hr/>
		
			<div class="row">

				<fieldset>
				<div class="span3">
				
					<label>First Name:</label>
					<input type="text" id="txtFirstName" />
						
					<label>Last Name:</label>
					<input type="text" id="txtLastName" />
					
					<label>Email Address:</label>
					<input type="text" id="txtEmail" />
					
					<label>Mobile Number:</label>
					<input type="text" id="txtMobile" />
					
					<label>Home Phone:</label>
					<input type="text" id="txtHomePhone" />
					
					<label>Birth Year:</label>
					<input type="text" id="txtBirthYear" />
					
					<label>Occupation:</label>
					<input type="text" id="txtOccupation" />
					
				</div>
				
				<div class="span3">
				
					<label>Address:</label>
					<input type="text" id="txtAddress1" /><br/>
					<input type="text" id="txtAddress2" /><br/>
					<input type="text" id="txtAddress3" />
					
					<label>Town:</label>
					<input type="text" id="txtTown" />
					
					<label>County:</label>
					<input type="text" id="txtCounty" />
					
					<label>Post Code:</label>
					<input type="text" id="txtPostCode" />
					
					<label>Next Of Kin:</label>
					<input type="text" id="txtNOK" />
					
					<label>Next Of Kin Address:</label>
					<input type="text" id="txtNOKAddress" />
					
					<label>Next Of Kin Phone Number:</label>
					<input type="text" id="txtNOKPhone" />
					
				</div>
				
				<div class="span3">
					
					<label>Tags / Capabilities (<strong>preferences</strong>):</label>
					<div class="checkbox">
						<label>
							<input type="checkbox" id="chkRider" onchange="onTagChecked('chkRider', 'Rider')" /> Rider - You own a bike and will happily ride for SERV
						</label>
						<label>
							<input type="checkbox" id="chkDriver" onchange="onTagChecked('chkDriver', 'Driver')" /> Driver - You own a car and will happily drive for SERV
						</label>
						<label>
							<input type="checkbox" id="chk4x4" onchange="onTagChecked('chk4x4', '4x4')" /> 4x4 - You have access to a 4x4 for use in bad weather
						</label>
						<label>
							<input type="checkbox" id="chkEmergencyList" onchange="onTagChecked('chkEmergencyList', 'EmergencyList')" /> Emergency Contact List - You are happy to be contacted (by phone or SMS) when SERV are short of riders / drivers
						</label>
						<label>
							<input type="checkbox" id="chkFundraiser" onchange="onTagChecked('chkFundraiser', 'Fundraiser')" /> Fundraiser - You are happy to be contacted (by phone or SMS) when SERV are arranging a fundraising event
						</label>
					</div>
				
				</div>
				
				<div class="span3">
										
					<label>Admin editable only:</label>
					
					<label>Join Date:</label>
					<input type="text" id="txtJoinDate" class="date"/>
					
					<label>Leave Date:</label>
					<input type="text" id="txtLeaveDate"  class="date"/>
					
					<label>Rider Assessment Date:</label>
					<input type="text" id="txtAssessmentDate" class="date" />
					
					<label>Ad-Qual Type:</label>
					<input type="text" id="txtAdQualType" />
					
					<label>Ad-Qual Date:</label>
					<input type="text" id="txtAdQualDate"  class="date"/>
					
					<label>Last GMP Date:</label>
					<input type="text" id="txtGMPDate"  class="date"/>
					
					<div class="checkbox">
						<label>
							<input type="checkbox" id="chkBlood" onchange="onTagChecked('chkBlood', 'Blood')" /> Blood volunteer
						</label>
						
						<label>
							<input type="checkbox" id="chkAA" onchange="onTagChecked('chkAA', 'AA')" /> Air Ambulance Volunteer
						</label>
						
						<label>
							<input type="checkbox" id="chkMilk" onchange="onTagChecked('chkMilk', 'Milk')" /> Milk Volunteer
						</label>
						
						<label>
							<input type="checkbox" id="chkWater" onchange="onTagChecked('chkWater', 'Water')" /> Water Volunteer
						</label>
						
						<label>
							<input type="checkbox" id="chkController" onchange="onTagChecked('chkController', 'Controller')"  /> Controller
						</label>
						
					</div>
					
					<label>Notes:</label>
					<textarea id="txtNotes" rows="2"></textarea>
					
				</div>
				</fieldset>

			</div>

		<br/>
		<hr/>
		<button type=button class="btn btn-primary btn-lg" onclick="SaveMember()">Save</button>
	
	</div>
	
	<script>
	
	$(function() 
	{
		if (<%=this.UserLevel%> < 4) // Admin
		{
			$("#chkBlood").attr('disabled', true);
			$("#chkAA").attr('disabled', true);
			$("#chkWater").attr('disabled', true);
			$("#chkMilk").attr('disabled', true);
			$("#chkController").attr('disabled', true);
			$("#txtJoinDate").attr('disabled', true);
			$("#txtLeaveDate").attr('disabled', true);
			$("#txtAssessmentDate").attr('disabled', true);
			$("#txtAdQualType").attr('disabled', true);
			$("#txtAdQualDate").attr('disabled', true);
			$("#txtGMPDate").attr('disabled', true);
			$("#txtNotes").attr('disabled', true);
		}
		else
		{
			$("#memNav").show();
		}
		if (<%=this.UserLevel%> == 3) // Committee
		{
			$("#txtAssessmentDate").attr('disabled', false);
			$("#txtGMPDate").attr('disabled', false);
		}
		$(".date").datepicker({ dateFormat: 'dd M yy' });
		DisplayMember(<%=this.MemberId%>);
	});
	
	function SaveMember()
	{
		SaveBasicMember(<%=this.MemberId%>);
	}
	
	function onTagChecked(src, tag)
	{
		if ($("#" + src).prop('checked') == true)
		{
			addMemberTag(<%=this.MemberId%>, tag);
		}
		else
		{
			removeMemberTag(<%=this.MemberId%>, tag);
		}
	}
		
	</script>

</asp:Content>
