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

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

