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
	public class LocationDAL : ILocationDAL
	{
		//static Logger log = new Logger();
		static SERVDataContract.DbLinq.SERVDB db;

		public LocationDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public List<Location> ListLocations()
		{
			return (from l in db.Location select l).ToList();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}

