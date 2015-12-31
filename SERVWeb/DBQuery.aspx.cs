using System;
using System.Web;
using System.Web.UI;

namespace SERVWeb
{
	
	public partial class DBQuery : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication((int)SERVDataContract.UserLevel.Admin, "Sorry, only admins can access this page.");
		}
		
	}
}

