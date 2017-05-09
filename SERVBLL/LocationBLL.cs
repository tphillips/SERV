using System.Collections.Generic;
using SERVDataContract;
using SERV.Utils;
using SERVDAL;

namespace SERVBLL
{
	public class LocationBLL
	{
		public LocationBLL()
		{
		}

		public List<Location> ListLocations(string search)
		{
			List<Location> ret = new List<Location>();
			List<SERVDataContract.DbLinq.Location> locs = new LocationDAL().ListLocations(search);
			foreach (SERVDataContract.DbLinq.Location l in locs)
			{
				ret.Add(new Location(l));
			}
			return ret;
		}

		public Location Get(int locationId)
		{
			SERVDataContract.DbLinq.Location lret = new LocationDAL().Get(locationId);
			Location ret = new Location(lret);
			return ret;
		}

		public int Save(Location location, User user)
		{
			using (LocationDAL dal = new LocationDAL())
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
			return new LocationDAL().Create(l);
		}

	}
}

