<%@ Page Language="C#" Inherits="SERVWeb.OpsMap" %>
<%@ Register TagPrefix="SERV" TagName="TopControl" Src="TopControl.ascx" %>
<%@ Register TagPrefix="SERV" TagName="Panels" Src="Panels.ascx" %>

<!DOCTYPE html>
<html>
    
    <head>
        <title>SERV - Ops Map</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
        <link rel="stylesheet" href="/css/style.css" />
        <link rel="stylesheet" href="/css/smoothness/jquery-ui-1.10.3.custom.min.css" />
        <link rel="stylesheet" href="/css/bootstrap.min.css" />
        <link rel="stylesheet" href="/css/icons.css" />
        <script src="/js/jquery-1.10.1.min.js"></script>
        <script src="/js/jquery-ui-1.10.3.custom.min.js"></script>
        <script language="JavaScript" src="js/JS.js"></script>
		<style type="text/css">
			html { height: 100% }
			body { height: 100%; margin: 0px; padding: 0; }
			#map_canvas { height: 100%;  }
		</style>
    </head>
    
    <body style="">
        
        <script src="/js/bootstrap.min.js"></script>
    	<script type="text/javascript" 
    		src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDv-Wccd0zvGm6GyENJJs7d3lQNmysym9o&sensor=true&libraries=weather"></script>
		<script type="text/javascript" src="js/opsMap.js"></script>
			
		<SERV:TopControl runat="server" id="topControl" />

		<div id="map_canvas" style="width:100%; height:100%;"></div>	
	
        <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
			<div class="navbar-inner">
				<ul class="nav">
					<li class="dropdown">
						<a href="#" class="dropdown-toggle" data-toggle="dropdown">Map <b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li id="lnkShowMember"><a href="#" onclick="showMemberSearchDialog();"><i class="icon-map-marker"></i> Show Member On Map</a></li>
							<li id="lnkShowMembers"><a href="#" onclick="showMembers();"><i class="icon-map-marker"></i> Show Members On Map</a></li>
							<li><a href="#" onclick="showHideWeather();"><i class="icon-cog"></i> Show / Hide Weather</a></li>
							<li><a href="#" onclick="showHideTraffic();"><i class="icon-warning-sign"></i> Show / Hide Traffic</a></li>
							<li class="divider"></li>
							<li><a href="#" onclick="$('#memberList').dialog('open');"><i class="icon-list"></i> Show Member List</a></li>
							<li><a href="#" onclick="$('#locationsList').dialog('open');"><i class="icon-list"></i> Show Locations List</a></li>
							<li class="divider"></li>
							<li><a href="#" onclick="showLoadRouteFile();"><i class="icon-resize-horizontal"></i> Load a TomTom ".itn" Route File</a></li>
							<li class="divider"></li>
							<li><a href="#" onclick="showControllerLog();"><i class="icon-pencil"></i> Log a New Run</a></li>
							<li class="divider"></li>
							<li><a href="#" onclick="window.location.reload();"><i class="icon-globe"></i> Reset</a></li>
						</ul>
					</li>
				</ul>
			</div>
		</nav>
            
    	<div id="routeDialog" style="display:none" title="Load an 'itn' Route">
			<p>
				Paste the itn file content in here:<br/>
				<textarea id="txtRoute" style="width:95%; height: 400px; font-family: courier; font-size:small;"></textarea>
				<br/><br/>
				<input class="btn" type="button" value="Load" onclick="loadRouteFile();" />
			</p>
		</div>    

		<div id="controllerLog" style="display:none" title="Controller Log">
			<iframe style="width:100%; height;620px" height="620px" frameborder="0" src="ControllerLog.aspx?NoTopMenu=1">
			</iframe>
		</div>

		<div id="locationsList" style="display:none" title="Locations">
			<iframe style="width:100%; height;600px" height="600px" frameborder="0" src="Locations.aspx?NoTopMenu=1">
			</iframe>
		</div>

		<div id="memberList" style="display:none" title="Members">
			<iframe style="width:100%; height;600px" height="600px" frameborder="0" src="Members.aspx?NoTopMenu=1">
			</iframe>
		</div>

		<div id="memberSearch" style="display:none" title="Choose a Member">
			<br/>
			<div class="input-prepend input-append ">
				<a style="width:10px" type=button class="btn btn-lg" disabled><i class="icon-search"></i></a>
				<input style="width:250px" type="text" class="riders" id="txtFindMember" />
				<input style="width:70px" type="button" value="Show" class="btn btn-primary btn-lg" onclick="showMember($('#txtFindMember').val());"></input>
			</div>
		</div>
	
    </body>

    <script>

    	if (<%=UserLevel%> < 2)
    	{
    		$("#lnkShowMember").hide();
    	}

    </script>
    
</html>










