using System;
using System.Web;
using System.Web.UI;
using SERVDataContract;

namespace SERVWeb
{

	public partial class Login : System.Web.UI.Page
	{

		protected void cmdLoginClick (object src, EventArgs e)
		{
			SERVUser u = new Service().Login(txtEmail.Text, txtPassword.Text);
			if (u != null)
			{
				SERVGlobal.User = u;
				Response.Redirect("Default.aspx");
			}
		}

	}
}

