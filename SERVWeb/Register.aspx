<%@ Page Language="C#" Inherits="SERVWeb.Register" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Register</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
<div id="intro">
	<div class="hero-unit">
		<div class="row">
			<div class="span2">
				<img src="img/logo.png"/>
			</div>
			<div class="span8">
				<p>By this point you should have already spoken to our recruitment manager and decided 
				that you would like to proceed to becoming a SERV volunteer. <strong>Congratulations!</strong> 
				Volunteering for SERV as a Rider, Driver, Controller or Fundraiser is a very 
				rewarding endeavour and supports a service that makes a real difference.</p>
			</div>
		</div>
	</div>
	<p>Before you can be put on our rota, you need to create an account on our system.  
	This system is used for managing the membership data, managing your preferences as a 
	volunteer and for logging runs.  The system may also send you messages via 
	SMS when we are short on riders or need extra help, if you are happy to receive them.</p>
	<p>Data in the system is strictly controlled, stored in a secure enviroment and will never be shared with anybody else.</p>
	<p>We also use a forum for general discussion and event planning, this is a seperate system which you will be given access to soon.</p>
	<p>Once you have registered and become a member, you will need to come back and add some more details, but let's get started with the basics.</p>
	<a id="lnkStart" href="#" onclick="$('#entry').slideDown();$('#intro').slideUp();">OK, lets get started!</a>
</div>
<br/>
<div id="entry" style="display:none">
	<h3>Register to become a SERV Volunteer</h3>
	<div class="row">

		<fieldset>
		<div class="span4">

			<label>First Name:</label>
			<input type="text" id="txtFirstName" />
				
			<label>Last Name:</label>
			<input type="text" id="txtLastName" />
			
			<label>Email Address:</label>
			<input type="text" id="txtEmail" />
			
			<label>Mobile Number:</label>
			<input type="text" id="txtMobile" />

			<label>Birth Year:</label>
			<input type="text" id="txtBirthYear" />
			
		</div>

		<div class="span4">

			<label>Address:</label>
			<input type="text" id="txtAddress1" /><br/>
			<input type="text" id="txtAddress2" />
			
			<label>Town:</label>
			<input type="text" id="txtTown" />
			
			<label>County:</label>
			<input type="text" id="txtCounty" />
			
			<label>Post Code:</label>
			<input type="text" id="txtPostCode" />
			
		</div>

		<div class="span4">
			
			<label>Tags / Capabilities (<strong>preferences</strong>):</label>
			<div class="checkbox">
				<label>
					<input type="checkbox" id="chkRider" /> Rider - You own a bike and will happily ride for SERV
				</label>
				<label>
					<input type="checkbox" id="chkDriver" /> Driver - You own a car and will happily drive for SERV
				</label>
				<label>
					<input type="checkbox" id="chk4x4" /> 4x4 - You have access to a 4x4 for use in bad weather
				</label>
				<label>
					<input type="checkbox" id="chkEmergencyList" /> Emergency Contact List - You are happy to be contacted (by phone or SMS) when SERV are short of riders / drivers
				</label>
				<label>
					<input type="checkbox" id="chkFundraiser" /> Fundraiser - You are happy to be contacted (by phone or SMS) when SERV are arranging a fundraising event
				</label>
			</div>
			<br/>
			<label>Anything else to tell/ask us?:</label>
			<textarea id="txtNotes" rows=3"></textarea>
		</div>
		</fieldset>
	</div>
	<br/>
	<div class="checkbox">
		<label>
			<input type="checkbox" id="chkAgree" /> By checking this box, I confirm that my vehicle is safe &amp; road legal (with Tax &amp; MOT), I have a full UK driving license with 6 or less penalty points and I am fully insured to use it. I have read &amp; understood the 
			<a id="showCop" href="#" onclick="$('#cop').slideDown();">Code of Practice</a>.
			<div id="cop" style="display:none">
				<br/>
				<small>
				<ul>
					<li>Members shall be responsible for carrying out SERV duties in a professional manner</li>
					<li>I must conduct myself honestly and appropriately so as not to bring SERV into disrepute</li>
					<li>I am prepared to contribute positively to making SERV a better charity and service to our community. This may from time to time require me to help with other duties such as attending events, controlling, or training</li>
					<li>I must use a SERV HiViz jacket and ID card, when on duty</li>
					<li>Wearing a SERV HiViz jacket or driving a SERV Vehicle when off duty is strictly forbidden</li>
					<li>Ride/drive within my own capabilities and always adhere to all traffic laws. I am deemed to be in charge of my vehicle at all times</li>
					<li>My vehicle is to be taxed, insured and kept in roadworthy condition with a current MOT certificate if applicable</li>
					<li>I must inform my insurance company that I am using my vehicle for SERV's charitable work</li>
					<li>If using a SERV Bike/Van I accept responsibility for duty of care of the charity's property</li>
					<li>I shall not use my role with SERV to seek business for my personal account</li>
					<li>SERV applies Equality, Diversity and Equal Opportunity principles and I must act accordingly</li>
				</ul>
				</small>
				<P>We accept that helping us is a voluntary action on your part.  We accept that you have a life outside of SERV, so you can say "no". We will do our best to treat you with integrity &amp; honesty. We will keep your personal information as secure as is reasonably possible and we will not share this information with anybody outside the organisation.</p>
			</div>
		</label>
	</div>
	<hr/>
	<button type=button class="btn btn-primary btn-lg readOnlyHidden" onclick="register()"><i class="icon-ok icon-white"></i> Register!</button>
</div>

<script>
	_loaded();
</script>

</asp:Content>


