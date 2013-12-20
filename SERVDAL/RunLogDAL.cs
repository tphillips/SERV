using System;
using System.Collections.Generic;
using System.Text;
using SERVIDAL;
using SERVDataContract.DbLinq;
using System.Data;
using System.Data.Common;
using SERV.Utils.Data;
using System.Linq;

namespace SERVDAL
{
	public class RunLogDAL : IRunLogDAL
	{
		static SERVDataContract.DbLinq.SERVDB db;

		public RunLogDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public bool CreateRawRecord(RawRunLog raw)
		{
			db.RawRunLog.InsertOnSubmit(raw);
			db.SubmitChanges();
			return true;
		}

		public void CreateRawRecords(List<RawRunLog> records)
		{
			foreach (RawRunLog r in records)
			{
				db.RawRunLog.InsertOnSubmit(r);
			}
			db.SubmitChanges();
		}

		public int CreateRunLog(RunLog log, List<int> prods)
		{
			db.RunLog.InsertOnSubmit(log);
			db.SubmitChanges();
			LinkRunLogToProducts(log, prods);
			return log.RunLogID;
		}

		static void LinkRunLogToProducts(RunLog log, List<int> prods)
		{
			Dictionary<int, int> prodD = new Dictionary<int, int>();
			foreach (int p in prods)
			{
				if (prodD.ContainsKey(p))
				{
					prodD[p] = prodD[p] + 1;
				}
				else
				{
					prodD.Add(p, 1);
				}
			}
			foreach (int p in prodD.Keys)
			{
				RunLogProduct rlp = new RunLogProduct();
				rlp.ProductID = p;
				rlp.RunLogID = log.RunLogID;
				rlp.Quantity = prodD[p];
				log.Description += string.Format("{0} x {1} ", prodD[p], (from prod in db.Product
				                                                          where prod.ProductID == p
				                                                          select prod).FirstOrDefault().Product1);
				db.RunLogProduct.InsertOnSubmit(rlp);
			}
			db.SubmitChanges();
		}

		public void TruncateRawRunLog()
		{
			db.ExecuteCommand("truncate table RawRunLog");
		}

		public DataTable Report_RecentRunLog()
		{
			string sql = 
				"SELECT " +
					"date(CallDate) as Date, " +
					"TIME(rpad(replace(CallTime, '.',':'), 5, '0')) as Time, " +
					"CONCAT(m.FirstName, ' ', m.LastName) as Rider, " +
					"Consignment, " +
					"CollectFrom as Origin, " +
					"Destination, " +
					"CONCAT(con.FirstName, ' ', con.LastName) as Controller " +
				"FROM RawRunLog rr " +
					"LEFT JOIN Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) " +
					"LEFT JOIN Member con on rr.Controller = (CONCAT(con.LastName, ' ', con.FirstName)) " +
				"order by CallDate desc, RawRunLogID desc " +
				"LIMIT 100;";
			return DBHelperFactory.DBHelper().ExecuteDataTable(sql);
		}

		public DataTable Report_Top10Riders()
		{
			string sql = 
				"select Name from " +
				"(select CONCAT(m.FirstName, ' ', m.LastName) Name, count(*) Runs " +
				"from RawRunLog rr " +
				"LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName)) " +
				"where m.FirstName is not null " +
				"group by Name " +
				"order by Runs desc " +
				"LIMIT 10) sub " +
				"order by Name;";
			return DBHelperFactory.DBHelper().ExecuteDataTable(sql);
		}

		public DataTable Report_RunButNoLogin()
		{
			string sql = "select CONCAT(m.FirstName, ' ', m.LastName) as Rider, date(m.JoinDate) as Joined, m.EmailAddress as Email, date(max(rr.CallDate)) as LastRun, count(*) as Runs " +
			             "from RawRunLog rr  " +
			             "LEFT join Member m on rr.Rider = (CONCAT(m.LastName, ' ', m.FirstName))  " +
			             "where m.MemberID not in " +
			             "(select m.MemberID from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate is not null) " +
			             "and rr.CallDate > '2013-05-01' " +
			             "and m.LeaveDate is null " +
			             "group by m.MemberID " +
			             "order by max(rr.CallDate) desc;";
			return DBHelperFactory.DBHelper().ExecuteDataTable(sql);
		}

		public DataTable Report_AverageCallsPerDay()
		{
			string sql = "SELECT dayname(case when Hour(CallTime) > 17 then CallDate else AddDate(CallDate, -1) end) as ShiftDay " +
			             ", round(count(*) / 34.28) as AverageCalls " +
			             //", ceil((count(*) / @weeks) * @riderfactor) as RidersRequired " +
			             "FROM RawRunLog " +
			             "WHERE CallDate > AddDate(CURRENT_DATE, -240) " +
			             "AND(Consignment like '%blood%' " +
			             "or Consignment like '%plate%' " +
			             "or Consignment like '%plas%' " +
			             "or Consignment like '%ffp%' " +
			             "or Consignment like '%sample%' " +
			             "or Consignment like '%drugs%' " +
			             "or Consignment like '%cd%' " +
			             "or Consignment like '%data%' " +
			             "or Consignment like '%disk%' " +
			             "or Consignment like '%disc%' " +
			             "or Consignment like '%package%') " +
			             "GROUP BY dayname(case when Hour(CallTime) > 17 then CallDate else AddDate(CallDate, -1) end) " +
			             "ORDER BY dayofweek(case when Hour(CallTime) > 17 then CallDate else AddDate(CallDate, -1) end);";
			return DBHelperFactory.DBHelper().ExecuteDataTable(sql);
		}

		public DataTable Report_CallsPerHourHeatMap()
		{
			string sql = "select dayname(case when Hour(CallTime) > 17 then CallDate else AddDate(CallDate, -1) end) as Day, " +
						"Hour(CallTime) as Hour, count(*) as Calls " +
						"from RawRunLog " +
						"WHERE CallDate > AddDate(CURRENT_DATE, -240) " +
						"AND Hour(CallTime) >= 0 AND Hour(CallTime) <= 23 " +
						"AND(Consignment like '%blood%' " +
						"or Consignment like '%plate%' " +
						"or Consignment like '%plas%' " +
						"or Consignment like '%ffp%' " +
						"or Consignment like '%sample%' " +
						"or Consignment like '%drugs%' " +
						"or Consignment like '%cd%' " +
						"or Consignment like '%data%' " +
						"or Consignment like '%disk%' " +
						"or Consignment like '%disc%' " +
						"or Consignment like '%package%') " +
			             "group by dayname(case when Hour(CallTime) > 17 then CallDate else AddDate(CallDate, -1) end), Hour(CallTime) " +
			             "ORDER BY dayofweek(case when Hour(CallTime) > 17 then CallDate else AddDate(CallDate, -1) end), Hour(CallTime)";
			return DBHelperFactory.DBHelper().ExecuteDataTable(sql);
		}

		public DataTable Report_TodaysUsers()
		{
			string sql = "select CONCAT(m.FirstName, ' ', m.LastName) as Member " +
				"from User u join Member m on m.MemberID = u.MemberID where u.lastLoginDate > CURRENT_DATE() " +
				"order by lastLoginDate desc;";
			return DBHelperFactory.DBHelper().ExecuteDataTable(sql);
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

