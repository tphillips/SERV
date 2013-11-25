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
			User u = new Service().Login(txtEmail.Text, SERV.Utils.Authentication.Hash(txtEmail.Text.ToLower().Trim() + txtPassword.Text));
			if (u != null)
			{
				SERVGlobal.User = u;
				if (Request["dest"] != null && Request["dest"] != "")
				{
					Response.Redirect(Request["dest"].ToString());
				}
				Response.Redirect("Home.aspx");
			}
		}

	}
}

