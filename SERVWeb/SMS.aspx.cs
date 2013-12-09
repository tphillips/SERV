
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class SMS : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication((int)SERVDataContract.UserLevel.Controller, "Sorry, you don't have access to the Bulk SMS tool.");
		}

		protected void cmdSendClick (object src, EventArgs e)
		{
			SERV.Utils.Messaging.SendTextMessage(txtCustomNumbers.Text, txtSMS.Text);
			Response.Redirect("Home.aspx?success=yes");
		}

	}
}

