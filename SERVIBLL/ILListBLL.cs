using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface ILListBLL
	{
		List<Location> ListLocations();
		List<VehicleType> ListVehicleTypes();
	}
}

