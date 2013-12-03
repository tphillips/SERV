
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
			dgReport.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_RecentRunLog();
			dgReport.DataBind();
			dgTop10.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_Top10Riders();
			dgTop10.DataBind();
			dgRunNoLogin.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_RunButNoLogin();
			dgRunNoLogin.DataBind();
			lblRunNoLogin.Text = string.Format("{0} Result(s)", ((DataTable)dgRunNoLogin.DataSource).Rows.Count);
		}
	}
}

