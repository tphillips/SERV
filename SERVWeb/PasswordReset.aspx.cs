
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class PasswordReset : System.Web.UI.Page
	{

		protected void cmdResetClick (object src, EventArgs e)
		{
			if (txtEmail.Text.Trim() == string.Empty) { return; }

			SERVBLLFactory.Factory.MemberBLL().SendPasswordReset(txtEmail.Text.Trim());
			Response.Redirect("Login.aspx");

		}

	}
}

