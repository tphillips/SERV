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
		bool CreateRawRecord(SERVDataContract.DbLinq.RawRunLog raw);
		void CreateRawRecords(List<SERVDataContract.DbLinq.RawRunLog> records);
		void TruncateRawRunLog();
		int CreateRunLog(SERVDataContract.DbLinq.RunLog log, List<int> prods);
		void DeleteRunLog(int runLogID);
		DataTable RunReport(SERVDataContract.Report report);
		DataTable Report_RunLog();
	}
}

