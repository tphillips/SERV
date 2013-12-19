using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface ILocationBLL
	{
		List<Location> ListLocations(string search);
		Location Get(int locationId);
		int Save(Location location, SERVDataContract.User user);
		int Create(Location m);
	}
}

