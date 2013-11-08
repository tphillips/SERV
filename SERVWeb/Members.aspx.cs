
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class Members : System.Web.UI.Page
	{

		protected int MemberId
		{
			get 
			{
				if (Request["self"] == "yes")
				{
					return SERVGlobal.User.MemberID;
				}
				if (Request["memberId"] == null) { return 0; }
				return int.Parse(Request["memberId"]);
			}
		}

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

