
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	
	public partial class ViewRota : System.Web.UI.Page
	{

		protected int CalendarId
		{
			get 
			{
				if (Request["CalendarId"] == null) { return 0; }
				return int.Parse(Request["CalendarId"]);
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
			if (CalendarId == 0)
			{
				Response.Redirect("Calendars.aspx");
			}
		}

	}
}

