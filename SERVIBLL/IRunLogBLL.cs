using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;
using System.Data;

namespace SERVIBLL
{
	public interface IRunLogBLL
	{
		bool ImportRawRunLog();

		RunLog Get(int runLogID);

		bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
		                int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
						int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, 
			string productIdCsv, DateTime? homeSafeDateTime, string notes);

		bool CreateAARunLog(DateTime dutyDate, DateTime collectDateTime, int controllerMemberId, int userID, 
			DateTime deliverDateTime, DateTime returnDateTime, int riderMemberId, int vehicleTypeId, 
			string boxesOutCsv, string boxesInCsv, string notes);

		DataTable Report_RecentRunLog();
		DataTable Report_Top10Riders();
		DataTable Report_Top102013Riders();
		DataTable Report_RunButNoLogin();
		DataTable Report_AverageCallsPerDay();
		DataTable Report_CallsPerHourHeatMap();
		DataTable Report_TodaysUsers();
		DataTable Report_RunLog();
	}
}

