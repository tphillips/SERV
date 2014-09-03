<%@ Page Language="C#" Inherits="SERVWeb.Default" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>

<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Landing</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	

	<div class="row" style="margin:0px;background-color:white; background-image:url(img/HeaderBg.jpg); background-position:center; padding:50px;text-align:center">
		<div class="span6">
			<img src="img/logo.png"/>
			<br/><br/>
			<h2>SERV SSL</h2>	
			<h5 style="color:#999999;">System for Emergency Rider Volunteers</h5>
			<h4>Welcome to <%=SERVGlobal.SystemName%> <%=SERVGlobal.SERVVersion%>.  </h4>	
		</div>
		<div class="span5 pull-right">
			<h4>Please <a href="Login.aspx">login</a> to continue</h4>
			<asp:TextBox runat="server" id="txtEmail" Style="width:250px; height:30px; font-size:medium" /><br/>
			<asp:TextBox runat="server" id="txtPassword" TextMode="password" Style="width:250px; height:30px; font-size:medium"/><br/>
			<asp:Button runat="server" id="cmdLogin" Text="Login" class="btn btn-primary btn-lg" onclick="cmdLoginClick" Style="width:265px; height:40px;"/><br/>
			<asp:Literal runat="server" id="litServerClient"></asp:Literal><br/>
			<p class="small">If you are interested in using the SERV system<br/>for your organisation, please <a href="http://www.servssl.org.uk/contact">contact us</a>.</p>
		</div>
	</div>
		
	<div class="row" style="margin:0px;background-color:black; color:white; padding:50px;text-align:center">
		<h2>Why your organisation should use the SERV system.</h2>
		<h4>Designed from the ground up to support Emergency Rider Volunteers</h4>
		<center>
			<table style="width:70%; text-align:center; margin-top:70px; font-size:medium">
				<tr>
					<td style="width:50%">Open Source &amp; Free (as in freedom)</td>
					<td style="width:50%">Easy to install and customise</td>
				</tr>
				<tr>
					<td style="width:50%">Easily Manage your Membership</td>
					<td style="width:50%">Plan and organise your rota &amp; calendar</td>
				</tr>
				<tr>
					<td style="width:50%">Automated bulk SMS</td>
					<td style="width:50%">Full controller logging</td>
				</tr>
				<tr>
					<td style="width:50%">System generated urgencies</td>
					<td style="width:50%">Members manage their own preferences</td>
				</tr>
				<tr>
					<td style="width:50%">Mobile friendly responsive design</td>
					<td style="width:50%">Automated emails</td>
				</tr>
				<tr>
					<td style="width:50%">Fine grained permission levels</td>
					<td style="width:50%">Flextel integration</td>
				</tr>
				<tr>
					<td style="width:50%">Manage your locations list</td>
					<td style="width:50%">Rich reporting suite</td>
				</tr>
			</table>
		</center>
	</div>

	<div class="row" style="margin:0px; padding:50px;text-align:center">
		<h2>The easiest way to manage Emergency Rider Volunteers.</h2>
		<h4><a href="http://www.servssl.org.uk/contact">Contact Us</a> Today for a Demonstration</h4>
		<p><a class="btn btn-large btn-success" href="http://www.servssl.org.uk/contact">Contact Us</a></p>
		<center>
			<table style="width:70%; text-align:center; margin-top:50px">
				<tr>
					<td style="width:50%"><img style="max-width:400px;" class="img-rounded" src="img/screenshots/1.png" width="400px" /></td>
					<td style="width:50%"><img style="max-width:400px;" class="img-rounded" src="img/screenshots/2.png" width="400px" /></td>
				</tr>
				<tr>
					<td style="width:50%"><img style="max-width:400px;" class="img-rounded" src="img/screenshots/3.png" width="400px" /></td>
					<td style="width:50%"><img style="max-width:400px;" class="img-rounded" src="img/screenshots/4.png" width="400px" /></td>
				</tr>
			</table>
		</center>
	</div>

	<div class="row" style="margin:0px;background-color:black; color:white; padding:50px;text-align:center">
		<h2><a style="color:white" href="http://www.servssl.org.uk">Learn more about SERV SSL.</a></h2>
		<h4><a class="btn btn-large btn-success" href="http://www.servssl.org.uk">Learn more . . .</a></h4>
	</div>
	
	
	<script>


	$(function() 
	{
		$("#loading").hide();
		$("#container").removeClass("container");
		$("#container").addClass("fullScreenContainer_noReally");
	});

	</script>
	
</asp:Content>
