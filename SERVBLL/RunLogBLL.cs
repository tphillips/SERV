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

		static Logger log = new Logger();
		Dictionary<string, string> nameReplacements = new Dictionary<string, string>();

		public RunLogBLL()
		{
			// Temporary dictionary to fix common spelling mistakes
			// TEMPORARY
			nameReplacements.Add("Bowers Jeff", "Bowers Jeffrey");
			nameReplacements.Add("Chappel Chris", "Chappell Chris");
			nameReplacements.Add("Chapple Chris", "Chappell Chris");
			nameReplacements.Add("Clark Bruce", "Clarke Bruce");
			nameReplacements.Add("Davis Terry", "Davies Terry");
			nameReplacements.Add("Faulty Jon", "Fautley Jon");
			nameReplacements.Add("Faurtney Jon", "Fautley Jon");
			nameReplacements.Add("Fautney Jon", "Fautley Jon");
			nameReplacements.Add("Fourtney John", "Fautley Jon");
			nameReplacements.Add("Fourtley Jon", "Fautley Jon");
			nameReplacements.Add("Foutney John", "Fautley Jon");
			nameReplacements.Add("Foutley Jon", "Fautley Jon");
			nameReplacements.Add("Fortney John", "Fautley Jon");
			nameReplacements.Add("Gardner Bob", "Gardner William");
			nameReplacements.Add("Hardy Edd", "Hardy Edward");
			nameReplacements.Add("Heler Caruel Pierre", "Heler-Caruel Pierre");
			nameReplacements.Add("Hickey David", "Hickey Dave");
			nameReplacements.Add("Keeynoy Mike", "Keenoy Mike");
			nameReplacements.Add("Kiernon Joe", "Kiernan Joe");
			nameReplacements.Add("King", "King Paul");
			nameReplacements.Add("Kirkham Iam=n", "Kirkham Ian");
			nameReplacements.Add("Lawence David", "Lawrence David");
			nameReplacements.Add("Lock Ally", "Lock Alison");
			nameReplacements.Add("MacDonald Jan", "MacDonald Janet");
			nameReplacements.Add("McCullock Ian", "McCulloch Ian");
			nameReplacements.Add("Mizsler Frank", "Miszler Frank");
			nameReplacements.Add("McEacham Ema", "McEachan Emma");
			nameReplacements.Add("McEacham Emma", "McEachan Emma");
			nameReplacements.Add("McEchan Emma", "McEachan Emma");
			nameReplacements.Add("Ney Mike", "Ney Michael");
			nameReplacements.Add("SoperAndrew", "Soper Andrew");
			nameReplacements.Add("Soper Andy", "Soper Andrew");
			nameReplacements.Add("Semmens Raymond", "Semmens Ray");
			nameReplacements.Add("Semmons Ray", "Semmens Ray");
			nameReplacements.Add("Whiteman Martin", "Whitehead Martin");
			nameReplacements.Add("Watson Phil", "Watson Philip");
			nameReplacements.Add("Watsom Philip", "Watson Philip");
			nameReplacements.Add("Wats Philip", "Watson Philip");

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
			csv = csv.Replace("    ", " ");
			csv = csv.Replace("   ", " ");
			csv = csv.Replace("  ", " ");
			return csv;
		}

		public bool ImportRawRunLog()
		{
			log.LogStart();
			List<SERVDataContract.DbLinq.RawRunLog> records = new List<SERVDataContract.DbLinq.RawRunLog>();
			log.Debug("Truncating RawRunLog");
			SERVDALFactory.Factory.RunLogDAL().TruncateRawRunLog();
			string docURI = "https://docs.google.com/spreadsheet/pub?key=0Avzf69R2XNmVdExyQkpRa3Rtb1d1cHBqM2dINDB1N0E&single=true&gid=0&output=csv";
			log.Info("Downloading RunLog");
			System.Net.ServicePointManager.ServerCertificateValidationCallback += SERV.Utils.Authentication.AcceptAllCertificates;
			string csv = new System.Net.WebClient().DownloadString(docURI);
			csv = CleanTextBlocks(csv);
			string[] rows = csv.Split('\n');
			int rowNum = 0;
			string prevRow = "";
			log.Debug("Starting import loop");
			foreach (string row in rows)
			{
				if (rowNum > 1)
				{
					string r = row.Trim();
					if (!string.IsNullOrEmpty(r))
					{
						//log.Debug(r);
						string[] cols = r.Split(',');
						SERVDataContract.DbLinq.RawRunLog raw = new SERVDataContract.DbLinq.RawRunLog();
						try 
						{
							raw.CallDate = DateTime.Parse(cols[1].Trim().Replace("\"", ""), new System.Globalization.CultureInfo("en-GB"));
							raw.CallTime = cols[2].Trim().Replace("\"", "");
							raw.Destination = cols[3].Trim().Replace("\"", "");
							raw.CollectFrom = cols[4].Trim().Replace("\"", "");
							raw.CollectTime = cols[5].Trim().Replace("\"", "");
							raw.DeliveryTime = cols[6].Trim().Replace("\"", "");
							raw.Consignment = cols[7].Trim().Replace("\"", "");
							raw.Urgency = cols[8].Trim().Replace("\"", "");
							raw.Controller = cols[9].Trim().Replace("\"", "");
							raw.Rider = cols[10].Trim().Replace("\"", "").Replace(".", "");
							if (nameReplacements.ContainsKey(raw.Rider)) { raw.Rider = nameReplacements[raw.Rider]; }
							raw.Notes = cols[11].Trim().Replace("\"", "");
							raw.CollectTime2 = cols[12].Trim().Replace("\"", "");
							raw.Vehicle = cols[13].Trim().Replace("\"", "");
							//SERVDALFactory.Factory.RunLogDAL().CreateRawRecord(raw);
							records.Add(raw);
						} 
						catch(System.FormatException fe)
						{
							log.Error(cols[1].Trim().Replace("\"", ""), fe);
						}
						catch(Exception ex)
						{
							log.Error(string.Format("{0} ------ prev: {1} ------ current: {2}", ex.Message, prevRow, row), ex);
							Console.WriteLine(string.Format("{0} ------ {1} --------- {2}", ex.Message,row, r));
						}
					}
				}
				rowNum++;
				prevRow = row;
			}
			log.Info("Starting batch insert of " + records.Count + " records");
			SERVDALFactory.Factory.RunLogDAL().CreateRawRecords(records);
			log.Info("Batch insert complete");
			return true;
		}

		public bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
			int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
			int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, string productIdCsv)
		{
			SERVDataContract.DbLinq.RunLog log = new SERVDataContract.DbLinq.RunLog();
			log.CallDateTime = callDateTime;
			log.CallFromLocationID = callFromLocationId;
			log.CollectDateTime = collectDateTime;
			log.CollectionLocationID = collectionLocationId;
			log.ControllerMemberID = controllerMemberId;
			log.CreateDate = DateTime.Now;
			log.CreatedByUserID = createdByUserId;
			log.DeliverDateTime = deliverDateTime;
			log.DeliverToLocationID = deliverToLocationId;
			log.DutyDate = dutyDate;
			log.FinalDestinationLocationID = finalDestinationLocationId;
			log.IsTransfer = 0;
			log.OriginLocationID = originLocationId;
			log.RiderMemberID = riderMemberId;
			log.Urgency = urgency;
			log.VehicleTypeID = vehicleTypeId;
			return SERVDALFactory.Factory.RunLogDAL().CreateRunLog(log) > 0;
		}

	}
}

