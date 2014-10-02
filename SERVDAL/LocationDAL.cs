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
		static Logger log = new Logger();
		private SERVDataContract.DbLinq.SERVDB db;

		public LocationDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public List<Location> ListLocations(string search)
		{
			return (from l in db.Location where l.Location1.Contains(search) orderby l.Location1 select l).ToList();
		}

		public Location Get(int locationId)
		{
			SERVDataContract.DbLinq.Location loc = (from l in db.Location where l.LocationID == locationId select l).FirstOrDefault();
			return loc;
		}

		public int Update(Location l)
		{
			log.LogStart();
			db.Log = Console.Out;
			db.SubmitChanges();
			db.Log = null;
			return l.LocationID;
		}

		public int Create(Location l)
		{
			log.LogStart();
			db.Location.InsertOnSubmit(l);
			db.SubmitChanges();
			return l.LocationID;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}

