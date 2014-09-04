<%@ Page Language="C#" Inherits="SERVWeb.ViewMember" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Member</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	
	<div id="entry" style="display:none">
		<h3><span id="lblTitle"></span> <span id="memNav" style="display:none"><a class="btn" href="ViewMember.aspx?memberId=<%=this.MemberId-1%>"><</a> <a class="btn" href="ViewMember.aspx?memberId=<%=this.MemberId+1%>">></a></span></h3>
		<button type=button class="btn btn-primary btn-lg readOnlyHidden" onclick="SaveMember()"><i class="icon-ok icon-white"></i> Save</button>
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
					<input type="text" id="txtPostCode" /> <a target="_blank" id="lnkPostCode">...</a>
					
					<label>Next Of Kin:</label>
					<input type="text" id="txtNOK" />
					
					<label>Next Of Kin Address:</label>
					<input type="text" id="txtNOKAddress" />
					
					<label>Next Of Kin Phone Number:</label>
					<input type="text" id="txtNOKPhone" />
					
				</div>
				
				<div class="span3">

					<label>Ad-Qual Type:</label>
					<input type="text" id="txtAdQualType" />
					
					<label>Ad-Qual Date:</label>
					<input type="text" id="txtAdQualDate"  class="date"/>
					<br/><br/>
					
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
							<input type="checkbox" id="chkEmergencyList" onchange="onChkEmergencyListChanged()" /> Emergency Contact List - You are happy to be contacted (by phone or SMS) when SERV are short of riders / drivers
						</label>
						<label>
							<input type="checkbox" id="chkFundraiser" onchange="onTagChecked('chkFundraiser', 'Fundraiser')" /> Fundraiser - You are happy to be contacted (by phone or SMS) when SERV are arranging a fundraising event
						</label>
					</div>
				
				</div>
				
				<div class="span3">
										
					<a id="cmdShow" href="#" onclick="$('#adminOnly').slideDown(); $('#cmdShow').slideUp();" >+ Show Advanced Fields</a>
					<div id="adminOnly" style="display:none">

						<label>User Level:</label>
						<select id="cboUserLevel" onchange="onCboUSerLevelChanged()">
							<option value="1">Member</option>
							<option value="2">Controller</option>
							<option value="3">Committee</option>
							<option value="4">Admin</option>
						</select>

						<button id="cmdImpersonate" type=button class="btn btn-small" onclick="ImpersonateMember(<%=this.MemberId%>)"><i class="icon-user"></i> Impersonate</button>
						<br/><br/>

						<label>Join Date:</label>
						<input type="text" id="txtJoinDate" class="date"/>
						
						<label>Leave Date:</label>
						<input type="text" id="txtLeaveDate"  class="date"/>
						
						<label>Rider Assessment Date:</label>
						<input type="text" id="txtAssessmentDate" class="date" />
						
						<label>Last GMP Date:</label>
						<input type="text" id="txtGMPDate"  class="date"/>
						
						<div class="checkbox" id="adminTags">
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

							<label>
								<input type="checkbox" id="chkCommittee" onchange="onTagChecked('chkCommittee', 'Committee')"  /> Committee Member
							</label>
							
						</div>
						
						<label>Notes:</label>
						<textarea id="txtNotes" rows="1"></textarea>
					</div>
				</div>
				</fieldset>

			</div>

		<br/>
		<hr/>
		<button type=button class="btn btn-primary btn-lg readOnlyHidden" onclick="SaveMember()"><i class="icon-ok icon-white"></i> Save</button>
	
	</div>
	
	<script>
	
	$(function() 
	{
		if (<%=this.UserLevel%> < 4) // Admin
		{
			$("#cboUserLevel").attr('disabled', true);
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
			$("#chkOnRota").attr('disabled', true);
			$("#chkCommittee").attr('disabled', true);
			$("#cmdImpersonate").attr('disabled', true);
		}
		else
		{
			$("#memNav").show();
		}
		if (<%=this.UserLevel%> == 3) // Committee
		{
			$("#txtAssessmentDate").attr('disabled', false);
			$("#txtGMPDate").attr('disabled', false);
			$("#chkOnRota").attr('disabled', false);
		}
		$(".date").datepicker({ dateFormat: 'dd M yy' });
		DisplayMember(<%=this.MemberId%>);
		initFeedback();
	});

	function onChkEmergencyListChanged()
	{
		if ($("#chkEmergencyList").prop('checked') == false)
		{
			if 
			(
				$("#chkOnRota").prop('checked') == false && 
				$("#chkController").prop('checked') == false && 
				$("#chkMilk").prop('checked') == false && 
				$("#chkCommittee").prop('checked') == false && 
				$("#chkWater").prop('checked') == false)
			{
				niceAlert('Sorry, as you do not commit to any other duties you need to remain on the emergency list.  If you think this is an error, please contact an Administrator on the forum.');
				$("#chkEmergencyList").prop('checked',true);
				return;
			}
		}
		onTagChecked('chkEmergencyList', 'EmergencyList')
	}

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

	function onCboUSerLevelChanged()
	{
		if (<%=this.UserLevel%> == 4)
		{
			setMemberUserLevel(<%=this.MemberId%>, $("#cboUserLevel").val());
		}
	}
		
	</script>

</asp:Content>
