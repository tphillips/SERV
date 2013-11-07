using System;
using System.Web;
using System.Web.UI;
using SERVDataContract;

namespace SERVWeb
{

	public partial class Login : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			if (Request["Logout"] != null) { SERVGlobal.User = null; Response.Redirect("Default.aspx"); }
		}

		protected void cmdLoginClick (object src, EventArgs e)
		{
			User u = new Service().Login(txtEmail.Text, txtPassword.Text);
			if (u != null)
			{
				SERVGlobal.User = u;
				Response.Redirect("Default.aspx");
			}
		}

	}
}

