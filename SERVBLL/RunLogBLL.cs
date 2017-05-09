using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;
using System.Data;
using System.Linq;
using SERVDAL;

namespace SERVBLL
{
	// Short term raw import until all controllers are using the system directly.
	// 1st hacky draft, needs refactoring
	public class RunLogBLL
	{

		static Logger log = new Logger();

		public RunLogBLL()
		{
		}

		public bool MarkOrderAsAccepted(int memberId, int runLogId)
		{
			log.LogStart();
			RunLog r = Get(runLogId);
			if (r.AcceptedDateTime == null && r.RiderMemberID == memberId)
			{
				new RunLogDAL().SetAcceptedDateTime(runLogId);
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<RunLog> ListQueuedOrders()
		{
			log.LogStart();
			List<RunLog> ret = new List<RunLog>();
			List<SERVDataContract.DbLinq.RunLog> lret = new RunLogDAL().ListQueuedOrders();
			foreach(SERVDataContract.DbLinq.RunLog rl in lret)
			{
				ret.Add(Get(rl.RunLogID));
			}
			return ret;
		}

		public List<RunLog> ListQueuedOrdersForMember(int memberId)
		{
			log.LogStart();
			List<RunLog> ret = new List<RunLog>();
			List<SERVDataContract.DbLinq.RunLog> lret = new RunLogDAL().ListQueuedOrdersForMember(memberId);
			foreach(SERVDataContract.DbLinq.RunLog rl in lret)
			{
				ret.Add(Get(rl.RunLogID));
			}
			return ret;
		}

		public int CreateOrder(RunLog run, List<int> products)
		{
			log.LogStart();
			SERVDataContract.DbLinq.RunLog runLog = new SERVDataContract.DbLinq.RunLog();
			runLog.CallDateTime = DateTime.Now;
			runLog.CallFromLocationID = run.CallFromLocationID;
			runLog.CollectionLocationID = run.CollectionLocationID;
			runLog.ControllerMemberID = 0; //TODO System COntroller Id;
			runLog.CreateDate = DateTime.Now;
			runLog.CreatedByUserID = run.CreatedByUserID;
			runLog.DeliverToLocationID = run.DeliverToLocationID;
			runLog.DutyDate = run.DutyDate;
			runLog.FinalDestinationLocationID = run.FinalDestinationLocationID;
			runLog.OriginLocationID = run.OriginLocationID;
			runLog.Urgency = run.Urgency;
			runLog.Notes = run.Notes;
			runLog.Boxes = products.Count;
			runLog.CallerNumber = run.CallerNumber.Trim().Replace(" ", "");
			runLog.CallerExt = run.CallerExt.Trim().Replace(" ", "");
			runLog.RiderMemberID = null;
			runLog.CollectDateTime = null;
			runLog.DeliverDateTime = null;
			runLog.HomeSafeDateTime = null;
			runLog.VehicleTypeID = null;
			return new RunLogDAL().CreateRunLog(runLog, products);
		}

		public bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
			int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
			int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, 
			string productIdCsv, DateTime? homeSafeDateTime, string notes, string callerNumber, string callerExt)
		{
			log.LogStart();
			List<int> prods = new List<int>();
			foreach (string p in productIdCsv.Split(','))
			{
				if (p.Trim() != string.Empty) { prods.Add(int.Parse(p)); }
			}
			SERVDataContract.DbLinq.RunLog runLog = new SERVDataContract.DbLinq.RunLog();
			runLog.CallDateTime = callDateTime;
			runLog.CallFromLocationID = callFromLocationId;
			runLog.CollectDateTime = collectDateTime;
			runLog.CollectionLocationID = collectionLocationId;
			runLog.ControllerMemberID = controllerMemberId;
			runLog.CreateDate = DateTime.Now;
			runLog.CreatedByUserID = createdByUserId;
			runLog.DeliverDateTime = deliverDateTime;
			runLog.DeliverToLocationID = deliverToLocationId;
			runLog.DutyDate = dutyDate;
			runLog.FinalDestinationLocationID = finalDestinationLocationId;
			runLog.IsTransfer = 0;
			runLog.OriginLocationID = originLocationId;
			runLog.RiderMemberID = riderMemberId;
			runLog.Urgency = urgency;
			runLog.VehicleTypeID = vehicleTypeId;
			runLog.HomeSafeDateTime = homeSafeDateTime;
			runLog.Notes = notes;
			runLog.Boxes = prods.Count;
			runLog.CallerNumber = callerNumber.Trim().Replace(" ", "");
			runLog.CallerExt = callerExt.Trim().Replace(" ", "");
			// Not completed run
			if (riderMemberId == -1)
			{
				runLog.RiderMemberID = null;
				runLog.CollectDateTime = null;
				runLog.DeliverDateTime = null;
				runLog.HomeSafeDateTime = null;
				runLog.VehicleTypeID = null;
			}
			return new RunLogDAL().CreateRunLog(runLog, prods) > 0;
		}

		public bool CreateAARunLog(DateTime dutyDate, DateTime collectDateTime, int controllerMemberId, 
			int createdByUserId, DateTime deliverDateTime, DateTime returnDateTime, int riderMemberId, 
			int vehicleTypeId, string boxesOutCsv, string boxesInCsv, string notes)
		{
			log.LogStart();
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
				SERVDataContract.DbLinq.RunLog runLog = new SERVDataContract.DbLinq.RunLog();
				runLog.CallDateTime = null;
				runLog.CallFromLocationID = FindAADeliverLocationID();
				runLog.CollectDateTime = collectDateTime;
				runLog.CollectionLocationID = FindAAPickupLocationID();
				runLog.ControllerMemberID = controllerMemberId;
				runLog.CreateDate = DateTime.Now;
				runLog.CreatedByUserID = createdByUserId;
				runLog.DeliverDateTime = deliverDateTime;
				runLog.DeliverToLocationID = FindAADeliverLocationID();
				runLog.DutyDate = dutyDate;
				runLog.FinalDestinationLocationID = FindAADeliverLocationID();
				runLog.IsTransfer = 0;
				runLog.OriginLocationID = FindAAPickupLocationID();
				runLog.RiderMemberID = riderMemberId;
				runLog.Urgency = 1;
				runLog.VehicleTypeID = vehicleTypeId;
				runLog.Notes = notes;
				runLog.Boxes = prodsOut.Count;
				new RunLogDAL().CreateRunLog(runLog, prodsOut);
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
				new RunLogDAL().CreateRunLog(log, prodsIn);
			}
			return true;
		}

