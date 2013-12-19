using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface ILocationDAL : IDisposable
	{
		List<Location> ListLocations(string search);
		SERVDataContract.DbLinq.Location Get(int locationId);
		int Update(SERVDataContract.DbLinq.Location l);
		int Create(SERVDataContract.DbLinq.Location l);
	}
}

