
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

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication((int)SERVDataContract.UserLevel.Controller, "Sorry, only controllers and above have access to contribute to the controller log.");
		}

	}
}

