using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;
using System.Data;

namespace SERVIBLL
{
	public interface IRunLogBLL
	{
		RunLog Get(int runLogID);
		List<RunLog> ListQueuedOrders();
		List<RunLog> ListQueuedOrdersForMember(int memberId);
		bool MarkOrderAsAccepted(int memberId, int runLogId);
		List<RunLog> ListRecent(int count);
		List<RunLog> ListYesterdays();
		bool CreateRunLog(DateTime callDateTime, int callFromLocationId, DateTime collectDateTime, int collectionLocationId, 
		                int controllerMemberId, int createdByUserId, DateTime deliverDateTime, int deliverToLocationId, DateTime dutyDate, 
						int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, 
			string productIdCsv, DateTime? homeSafeDateTime, string notes, string callerNumber, string callerExt);
		bool CreateAARunLog(DateTime dutyDate, DateTime collectDateTime, int controllerMemberId, int userID, 
			DateTime deliverDateTime, DateTime returnDateTime, int riderMemberId, int vehicleTypeId, 
			string boxesOutCsv, string boxesInCsv, string notes);
		List<Report> RunReports();
		void DeleteRun(int runLogID);
		DataTable Report_RunLog();
		string[] GetMemberUniqueRuns(int memberID);
		string GetYesterdaysSummary();
		string ExecuteSQL(string sql);
	}
}

