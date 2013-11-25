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

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

