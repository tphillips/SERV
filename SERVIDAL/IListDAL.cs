using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface IListDAL : IDisposable
	{
		List<VehicleType> ListVehicleTypes();
		List<SERVDBGROUp> ListGroups();
	}
}

