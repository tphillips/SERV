using System;
using System.Web;
using System.Web.UI;
using SERVDataContract.DbLinq;

namespace SERVWeb
{

	public partial class ViewMember : System.Web.UI.Page
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

		protected override void OnLoad (EventArgs e)
		{

		}

	}

}

