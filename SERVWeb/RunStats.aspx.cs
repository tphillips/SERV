
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;
	using SERVBLLFactory;
	using System.Data;

	public partial class RunStats : System.Web.UI.Page
	{
		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
			dgRunLog.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_RunLog();
			dgRunLog.DataBind();
			dgTodaysUsers.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_TodaysUsers();
			dgTodaysUsers.DataBind();
			dgReport.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_RecentRunLog();
			dgReport.DataBind();
			dgTop10.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_Top10Riders();
			dgTop10.DataBind();
			dgRunNoLogin.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_RunButNoLogin();
			dgRunNoLogin.DataBind();
			lblRunNoLogin.Text = string.Format("{0} Result(s)", ((DataTable)dgRunNoLogin.DataSource).Rows.Count);
			dgAvgPerDay.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_AverageCallsPerDay();
			dgAvgPerDay.DataBind();
			dgHeatMap.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_CallsPerHourHeatMap();
			dgHeatMap.DataBind();
		}
	}
}

