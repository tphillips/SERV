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
	public class BuddhaDAL
	{

		//static Logger log = new Logger();
		static SERVDataContract.DbLinq.SERVDB db;

		public BuddhaDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

