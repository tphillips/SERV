
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class Home : System.Web.UI.Page
	{

		protected string Username
		{
			get
			{
				if (SERVGlobal.User != null)
				{
					return SERVGlobal.User.Member.FirstName;
				}
				else
				{
					return "";
				}
			}
		}

		protected string Success
		{
			get
			{
				if (Request["success"] == null)
				{
					return "";
				}
				return Request["success"].ToString();
			}
		}

		protected string Message
		{
			get
			{
				if (Request["message"] == null)
				{
					return "";
				}
				return Request["message"].ToString();
			}
		}

		protected override void OnLoad (EventArgs e)
		{
			SERVGlobal.AssertAuthentication();
		}

	}
}

