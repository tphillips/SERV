using System;
using SERVDataContract;

namespace SERVWeb
{
	public static class SERVGlobal
	{

		public static Service Service = new Service();

		public static User User
		{
			get
			{
				if (System.Web.HttpContext.Current.Session["User"] == null) { return null; }
				return (User)System.Web.HttpContext.Current.Session["User"];
			}
			set
			{
				System.Web.HttpContext.Current.Session["User"] = value;
			}
		}

		public static void GotoDefault()
		{
			System.Web.HttpContext.Current.Response.Redirect("Default.aspx");
		}

	}
}

