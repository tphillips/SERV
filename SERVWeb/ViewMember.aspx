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
		<h2>Member</h2>
		<fieldset>
			<div class="row">
			
				<div class="span3">
					
					<h4>Unit Settings</h4>
				
					<label>First Name:</label>
					<input type="text" id="txtFirstName" required="*" />
						
					<label>Last Name:</label>
					<input type="text" id="txtLastName" required="*" /> *
					
				</div>
			</div>
		</fieldset>
	</div>

</asp:Content>
