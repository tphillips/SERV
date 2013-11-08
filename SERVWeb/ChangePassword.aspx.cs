
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;
	using SERVDataContract;

	public partial class ChangePassword : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}

		protected void cmdChangeClick (object src, EventArgs e)
		{
			User u = new Service().Login(txtEmail.Text.ToLower().Trim(), SERV.Utils.Authentication.Hash(txtEmail.Text.ToLower().Trim() + txtOldPassword.Text));
			if (u != null && txtNewPassword.Text == txtNewPassword2.Text)
			{
				SERVBLLFactory.Factory.MemberBLL().SetPassword(u.Member.EmailAddress, SERV.Utils.Authentication.Hash(u.Member.EmailAddress.ToLower().Trim() + txtNewPassword.Text));
				Response.Redirect("Default.aspx?success=yes");
			}
		}

	}
}

