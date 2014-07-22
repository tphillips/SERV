<%@ Control Language="C#" Inherits="SERVWeb.Panels" %>
<%@ Import Namespace="SERVWeb" %>

<div id="loading">
<br/>
	<p><img src="img/spinnerLarge.gif" width="40" /></p>
</div>

<div id="error" style="display:none">
	<div class="row">
		<div class="span3" id="errorImg">
			<img id="imgError" class="img-rounded" src="" width="200"/><br/>
		</div>
		<div class="span9">
			<h4>Sorry, something went wrong</h4>
			<br/>
			<p>Your session may have timed out, or you <i>may</i> have discovered a bug.</p>
			<p>If this keeps happening, please contact Tris</p>
		</div>
	</div>
	<br/>
</div>

<div id="success" style="display:none" class="hero-unit">
	<h2>Success!</h2>	
	<p><span id="successMessage">Super . . .</span> </p>	
	<button id="cmdAgain" type=button class="btn btn-success btn-lg" onclick="window.location.href=window.location.href;">Again Please!</button>
</div>

<div id="message" style="display:none" class="hero-unit">
	<h2>Please note:</h2>	
	<p><span id="successMessage"><span id="lblMessage"></span></span></p>	
</div>

<div id="alert" style="display:none" title="SERV">
	<p><span id="alertMessage">Default message</span></p>
</div>

<div id="pnlReadOnly" class="alert" style="display:none" title="SERV">
  <button type="button" class="close" data-dismiss="alert">&times;</button>
  <strong>CAUTION!</strong> <%=SERVGlobal.SystemName%> is currently in <strong>Read Only mode</strong>.  You will not be able to save any changes.
</div>

<div id="feedbackDialog" style="display:none; background-color:#fcfcfc" title="Feedback">
	<p>Use this form to provide <strong>anonymous</strong> feedback on <i>any</i> aspect of SERV or the <%=SERVGlobal.SystemName%> system.</p>
	<textarea id="txtFeedback" cols="40" rows="5" style="width:450px" onkeypress="filterKeys()"></textarea>
	<br/><br/>
	<input type="button" value="Submit" class="btn btn-primary btn-lg" onclick="cmdSubmitFeedbackClicked();"></input> <input type="button" value="Cancel" class="btn btn-lg" onclick="$('#feedbackDialog').dialog('close');"></input>
	<br/><br/>
</div>

<button onmouseover="showFeedbackButton();" onmouseout="hideFeedbackButton();" onclick="showFeedbackForm()" id="cmdFeedback" class="btn btn-lg" style="position:fixed;top:50%;right:-7px;display:none;"><img src="img/Feedback.png"></img></button>


<script>

	$("#imgError").attr('src', getErrorImage());



</script>

<!--
<div class="alert">
  <button type="button" class="close" data-dismiss="alert">&times;</button>
  <strong>CAUTION!</strong> A release will be performed in the next <strong>30 minutes</strong>!!<br/>
  When the new code is released, your session will be terminated. <strong>Any unsaved entries you have made will be lost.</strong>
</div>
  -->