
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class OpsMap : System.Web.UI.Page
	{

		protected int UserLevel
		{
			get 
			{
				return SERVGlobal.User.UserLevelID;
			}
		}

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}

	}
}

