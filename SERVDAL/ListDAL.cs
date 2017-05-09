using System.Collections.Generic;
using SERVDataContract.DbLinq;
using System.Linq;

namespace SERVDAL
{
	public class ListDAL
	{

		//static Logger log = new Logger();
		private SERVDataContract.DbLinq.SERVDB db;

		public ListDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public List<VehicleType> ListVehicleTypes()
		{
			return (from l in db.VehicleType where l.Enabled == 1 select l).ToList();
		}

		public List<SERVDBGROUp> ListGroups()
		{
			return (from g in db.SERVDBGROUp select g).ToList();
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

