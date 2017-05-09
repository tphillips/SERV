using System.Collections.Generic;
using SERVDataContract;
using SERVDAL;

namespace SERVBLL
{
	public class ListBLL
	{

		public ListBLL()
		{
		}

		public List<VehicleType> ListVehicleTypes()
		{
			List<VehicleType> ret = new List<VehicleType>();
			List<SERVDataContract.DbLinq.VehicleType> locs = new ListDAL().ListVehicleTypes();
			foreach (SERVDataContract.DbLinq.VehicleType l in locs)
			{
				ret.Add(new VehicleType(l));
			}
			return ret;
		}

		public List<Group> ListGroups()
		{
			List<Group> ret = new List<Group>();
			List<SERVDataContract.DbLinq.SERVDBGROUp> groups = new ListDAL().ListGroups();
			foreach (SERVDataContract.DbLinq.SERVDBGROUp l in groups)
			{
				ret.Add(new Group(l));
			}
			return ret;
		}

	}
}

