using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IListBLL
	{
		List<VehicleType> ListVehicleTypes();
		List<Group> ListGroups();
	}
}

