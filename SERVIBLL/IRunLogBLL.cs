using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IRunLogBLL
	{
		bool ImportRawRunLog();
		bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
		                int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
						int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, string productIdCsv);
	}
}

