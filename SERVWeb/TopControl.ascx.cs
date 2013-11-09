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
			if (SERVGlobal.User == null)
			{
				pnlNotLoggedIn.Visible = true;
				pnlLoggedIn.Visible = false;
			}
			else
			{
				pnlLoggedIn.Visible = true;
				pnlNotLoggedIn.Visible = false;
				litLoginName.Text = string.Format("{0} {1}", SERVGlobal.User.Member.FirstName, SERVGlobal.User.Member.LastName);
			}
		}
	}
}

