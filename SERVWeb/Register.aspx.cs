
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;

	
	public partial class Register : System.Web.UI.Page
	{

		protected override void OnLoad (EventArgs e)
		{
			if (SERVGlobal.User != null)
			{
				System.Web.HttpContext.Current.Response.Redirect("Home.aspx?message=You appear to already be registered!");
			}
		}

	}
}

