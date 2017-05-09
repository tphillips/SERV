using System;
using System.Data;
using System.Collections.Generic;

namespace SERVDataContract
{
	[Serializable]
	public class Report
	{
		public Report()
		{
		}

		public string Heading { get; set; }
		public string Description { get;set; }
		public string Anchor { get;set; }
		public string Query { get; set; }
		public DataTable Results { get; set; }

		public static Report GetByAnchor(List<Report> reports, string anchor)
		{
			foreach (Report r in reports)
			{
				if (r.Anchor == anchor)
				{
					return r;
				}
			}
			return null;
		}

	}
}

