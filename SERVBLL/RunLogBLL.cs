using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;
using System.Data;

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
				if (x % 200 == 0) { System.Threading.Thread.Sleep(5); }
				if (x % 1000 == 0)
				{
					log.Info(string.Format("Processed {0} of {1} csv chars", x, stop));
					System.Threading.Thread.Sleep(10);  // take a breath
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
			System.Net.ServicePointManager.ServerCertificateValidationCallback += SERV.Utils.Authentication.AcceptAllCertificates;
			bool OK = false;
			string csv = "";
			while (!OK)
			{
				log.Info("Downloading RunLog");
				try
				{
					csv = new System.Net.WebClient().DownloadString(docURI);
					OK = true;
				}
				catch(Exception ex)
				{
					log.Error(ex.Message, ex);
				}
			}
			log.Info("Download OK");
			log.Info("Cleaning CSV");
			csv = CleanTextBlocks(csv);
			log.Info("Processing Rows");
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
							raw.CallTime = FixTime(cols[2].Trim());
							raw.Destination = cols[3].Trim().Replace("\"", "");
							raw.CollectFrom = cols[4].Trim().Replace("\"", "");
							raw.CollectTime = FixTime(cols[5].Trim());
							raw.DeliveryTime = FixTime(cols[6].Trim());
							raw.Consignment = cols[7].Trim().Replace("\"", "");
							raw.Urgency = cols[8].Trim().Replace("\"", "");
							raw.Controller = cols[9].Trim().Replace("\"", "");
							raw.Rider = cols[10].Trim().Replace("\"", "").Replace(".", "");
							if (nameReplacements.ContainsKey(raw.Rider)) { raw.Rider = nameReplacements[raw.Rider]; }
							raw.Notes = cols[11].Trim().Replace("\"", "");
							raw.CollectTime2 = FixTime(cols[12].Trim());
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
				System.Threading.Thread.Sleep(5);
				if (rowNum % 100 == 0)
				{
					log.Info("Starting batch insert of " + records.Count + " records");
					SERVDALFactory.Factory.RunLogDAL().CreateRawRecords(records);
					records = new List<SERVDataContract.DbLinq.RawRunLog>();
					System.Threading.Thread.Sleep(50);
				}
			}
			log.Info("Starting batch insert of " + records.Count + " records");
			SERVDALFactory.Factory.RunLogDAL().CreateRawRecords(records);
			log.Info("Taking out the trash");
			GC.Collect();
			log.Info("Batch insert complete");
			return true;
		}

		private string FixTime(string time)
		{
			time = time.Replace("\"", "").Replace(".", ":");
			if (time.Contains(":"))	{ time = time.Split(':')[0].PadLeft(2, '0') + ":" + time.Split(':')[1].PadRight(2, '0'); }
			return time;
		}

		public bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
			int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
			int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, 
			string productIdCsv, DateTime? homeSafeDateTime, string notes)
		{
			List<int> prods = new List<int>();
			foreach (string p in productIdCsv.Split(','))
			{
				if (p.Trim() != string.Empty) { prods.Add(int.Parse(p)); }
			}
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
			log.HomeSafeDateTime = homeSafeDateTime;
			log.Notes = notes;
			log.Boxes = prods.Count;
			// Not completed run
			if (riderMemberId == -1)
			{
				log.RiderMemberID = null;
				log.CollectDateTime = null;
				log.DeliverDateTime = null;
				log.HomeSafeDateTime = null;
				log.VehicleTypeID = null;
			}
			return SERVDALFactory.Factory.RunLogDAL().CreateRunLog(log, prods) > 0;
		}

		public bool CreateAARunLog(DateTime dutyDate, DateTime collectDateTime, int controllerMemberId, 
			int createdByUserId, DateTime deliverDateTime, DateTime returnDateTime, int riderMemberId, 
			int vehicleTypeId, string boxesOutCsv, string boxesInCsv, string notes)
		{
			List<int> prodsOut = new List<int>();
			foreach (string p in boxesOutCsv.Split(','))
			{
				if (p.Trim() != string.Empty) { prodsOut.Add(int.Parse(p)); }
			}
			List<int> prodsIn = new List<int>();
			foreach (string p in boxesInCsv.Split(','))
			{
				if (p.Trim() != string.Empty) { prodsIn.Add(int.Parse(p)); }
			}
			// Out trip
			if (prodsOut.Count > 0)
			{
				SERVDataContract.DbLinq.RunLog log = new SERVDataContract.DbLinq.RunLog();
				log.CallDateTime = null;
				log.CallFromLocationID = FindAADeliverLocationID();
				log.CollectDateTime = collectDateTime;
				log.CollectionLocationID = FindAAPickupLocationID();
				log.ControllerMemberID = controllerMemberId;
				log.CreateDate = DateTime.Now;
				log.CreatedByUserID = createdByUserId;
				log.DeliverDateTime = deliverDateTime;
				log.DeliverToLocationID = FindAADeliverLocationID();
				log.DutyDate = dutyDate;
				log.FinalDestinationLocationID = FindAADeliverLocationID();
				log.IsTransfer = 0;
				log.OriginLocationID = FindAAPickupLocationID();
				log.RiderMemberID = riderMemberId;
				log.Urgency = 1;
				log.VehicleTypeID = vehicleTypeId;
				log.Notes = notes;
				log.Boxes = prodsOut.Count;
				SERVDALFactory.Factory.RunLogDAL().CreateRunLog(log, prodsOut);
			}
			// In Trip
			if (prodsIn.Count > 0)
			{
				SERVDataContract.DbLinq.RunLog log = new SERVDataContract.DbLinq.RunLog();
				log.CallDateTime = null;
				log.CallFromLocationID = FindAADeliverLocationID();
				log.CollectDateTime = deliverDateTime;
				log.CollectionLocationID = FindAADeliverLocationID();
				log.ControllerMemberID = controllerMemberId;
				log.CreateDate = DateTime.Now;
				log.CreatedByUserID = createdByUserId;
				log.DeliverDateTime = returnDateTime;
				log.DeliverToLocationID = FindAAPickupLocationID();
				log.DutyDate = dutyDate;
				log.FinalDestinationLocationID = FindAAPickupLocationID();
				log.IsTransfer = 0;
				log.OriginLocationID = FindAADeliverLocationID();
				log.RiderMemberID = riderMemberId;
				log.Urgency = 1;
				log.VehicleTypeID = vehicleTypeId;
				log.Notes = notes;
				log.Boxes = prodsIn.Count;
				SERVDALFactory.Factory.RunLogDAL().CreateRunLog(log, prodsIn);
			}
			return true;
		}

		public void DeleteRun(int runLogID)
		{
			SERVDALFactory.Factory.RunLogDAL().DeleteRunLog(runLogID);
		}

		public RunLog Get(int runLogID)
		{
			SERVDataContract.DbLinq.RunLog metal = SERVDALFactory.Factory.RunLogDAL().Get(runLogID);
			RunLog ret = new RunLog(metal);
			ret.Products = new Dictionary<string, int>();
			foreach (SERVDataContract.DbLinq.RunLogProduct rlp in metal.RunLogProduct)
			{
				if (ret.Products.ContainsKey(rlp.ProductID.ToString()))
				{
					ret.Products[rlp.ProductID.ToString()] = ret.Products[rlp.ProductID.ToString()] + rlp.Quantity;
				}
				else
				{
					ret.Products.Add(rlp.ProductID.ToString(), rlp.Quantity);
				}
			}
			ret.Vehicle = metal.VehicleTypeID != null ? metal.VehicleType.VehicleType1 : "";
			return ret;
		}

		int FindAAPickupLocationID()
		{
			return new LocationBLL().ListLocations("East Surrey")[0].LocationID;
		}

		int FindAADeliverLocationID()
		{
			return new LocationBLL().ListLocations("Redhill Aerodrome")[0].LocationID;
		}

		public List<Report> RunReports()
		{
			List<Report> reports = new List<Report>();

			Report rep = new Report();
			rep.Heading = "Recent Runs";
			rep.Description = "Real time information from the controller log.";
			rep.Anchor = "runLog";
			rep.Query = "select RunLogID as ID, date(DutyDate) as 'Duty Date', coalesce(CallDateTime, 'N/A') as 'Call Date & Time', cf.Location as 'Call From', cl.Location as 'From', " +
			            "dl.Location as 'To', coalesce(time(rl.CollectDateTime), 'NOT ACCEPTED') as Collected, time(rl.DeliverDateTime) as Delivered, " +
			            //"timediff(rl.DeliverDateTime, rl.CollectDateTime) as 'Run Time', " +
			            "fl.Location as 'Destination', concat(m.FirstName, ' ', m.LastName) as Rider, v.VehicleType as 'Vehicle', rl.Description as 'Consignment', " +
			            "concat(c.FirstName, ' ', c.LastName) as Controller from RunLog rl " +
			            "left join Member m on m.MemberID = rl.RiderMemberID " +
			            "join Member c on c.MemberID = rl.ControllerMemberID " +
			            "join Location cf on cf.LocationID = rl.CallFromLocationID " +
			            "join Location cl on cl.LocationID = rl.CollectionLocationID " +
			            "join Location dl on dl.LocationID = rl.DeliverToLocationID " +
			            "join Location fl on fl.LocationID = rl.FinalDestinationLocationID " +
			            "left join VehicleType v on v.VehicleTypeID = rl.VehicleTypeID " +
			            "where DutyDate > '2013-12-31' or CallDateTime > '2013-12-31' " +
			            "order by rl.DutyDate desc, rl.CallDateTime desc LIMIT 30;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Top 10 Rider / Drivers";
			rep.Description = "Based on number of runs done in 2014 and on. Ordered by name.";
			rep.Anchor = "top10";
			rep.Query = "select Name from " +
			            "(select CONCAT(m.FirstName, ' ', m.LastName) Name, count(*) Runs " +
			            "from RunLog rl " +
			            "LEFT join Member m on m.MemberID = rl.RiderMemberID " +
			            "where rl.RiderMemberID is not null " +
			            "group by Name " +
			            "order by Runs desc " +
			            "LIMIT 10) top " +
			            "order by Name;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Top 3 Controllers";
			rep.Description = "Based on number of runs controlled in 2014 and on. Ordered by name.";
			rep.Anchor = "top10Controllers";
			rep.Query = "select Name from " +
			            "(select CONCAT(m.FirstName, ' ', m.LastName) Name, count(*) Runs " +
			            "from RunLog rl " +
			            "LEFT join Member m on m.MemberID = rl.ControllerMemberID " +
			            "group by Name " +
			            "order by Runs desc " +
			            "LIMIT 3) top " +
			            "order by Name;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Todays Users";
			rep.Description = "Users who have logged on today.";
			rep.Anchor = "todaysUsers";
			rep.Query = "select CONCAT(m.FirstName, ' ', m.LastName) as Member " +
			            "from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate > CURRENT_DATE() " +
			            "order by lastLoginDate desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Boxes by Product by Month";
			rep.Description = "The count of <strong>boxes carried</strong> (not runs!), by product, by month.";
			rep.Anchor = "boxesByProdByMonth";
			rep.Query =  "select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", p.Product, sum(rlp.Quantity) as Boxes from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month, Product " +
			            "order by month(rl.DutyDate), Product;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Blood Product Runs by Month";
			rep.Description = "The count of runs carrying a blood product (Blood, Platelets & Plasma).";
			rep.Anchor = "bloodRunsByMonth";
			rep.Query =  "select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", count(distinct rl.RunLogID) as Runs from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "AND rlp.ProductID in (1,2,3) " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month " +
			            "order by month(rl.DutyDate);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Core Runs by Month";
			rep.Description = "Runs by month excluding AA and Milk.";
			rep.Anchor = "coreRunsByMonth";
			rep.Query =  "select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", count(distinct rl.RunLogID) as Runs from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "AND rlp.ProductID in (1,2,3, 4, 15,16) " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month " +
			            "order by month(rl.DutyDate);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Sample Runs by Month";
			rep.Description = "Sample runs by month.";
			rep.Anchor = "sampleRunsByMonth";
			rep.Query =  "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", count(distinct rl.RunLogID) as Runs from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "AND rlp.ProductID in (4) " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month " +
			            "order by month(rl.DutyDate);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Package Runs by Month";
			rep.Description = "Package runs by month.";
			rep.Anchor = "packageRunsByMonth";
			rep.Query =  "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", count(distinct rl.RunLogID) as Runs from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "AND rlp.ProductID in (16) " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month " +
			            "order by month(rl.DutyDate);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Milk Runs by Month";
			rep.Description = "Milk runs by month.";
			rep.Anchor = "milkRunsByMonth";
			rep.Query =  "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", count(distinct rl.RunLogID) as Runs from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "AND rlp.ProductID in (5) " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month " +
			            "order by month(rl.DutyDate);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "AA Runs by Month";
			rep.Description = "AA runs by month.";
			rep.Anchor = "aaRunsByMonth";
			rep.Query =  "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", count(distinct rl.RunLogID) as Runs from RunLog rl " +
			            "join RunLog_Product rlp on rlp.RunLogID = rl.RunLogID " +
			            "join Product p on p.ProductID = rlp.ProductID " +
			            "AND rlp.ProductID in (7,8,9,10,11,12,13,14) " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month " +
			            "order by month(rl.DutyDate);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Runs by Member Month";
			rep.Description = "Runs by member by month.";
			rep.Anchor = "memberRunsByMonth";
			rep.Query =  "select concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", concat(m.FirstName, ' ', m.LastName) as Member, count(*) as Runs from RunLog rl  " +
			            "join Member m on m.MemberID = rl.RiderMemberID " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month, Member " +
			            "order by month(rl.DutyDate), count(*) desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Runs by Final Destination by Month";
			rep.Description = "Runs by final destination (where the product ended up) by month.";
			rep.Anchor = "finalDestByMonth";
			rep.Query =  "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
			            ", l.Location as Location, count(*) Runs from RunLog rl  " +
			            "join Location l on l.LocationID = rl.FinalDestinationLocationID " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month, l.Location " +
			            "order by month(rl.DutyDate), Runs desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Runs by Pickup Location by Month";
			rep.Description = "Runs by pickup location by month.";
			rep.Anchor = "pickupDestByMonth";
			rep.Query = "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
						", l.Location as Location, count(*) as Runs from RunLog rl " +
						"join Location l on l.LocationID = rl.CollectionLocationID " +
			            "where rl.RiderMemberID is not null " +
						"group by Month, l.Location " +
						"order by month(rl.DutyDate), Runs desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Runs by Drop Off Location by Month";
			rep.Description = "Runs by drop off location by month.";
			rep.Anchor = "dropoffDestByMonth";
			rep.Query = "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
						", l.Location as Location, count(*) as Runs from RunLog rl " +
						"join Location l on l.LocationID = rl.DeliverToLocationID " +
			            "where rl.RiderMemberID is not null " +
						"group by Month, l.Location " +
						"order by month(rl.DutyDate), Runs desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Runs by Caller by Month";
			rep.Description = "Runs by Caller by month.";
			rep.Anchor = "callerByMonth";
			rep.Query = "select  concat(MONTHNAME(rl.DutyDate), ' ', year(rl.DutyDate)) as Month " +
						", l.Location as Caller, count(*) as Runs from RunLog rl " +
			            "join Location l on l.LocationID = rl.CallFromLocationID " +
			            "where rl.RiderMemberID is not null " +
			            "group by Month, l.Location " +
			            "order by month(rl.DutyDate), Runs desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Active Member - No Login";
			rep.Description = "This report shows members who have done a run since Jan 14 but not yet logged into the new system.";
			rep.Anchor = "activeNoLogin";
			rep.Query = "select CONCAT(m.FirstName, ' ', m.LastName) as Rider, date(m.JoinDate) as Joined, m.EmailAddress as Email, date(max(rr.CallDateTime)) as LastRun, count(*) as Runs " +
			            "from RunLog rr  " +
			            "LEFT join Member m on rr.RiderMemberID = m.MemberID  " +
			            "where m.MemberID not in " +
			            "(select m.MemberID from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null) " +
			            "and rr.CallDateTime > '2014-01-01' " +
			            "and m.LeaveDate is null " +
			            "group by m.MemberID " +
			            "order by max(rr.CallDateTime) desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Latest Run Date by Member";
			rep.Description = "This report shows the last run date by member in 2014.";
			rep.Anchor = "lastRunByMember";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', m.MobileNumber as Phone,  coalesce(date(LastDuty), '<span style=\"color:red\">NOTHING IN 2014</span>') as 'Last Run' from Member m " +
			            "left join " +
			            "( " +
			            "select RiderMemberID, max(DutyDate) as LastDuty from RunLog group by RiderMemberID " +
			            ") rl on m.MemberID = rl.RiderMemberID " +
			            "where m.LeaveDate is null " +
			            "order by LastName;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Last Month's Statistics";
			rep.Description = "High level stats for last month.";
			rep.Anchor = "lastMonth";
			rep.Query = "call LastMonthRunStats;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "This Month's Statistics";
			rep.Description = "High level stats for this month to date.";
			rep.Anchor = "thisMonth";
			rep.Query = "call ThisMonthRunStats;";
			reports.Add(rep);

			foreach (Report r in reports)
			{
				r.Results = SERVDALFactory.Factory.RunLogDAL().RunReport(r);
			}

			return reports;
		}

		public DataTable Report_RunLog()
		{
			return SERVDALFactory.Factory.RunLogDAL().Report_RunLog();
		}

	}
}

