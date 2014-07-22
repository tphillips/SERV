<%@ Page Language="C#" Inherits="SERVWeb.Forum"%>
<%@ Register TagPrefix="SERV" TagName="TopControl" Src="TopControl.ascx" %>
<%@ Register TagPrefix="SERV" TagName="Panels" Src="Panels.ascx" %>
<%@ Import Namespace="SERVWeb" %>

<!DOCTYPE html>
<html>
    
    <head>
        <title><%=SERVGlobal.SystemName%> - The Forum</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
        <%=SERVGlobal.CSSInclude()%>
        <link rel="stylesheet" href="/css/smoothness/jquery-ui-1.10.3.custom.min.css" />
        <link rel="stylesheet" href="/css/bootstrap.min.css" />
        <link rel="stylesheet" href="/css/icons.css" />
        <link rel="icon" type="image/png" href="img/logo.png">
        <script src="/js/jquery-1.10.1.min.js"></script>
        <script src="/js/jquery-ui-1.10.3.custom.min.js"></script>
        <%=SERVGlobal.MainJSInclude()%>	
		<style type="text/css">
			html { height: 100% }
			body { height: 100%; margin: 0px; padding: 0; }
		</style>
    </head>

    <body style="overflow:hidden">
        
        <script src="/js/bootstrap.min.js"></script>			
		<SERV:TopControl runat="server" id="topControl" />
      
		<iframe src="http://servssl.org.uk/members/index.php?app=core&module=search&do=viewNewContent&search_app=forums" style="width:100%; height:100%"  frameborder="0">
		</iframe>
		<script>
			$("#loading").hide();
			loadNewsBanner();
		</script>
	</body>
</html>


