
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class ControllerLog : System.Web.UI.Page
	{

		protected int MemberId
		{
			get 
			{
				return SERVGlobal.User.MemberID;
			}
		}

		protected bool ShowAA
		{
			get 
			{
				return Request["AA"] != null;
			}
		}

		protected int UserLevel
		{
			get 
			{
				return SERVGlobal.User.UserLevelID;
			}
		}

		protected string MemberName
		{
			get 
			{
				return string.Format("{0} {1}", SERVGlobal.User.Member.LastName, SERVGlobal.User.Member.FirstName);
			}
		}

		protected int RunLogID
		{
			get 
			{
				if (Request["RunLogID"] != null)
				{
					return int.Parse(Request["RunLogID"]);
				}
				else
				{
					return -1;
				}
			}
		}

		protected override void OnLoad (EventArgs e)
		{
			if (Request["Delete"] != null)
			{
				SERVGlobal.AssertAuthentication((int)SERVDataContract.UserLevel.Admin, "Sorry, only administrators can delete from the controller log.");
				SERVGlobal.Service.DeleteRun(RunLogID);
				Response.Redirect("RecentRuns.aspx");
			}
			if (RunLogID < 0)
			{
				SERVGlobal.AssertAuthentication((int)SERVDataContract.UserLevel.Controller, "Sorry, only controllers and above have access to contribute to the controller log.");
			}
		}

	}
}

