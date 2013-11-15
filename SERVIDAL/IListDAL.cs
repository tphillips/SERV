using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface IListDAL : IDisposable
	{
		List<Location> ListLocations();
		List<VehicleType> ListVehicleTypes();
	}
}

