
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	
	public partial class ViewCalendar : System.Web.UI.Page
	{

		protected int MemberId
		{
			get
			{
				return SERVGlobal.User.MemberID;
			}
		}

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}

	}
}

