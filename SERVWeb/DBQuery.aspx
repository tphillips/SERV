<%@ Page Language="C#" Inherits="SERVWeb.DBQuery" MasterPageFile="~/Master.master"  %>
<%@ MasterType VirtualPath="~/Master.master" %>
<%@ Import Namespace="SERVWeb" %>
<asp:Content ContentPlaceHolderID="titlePlaceholder" ID="titlePlaceholderContent" runat="server">Database Query</asp:Content>
<asp:Content ContentPlaceHolderID="contentPlaceholder" ID="contentPlaceholderContent" runat="server">
<script src="/js/ace.js" type="text/javascript" charset="utf-8"></script>
<script src="/js/ext-language_tools.js"></script>
	<h3>Database Query</h3>
	<div class="row">
		<div class="span12">
			<div id="entry">
				<div id="editor" style="height:150px; wi--dth:800px;">/* 
Your query here.
Do not do silly things like Select * from huge_table
To start you off, this query shows you all the Database Tables
*/

select TABLE_NAME from information_schema.tables where TABLE_TYPE = 'BASE TABLE'











/*
Availability
Calendar
CalendarEntry
CalendarRequirements
Duty
FleetVehicle
Karma
Location
Member
MemberStatus
MemberType
Member_Calendar
Member_Duty
Member_Tag
Message
MessageType
Product
RawRunLog
RunLog
RunLog_Product
SERVGroup
Tag
User
UserLevel
VehicleType
*/
				</div>
			</div>
			<input type="button" class="btn btn-primary" onclick="executeQuery()" value="Run"/>
		</div>
	</div>
	<br/><br/>
	<div class="row">
		<div class="span12">
			<div id="results">
			</div>
		</div>
	</div>

	<script>

		var editor = ace.edit("editor");

		$(function()
		{
			window.onbeforeunload = function()
			{
				return "Leaving this page will scrap your SQL query. Make sure you have copied it out.";
			}
			ace.require("ace/ext/language_tools");
			editor.setTheme("ace/theme/xcode");
			editor.getSession().setMode("ace/mode/sql");
			editor.setOptions({
				showPrintMargin:false,
				highlightActiveLine:false,
				enableBasicAutocompletion: true
			});
			loaded();
		});

		function executeQuery()
		{
			var sql = editor.getSession().getTextRange(editor.getSelectionRange())
			if (sql.length == 0)
			{
				niceAlert("Select the SQL you rant to run first . . .");
				return;
				//sql = editor.getSession().getValue();
			}
			if (sql.indexOf("/*admin*/") == -1)
			{
				var sqlL = sql.toLowerCase();
				if (sqlL.indexOf("update ") != -1 ||
					sqlL.indexOf("insert ") != -1 ||
					sqlL.indexOf("drop ") != -1 ||
					sqlL.indexOf("truncate ") != -1 ||
					sqlL.indexOf("delete ") != -1 ||
					sqlL.indexOf("exec ") != -1 ||
					sqlL.indexOf("execute_sp ") != -1 ||
					sqlL.indexOf("create ") != -1 ||
					sqlL.indexOf("update\n") != -1 ||
					sqlL.indexOf("insert\n") != -1 ||
					sqlL.indexOf("drop\n") != -1 ||
					sqlL.indexOf("truncate\n") != -1 ||
					sqlL.indexOf("delete\n") != -1 ||
					sqlL.indexOf("exec\n") != -1 ||
					sqlL.indexOf("execute_sp\n") != -1 ||
					sqlL.indexOf("create\n") != -1)
				{
					niceAlert("This tool is only for queries.  Your SQL will not be run.");
					return;
				}
			}
			loading();
			callServerSide(
				"Service/Service.asmx/ExecuteSQL", 
				'{"sql":"' + sql + '"}',
				function(json)
				{
					$("#results").empty();
					$("#results").append(json.d);
					$(".sqlResults").addClass("table"); 
					$(".sqlResults").addClass("table-striped"); 
					$(".sqlResults").addClass("table-bordered"); 
					$(".sqlResults").addClass("table-condensed"); 
					loaded();					  
				},
				function(json)
				{
					niceAlert("Error");
					loaded();
				}
			);

		}

	</script>

</asp:Content>

