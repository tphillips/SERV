<%@ Page Language="C#" Inherits="SERVWeb.Home" MasterPageFile="~/Master.master" %>
<%@ MasterType VirtualPath="~/Master.master" %>

<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
	<div class="hero-unit">
		<h2>Hey <%=this.Username%>,</h2>	
		<p>Now that you have logged in, please make sure you change your password.  You can then <a href="ViewMember.aspx?self=yes">view and edit your profile</a> to make sure it is all correct, or take a look at the <a href="Members.aspx">members list</a>.</p>	
		<p>You will notice an "Admin editable only" section in your profile.  If you need something changed in there, please PM <a target="_blank" href="http://servssl.org.uk/members/index.php?/user/29-tristan-phillips/">Tris</a>.</p>
		<p>There will be loads of cool things coming along in this new system, and the majority of the benefits will be realised if we all keep our volunteer profiles and preferences up to date.  If you have any suggestions or comments, then feel free to PM <a target="_blank" href="http://servssl.org.uk/members/index.php?/user/29-tristan-phillips/">Tris</a>.</p>
	</div>
	
	<div class="row">
		<div class="span1"></div>
		<div class="span4">
			<h3>Coming Soon</h3>
			<ul>
				<li>Mass SMS Alert System</li>
				<li>Controller Logging - No more Google!</li>
				<li>Run stats - See how many runs you / we have done &amp; how busy we are on different nights</li>
			</ul>
		</div>
		<div class="span2"></div>
		<div class="span4">
			<h3>Known Issues</h3>
			<ul>
				<li>Lack of validation</li>
			</ul>
			<p>Found an issue? PM <a target="_blank" href="http://servssl.org.uk/members/index.php?/user/29-tristan-phillips/">Tris</p>
		</div>
		<div class="span1"></div>
	</div>
	
	<script>
		$("#loading").hide();
		if ("<%=this.Success%>" == "yes") { $("#success").slideDown(); }
	</script>
	
</asp:Content>
