using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	public class ListBLL : IListBLL
	{

		public ListBLL()
		{
		}

		public List<VehicleType> ListVehicleTypes()
		{
			List<VehicleType> ret = new List<VehicleType>();
			List<SERVDataContract.DbLinq.VehicleType> locs = SERVDALFactory.Factory.ListDAL().ListVehicleTypes();
			foreach (SERVDataContract.DbLinq.VehicleType l in locs)
			{
				ret.Add(new VehicleType(l));
			}
			return ret;
		}

	}
}

