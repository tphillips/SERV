using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	// Short term raw import until all controllers are using the system directly.
	// 1st hacky draft, needs refactoring
	public class RunLogBLL : IRunLogBLL
	{
		public RunLogBLL()
		{
		}

		static string CleanTextBlocks(string csv)
		{
			bool inText = false;
			int stop = csv.Length;
			for (int x = 0; x < stop; x++)
			{
				if (csv.Substring(x, 1) == "\"")
				{
					inText = !inText;
				}
				if (csv.Substring(x, 1) == "," && inText)
				{
					csv = csv.Remove(x, 1);
					stop--;
				}
				if (csv.Substring(x, 1) == "\r" && inText)
				{
					csv = csv.Remove(x, 1);
					stop--;
				}
				if (csv.Substring(x, 1) == "\n" && inText)
				{
					csv = csv.Remove(x, 1);
					stop--;
				}
			}
			return csv;
		}

		public bool ImportRawRunLog()
		{
			string docURI = "https://docs.google.com/spreadsheet/pub?key=0Avzf69R2XNmVdExyQkpRa3Rtb1d1cHBqM2dINDB1N0E&single=true&gid=0&output=csv";
			string csv = new System.Net.WebClient().DownloadString(docURI);
			csv = CleanTextBlocks(csv);
			string[] rows = csv.Split('\n');
			int rowNum = 0;
			foreach (string row in rows)
			{
				if (rowNum > 1)
				{
					string r = row.Trim();
					if (!string.IsNullOrEmpty(r))
					{
						string[] cols = r.Split(',');
						SERVDataContract.DbLinq.RawRunLog raw = new SERVDataContract.DbLinq.RawRunLog();
						try 
						{
							raw.CallDate = cols[1].Trim().Replace("\"", "");
							raw.CallTime = cols[2].Trim().Replace("\"", "");
							raw.Destination = cols[3].Trim().Replace("\"", "");
							raw.CollectFrom = cols[4].Trim().Replace("\"", "");
							raw.CollectTime = cols[5].Trim().Replace("\"", "");
							raw.DeliveryTime = cols[6].Trim().Replace("\"", "");
							raw.Consignment = cols[7].Trim().Replace("\"", "");
							raw.Urgency = cols[8].Trim().Replace("\"", "");
							raw.Controller = cols[9].Trim().Replace("\"", "");
							raw.Rider = cols[10].Trim().Replace("\"", "");
							raw.Notes = cols[11].Trim().Replace("\"", "");
							raw.CollectTime2 = cols[12].Trim().Replace("\"", "");
							raw.Vehicle = cols[13].Trim().Replace("\"", "");
							SERVDALFactory.Factory.RunLogDAL().CreateRawRecord(raw);
						} 
						catch(Exception ex)
						{
							Console.WriteLine(string.Format("{0} ------ {1} --------- {2}", ex.Message,row, r));
						}
					}
				}
				rowNum++;
			}
			return true;
		}

	}
}

