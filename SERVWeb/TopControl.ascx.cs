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
				litLoginName.Text = string.Format("{0} <i id=\"icoSessionStatus\" class=\"icon-certificate icon-green\"></i>", SERVGlobal.User.Member.FirstName);
				if (SERVGlobal.User.UserLevelID > (int)SERVDataContract.UserLevel.Volunteer)
				{
					pnlPowerUser.Visible = true;
				}
			}

		}
	}
}

