
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
			User u = new Service().Login(txtEmail.Text, txtOldPassword.Text);
			if (u != null && txtNewPassword.Text == txtNewPassword2.Text)
			{
				SERVBLLFactory.Factory.MemberBLL().SetPassword(u.Member.EmailAddress, txtNewPassword.Text);
			}
		}

	}
}

