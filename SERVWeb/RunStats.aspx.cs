
namespace SERVWeb
{
	using System;
	using System.Web;
	using System.Web.UI;
	using SERVBLLFactory;
	using System.Data;
	using SERVDataContract;
	using System.Collections.Generic;
	using System.Web.UI.WebControls;

	public partial class RunStats : System.Web.UI.Page
	{

		private int _idx = 0;
		private int idx
		{
			get
			{
				//_idx++;
				return _idx;
			}
		}

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad(e);
			List<Report> reports = SERVBLLFactory.Factory.RunLogBLL().RunReports();
			ContentPlaceHolder contentPlaceholderContent = (ContentPlaceHolder)Master.FindControl("contentPlaceholder");
			contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { 
				Text = "<div class=\"alert\">\n  <button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>\n  " +
				       "<strong>Please Note:</strong> This data is cached for 2 minutes.  If you log or update a run, you may need to wait before you see it displayed here.</div>" });
			foreach (Report r in reports)
			{
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "<div class=\"row\">" });
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "<div class=\"span12\">" });
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "<a id=\"" + r.Anchor + "\"></a>" });
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "<h3>" + r.Heading + "</h3>" });
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "<p>" + r.Description + "</p> " });
				DataGrid dg = new DataGrid();
				dg.CssClass = "table table-striped table-bordered table-condensed";
				dg.DataSource = r.Results;
				dg.DataBind();
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, dg);
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "</div>" });
				contentPlaceholderContent.Controls.AddAt(contentPlaceholderContent.Controls.Count-1, new Literal() { Text = "</div>" });
			}

			SERVGlobal.AssertAuthentication();

		}
	}
}

