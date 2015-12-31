using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;
using System.Data;

namespace SERVIDAL
{
	public interface IRunLogDAL : IDisposable
	{
		SERVDataContract.DbLinq.RunLog Get(int runLogID);
		void SetAcceptedDateTime(int runLogId);
		List<RunLog> ListQueuedOrders();
		List<RunLog> ListQueuedOrdersForMember(int memberID);
		List<RunLog> ListRecent(int recent);
		List<RunLog> ListYesterdays();
		bool CreateRawRecord(SERVDataContract.DbLinq.RawRunLog raw);
		void CreateRawRecords(List<SERVDataContract.DbLinq.RawRunLog> records);
		void TruncateRawRunLog();
		int CreateRunLog(SERVDataContract.DbLinq.RunLog log, List<int> prods);
		void DeleteRunLog(int runLogID);
		DataTable RunReport(SERVDataContract.Report report);
		DataTable Report_RunLog();
		DataTable GetMemberUniqueRuns(int memberID);
		DataTable ExecuteSQL(string sql);
	}
}

