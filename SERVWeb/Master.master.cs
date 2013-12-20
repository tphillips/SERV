
namespace SERVWeb
{
    using System;
    using System.Web;
    using System.Web.UI;

    public partial class Master : System.Web.UI.MasterPage
    {
		protected override void OnLoad (EventArgs e)
		{
			if (Request["NoTopMenu"] != null)
			{
				topControl.Visible = false;
			}
		}
    }
}

