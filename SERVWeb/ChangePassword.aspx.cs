
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;
	using SERVDataContract;
	using SERVBLL;

	public partial class ChangePassword : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}

		protected void cmdChangeClick (object src, EventArgs e)
		{
			if (txtNewPassword.Text.Trim() == string.Empty) { return; }
			User u = new Service().Login(txtEmail.Text.ToLower().Trim(), SERV.Utils.Authentication.Hash(txtEmail.Text.ToLower().Trim() + txtOldPassword.Text));
			if (u != null && txtNewPassword.Text == txtNewPassword2.Text)
			{
				new MemberBLL().SetPassword(u.Member.EmailAddress, SERV.Utils.Authentication.Hash(u.Member.EmailAddress.ToLower().Trim() + txtNewPassword.Text));
				Response.Redirect("Home.aspx?success=yes");
			}
		}

	}
}

