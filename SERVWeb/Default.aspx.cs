using System;
using System.Web;
using System.Web.UI;
using SERVDataContract;

namespace SERVWeb
{
	public partial class Default : System.Web.UI.Page
	{

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
			else
			{
				litServerClient.Text = "<script>niceAlert(\"Sorry, either your email address or password is incorrect.  For help logging in for the first time, please consult the forum.\");</script>";
			}
		}

	}
}

