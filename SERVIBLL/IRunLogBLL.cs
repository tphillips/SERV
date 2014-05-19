using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;
using System.Data;

namespace SERVIBLL
{
	public interface IRunLogBLL
	{
		[Obsolete]
		bool ImportRawRunLog();
		RunLog Get(int runLogID);
		bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
		                int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
						int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, 
			string productIdCsv, DateTime? homeSafeDateTime, string notes);
		bool CreateAARunLog(DateTime dutyDate, DateTime collectDateTime, int controllerMemberId, int userID, 
			DateTime deliverDateTime, DateTime returnDateTime, int riderMemberId, int vehicleTypeId, 
			string boxesOutCsv, string boxesInCsv, string notes);
		List<Report> RunReports();
		void DeleteRun(int runLogID);
		DataTable Report_RunLog();
	}
}

