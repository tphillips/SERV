
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	
	public partial class Calendar : System.Web.UI.Page
	{

		protected int MemberId
		{
			get
			{
				return SERVGlobal.User.MemberID;
			}
		}

		protected int UserLevel
		{
			get 
			{
				return SERVGlobal.User.UserLevelID;
			}
		}

		protected int PageNum
		{
			get 
			{
				if (Request["Page"] != null)
				{
					return int.Parse(Request["Page"].ToString());
				}
				return 0;
			}
		}

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}

	}
}

