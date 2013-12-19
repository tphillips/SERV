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

		public List<Location> ListLocations(string search)
		{
			List<Location> ret = new List<Location>();
			List<SERVDataContract.DbLinq.Location> locs = SERVDALFactory.Factory.LocationDAL().ListLocations(search);
			foreach (SERVDataContract.DbLinq.Location l in locs)
			{
				ret.Add(new Location(l));
			}
			return ret;
		}

		public Location Get(int locationId)
		{
			SERVDataContract.DbLinq.Location lret = SERVDALFactory.Factory.LocationDAL().Get(locationId);
			Location ret = new Location(lret);
			return ret;
		}

		public int Save(Location location, User user)
		{
			using (SERVIDAL.ILocationDAL dal = SERVDALFactory.Factory.LocationDAL())
			{
				SERVDataContract.DbLinq.Location l = dal.Get(location.LocationID);
				UpdatePolicyAttribute.MapPropertiesWithUpdatePolicy(location, l, user, false);
				return dal.Update(l);
			}
		}

		public int Create(Location location)
		{
			SERVDataContract.DbLinq.Location l = new SERVDataContract.DbLinq.Location();
			PropertyMapper.MapProperties(location, l);
			return SERVDALFactory.Factory.LocationDAL().Create(l);
		}

	}
}

