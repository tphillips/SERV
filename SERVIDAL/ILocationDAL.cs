using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface ILocationDAL : IDisposable
	{
		List<Location> ListLocations();
	}
}

