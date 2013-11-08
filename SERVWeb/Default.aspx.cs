using System;
using System.Web;
using System.Web.UI;

namespace SERVWeb
{
	public partial class Default : System.Web.UI.Page
	{
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
	}
}

