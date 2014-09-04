using System;
using System.Web;
using System.Web.UI;

namespace SERVWeb
{
	public partial class BloodBoxBingo : System.Web.UI.Page
	{
		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}
	}
}

