
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class ViewLocation : System.Web.UI.Page
	{

		protected int LocationId
		{
			get 
			{
				if (Request["locationId"] == null) { return 0; }
				return int.Parse(Request["locationId"]);
			}
		}

		protected int UserLevel
		{
			get 
			{
				return SERVGlobal.User.UserLevelID;
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
			if (Request["new"] != null)
			{
				int blank = SERVGlobal.Service.CreateBlankLocation(SERVGlobal.User);
				Response.Redirect("ViewLocation.aspx?locationId=" + blank + "&message=Make sure the location you intend on adding does not already exist!");
			}
		}

	}
}

