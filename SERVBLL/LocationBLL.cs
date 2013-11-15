using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	public class LocationBLL : ILocationBLL
	{

		public LocationBLL()
		{
		}

		public List<Location> ListLocations()
		{
			List<Location> ret = new List<Location>();
			List<SERVDataContract.DbLinq.Location> locs = SERVDALFactory.Factory.LocationDAL().ListLocations();
			foreach (SERVDataContract.DbLinq.Location l in locs)
			{
				ret.Add(new Location(l));
			}
			return ret;
		}

	}
}

