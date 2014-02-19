
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class RecentRuns : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
			dgRecentRuns.DataSource = SERVBLLFactory.Factory.RunLogBLL().Report_RunLog();
			dgRecentRuns.DataBind();
		}

	}
}

