<%@ Control Language="C#" Inherits="SERVWeb.Panels" %>
<div id="loading">
	<h2>Loading</h2>
	<p>Please wait . . .</p>
</div>

<div id="error" style="display:none">
	<h2>Ooops!</h2>	
	<p>There was an error :(</p>	
</div>

<div id="success" style="display:none" class="hero-unit">
	<h2>Success!</h2>	
	<p ><span id="successMessage">Super . . .</span> </p>	
	<button type=button class="btn btn-success btn-lg" onclick="window.location.href=window.location.href;">Again Please!</button>
</div>

<div id="message" style="display:none" class="hero-unit">
	<h2>Please note:</h2>	
	<p ><span id="successMessage"><span id="lblMessage"></span></span></p>	
</div>

<div id="alert" style="display:none" title="SERV">
	<p><span id="alertMessage">Default message</span></p>
</div>