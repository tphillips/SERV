using System;
using System.Web;
using System.Web.UI;
using SERVDataContract.DbLinq;

namespace SERVWeb
{
	public partial class TopControl : System.Web.UI.UserControl
	{
		protected override void OnLoad (EventArgs e)
		{
			pnlPowerUser.Visible = false;
			pnlNotLoggedIn.Visible = true;
			pnlLoggedIn.Visible = false;
			if (SERVGlobal.User != null)
			{
				pnlLoggedIn.Visible = true;
				pnlNotLoggedIn.Visible = false;
				litLoginName.Text = string.Format("{0} <i title=\"Keep Alive \nGreen: Your session is alive and being kept alive. \n" +
					"Grey: You are logged in but your session is not being activley kept alive (normal). \n" +
					"Red: Keep alive failed and you need to login again (you can do that on another tab).\" " +
					"id=\"icoSessionStatus\" class=\"icon-globe\"></i>", SERVGlobal.User.Member.FirstName);
				if (SERVGlobal.User.UserLevelID > (int)SERVDataContract.UserLevel.Volunteer)
				{
					pnlPowerUser.Visible = true;
				}
			}

		}
	}
}

