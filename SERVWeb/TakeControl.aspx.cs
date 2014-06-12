
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	
	public partial class TakeControl : System.Web.UI.Page
	{

		protected string DefaultNumber { get; set; }

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication((int)SERVDataContract.UserLevel.Controller, "Sorry, you don't have access to take control.");
			this.DefaultNumber = SERVGlobal.User.Member.MobileNumber.Replace(" ","");
		}

	}
}