		public void DeleteRun(int runLogID)
		{
			new RunLogDAL().DeleteRunLog(runLogID);
		}

		public RunLog Get(int runLogID)
		{
			log.LogStart();
			SERVDataContract.DbLinq.RunLog metal = new RunLogDAL().Get(runLogID);
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

		public List<RunLog> ListRecent(int count)
		{
			log.LogStart();
			List<RunLog> ret = new List<RunLog>();
			List<SERVDataContract.DbLinq.RunLog> lret = new RunLogDAL().ListRecent(count);
			foreach(SERVDataContract.DbLinq.RunLog rl in lret)
			{
				ret.Add(new RunLog(rl));
			}
			return ret;
		}

		public List<RunLog> ListYesterdays()
		{
			List<RunLog> ret = new List<RunLog>();
			List<SERVDataContract.DbLinq.RunLog> lret = new RunLogDAL().ListYesterdays();
			foreach(SERVDataContract.DbLinq.RunLog rl in lret)
			{
				ret.Add(new RunLog(rl));
			}
			return ret;
		}

		public string GetYesterdaysSummary()
		{
			List<RunLog> y = ListYesterdays();
			if (y.Count == 0) { return null; }
			string twitterId = "@" + System.Configuration.ConfigurationManager.AppSettings["TwitterID"];
			int boxes = (from r in y
			             select r.Boxes).Sum();
			string[] opts = new string[]
			{"Yesterday {0} performed {1} runs delivering {2} boxes of urgently needed medical supplies.", 
				"Yesterday the amazing volunteers of {0} delivered {2} boxes to our hospitals.", 
				"Yesterday {0} delivered {2} boxes of urgently needed medical supplies.", 
				"Yesterday {0} did {1} runs delivering {2} boxes of medical supplies. Want to help?",
				"{0} performed {1} runs yesterday, delivering {2} boxes of medical supplies. Want to help?",
			};
			string format = opts[new Random(DateTime.Now.Millisecond).Next(0, opts.Length)];
			string ret = string.Format(format, twitterId, y.Count, boxes);
			return ret;
		}

		int FindAAPickupLocationID()
		{
			string find = new System.Configuration.AppSettingsReader().GetValue("AAPickupLocation", typeof(string)).ToString();
			return new LocationBLL().ListLocations(find)[0].LocationID;
		}

		int FindAADeliverLocationID()
		{
			string find = new System.Configuration.AppSettingsReader().GetValue("AADeliverLocation", typeof(string)).ToString();
			return new LocationBLL().ListLocations(find)[0].LocationID;
		}

		public string ExecuteSQL(string sql)
		{
			DataTable ret = null;
			try
			{
				ret = new RunLogDAL().ExecuteSQL(sql);
			}
			catch(Exception ex)
			{
				return "<table class='sqlResults'><tr><td>" + ex.Message + "</td></tr></table>";
			}
			StringBuilder b = new StringBuilder();
			b.AppendLine("<table class='sqlResults'>");
			string line = "<tr>";
			foreach (DataColumn c in ret.Columns)
			{
				line += string.Format("<td>{0}</td>", c.ColumnName);
			}
			line += "</tr>";
			b.AppendLine(line);
			foreach (DataRow r in ret.Rows)
			{
				line = "<tr>";
				foreach (object s in r.ItemArray)
				{
					line += string.Format("<td>{0}</td>", s.ToString());
				}
				line += "</tr>";
				b.AppendLine(line.Substring(0, line.Length - 1));
			}
			b.AppendLine("</table>");
			return b.ToString();
		}

		public List<Report> RunReports()
		{
			List<Report> reports = new List<Report>();

			Report rep = new Report();
			rep.Heading = "Recent Runs";
			rep.Description = "Real time information from the controller log.";
			rep.Anchor = "runLog";
			rep.Query = "select RunLogID as ID, date_format(DutyDate, '%Y-%m-%d') as 'Duty Date', coalesce(date_format(CallDateTime, '%Y-%m-%d %H:%i'), 'N/A') as 'Call Date & Time', cf.Location as 'Call From', cl.Location as 'From', " +
				"dl.Location as 'To', coalesce(date_format(rl.CollectDateTime, '%H:%i'), 'NOT ACCEPTED') as Collected, date_format(rl.DeliverDateTime, '%H:%i') as Delivered, " +
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
			rep.Query = "select CONCAT(m.FirstName, ' ', m.LastName) as Rider, date(m.JoinDate) as Joined, m.EmailAddress as Email, date(max(rr.CallDateTime)) as LastRun, date(JoinDate) as 'Join Date', count(*) as Runs " +
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
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', m.MobileNumber as Phone,  coalesce(date(LastDuty), '<span style=\"color:red\">NOTHING IN 2014</span>') as 'Last Run', date(JoinDate) as 'Join Date', " +
			            "concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link' " +
						"from Member m " +
			            "left join " +
			            "( " +
			            "select RiderMemberID, max(DutyDate) as LastDuty from RunLog group by RiderMemberID " +
			            ") rl on m.MemberID = rl.RiderMemberID " +
			            "where m.LeaveDate is null " +
			            "order by LastName;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Members on the Rota";
			rep.Description = "This report shows members who have committed to the rota.";
			rep.Anchor = "membersOnRota";
			rep.Query = "select distinct(concat(FirstName, ' ', LastName)) as Member, " +
				"concat('<a href=\"ViewMember.aspx?memberId=', m.MemberId,'\">view/edit</a>') as Link " +
				"from CalendarEntry ce join Member m on ce.MemberID = m.MemberID " +
				"where EntryDate > NOW() and m.LeaveDate is null order by m.LastName";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Members NOT on the Rota";
			rep.Description = "This report shows members who have not committed to the rota.";
			rep.Anchor = "membersNotOnRota";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', m.MobileNumber as Phone,  coalesce(date(LastDuty), '<span style=\"color:red\">NOTHING IN 2014</span>') as 'Last Run', date(JoinDate) as 'Join Date', " +
				"concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link' " +
				"from Member m " +
				"left join " +
				"( " +
				"select RiderMemberID, max(DutyDate) as LastDuty from RunLog group by RiderMemberID " +
				") rl on m.MemberID = rl.RiderMemberID " +
				"where m.MemberID not in (select distinct MemberID from CalendarEntry where EntryDate > NOW()) and m.LeaveDate is null " +
				"and MemberID not in (select MemberID from Member_Tag where TagID in(3,12)) " +
				"order by date(LastDuty);";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Emergency List - Last Run Date";
			rep.Description = "This report shows members who are tagged as on the emergency and when they last did a run.";
			rep.Anchor = "emergencyListWaste";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', m.MobileNumber as Phone,  coalesce(date(LastDuty), '<span style=\\\"color:red\\\">NOTHING IN 2014</span>') as 'Last Run', date(JoinDate) as 'Join Date', \nconcat('<a href=\\\"ViewMember.aspx?memberId=', MemberId,'\\\">view/edit</a>') as 'Link' \nfrom Member m \nleft join \n( \nselect RiderMemberID, max(DutyDate) as LastDuty from RunLog group by RiderMemberID \n) rl on m.MemberID = rl.RiderMemberID \nwhere m.LeaveDate is null \nand m.MemberID in\n(\nselect distinct m.MemberID from Tag t\njoin Member_Tag mt on t.TagID = mt.TagID \njoin Member m on mt.MemberID = m.MemberID\nwhere t.Tag = 'EmergencyList'\nand m.LeaveDate is null\nand m.MobileNumber is not null\n)\norder by LastDuty, JoinDate";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Top Ad-Hocs";
			rep.Description = "This report shows the number of shifts volunteers have put themselves forward for over and above rostered shifts (if applicable).";
			rep.Anchor = "topAdHocs";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', " +
				"count(*) as Count from CalendarEntry ce " +
				"join Member m on ce.MemberID = m.MemberID " +
				"where AdHoc =1 " +
				"group by concat(FirstName, ' ', LastName) " +
				"order by count(*) desc;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "AdQual Members";
			rep.Description = "This report shows members who are adqual and the relevant data. Part of the reason for this report is to aid in data cleansing.";
			rep.Anchor = "adQualMembers";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', AdQualType as Type, date(AdQualPassDate) as 'Pass Date', date(JoinDate) as 'Join Date'," +
			            "concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link' " +
			            "from Member  " +
			            "where (AdQualPassDate is not null or (AdQualType is not null and AdQualType != '')) and LeaveDate is null;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Non AdQual Members";
			rep.Description = "This report shows members who are NOT adqual and the relevant data. Part of the reason for this report is to aid in data cleansing.";
			rep.Anchor = "nonAdQualMembers";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', date(JoinDate) as 'Join Date', " +
			            "concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link' " +
			            "from Member  " +
			            "where AdQualPassDate is null and LeaveDate is null order by JoinDate;";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Training Dates";
			rep.Description = "This report shows the last GMP / GDP dates by member. Part of the reason for this report is to aid in data cleansing.";
			rep.Anchor = "lastGdpDates";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', coalesce(date(LastGDPGMPDate), '<span style=\"color:red\">NO GDP DATE SET</span>') as 'Last GDP/GMP Date', date(JoinDate) as 'Join Date', " +
			            "concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link'" +
			            "from Member where  LeaveDate is null " +
			            "order by LastGDPGMPDate";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "New Members";
			rep.Description = "This report shows members who are not tagged as active therefore need their induction / site ride / assesment.";
			rep.Anchor = "newMembers";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', date(JoinDate) as 'Join Date', " +
						"concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link' " +
						"from Member where MemberID not in (select MemberID from Member_Tag where TagID in(3,7,8,9,12,10)) and LeaveDate is NULL order by MemberID desc";
			reports.Add(rep);

			rep = new Report();
			rep.Heading = "Rider Assessment Dates";
			rep.Description = "This report shows the assessment date by member. Part of the reason for this report is to aid in data cleansing.";
			rep.Anchor = "riderAssessmentDates";
			rep.Query = "select concat(FirstName, ' ', LastName) as 'Member', MobileNumber as Phone, coalesce(date(RiderAssesmentPassDate), '<span style=\"color:red\">NOT ASSESSED</span>') as 'Assessment Date', date(JoinDate) as 'Join Date', " +
			            "concat('<a href=\"ViewMember.aspx?memberId=', MemberId,'\">view/edit</a>') as 'Link'" +
			            "from Member where  LeaveDate is null " +
			            "order by RiderAssesmentPassDate";
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
				r.Results = new RunLogDAL().RunReport(r);
			}

			return reports;
		}

		public string[] GetMemberUniqueRuns(int memberID)
		{
			List<string> ret = new List<string>();
			DataTable t = new RunLogDAL().GetMemberUniqueRuns(memberID);
			if (t != null)
			{
				foreach (DataRow r in t.Rows)
				{
					ret.Add(r["Location"] + "-" + r["Product"]);
				}
			}
			return ret.ToArray();
		}

		public DataTable Report_RunLog()
		{
			return new RunLogDAL().Report_RunLog();
		}

	}
}

