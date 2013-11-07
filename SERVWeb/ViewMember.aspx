<%@ Page Language="C#" Inherits="SERVWeb.ViewMember" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Member</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	<script language="JavaScript" src="js/JS.js"></script>
	
	<div id="loading">
		<h2>Member</h2>
		<p>Please wait . . .</p>
	</div>
	
	<div id="error" style="display:none">
		<h2>Ooops!</h2>	
		<p>There was an error :(</p>	
	</div>
	
	<div id="success" style="display:none" class="hero-unit">
		<h2>Success!</h2>	
		<p ><span id="successMessage">Super . . .</span></p>	
	</div>
	
	<div id="entry" style="display:none">
		<h3><span id="lblTitle"></span></h3>
		<fieldset>
			<div class="row">
			
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
					
				</div>
				
				<div class="span3">
				
					<label>Occupation:</label>
					<input type="text" id="txtOccupation" />
					
					<label>Birth Year:</label>
					<input type="text" id="txtBirthYear" />
					
					<label>Next Of Kin:</label>
					<input type="text" id="txtNOK" />
					
					<label>Next Of Kin Address:</label>
					<input type="text" id="txtNOKAddress" />
					
					<label>Next Of Kin Phone Number:</label>
					<input type="text" id="txtNOKPhone" />
				
				</div>
				
			</div>
		</fieldset>
		<br/>
		<input type=button class="btn" onclick="SaveMember()" value="Save" />
		
	</div>
	<br/>
	<br/>
	
	<script>
	
	$(function() 
	{
		DisplayMember(<%=this.MemberId%>);
	});
	
	function SaveMember()
	{
		SaveBasicMember(<%=this.MemberId%>);
	}
		
	</script>

</asp:Content>
