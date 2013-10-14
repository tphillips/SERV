using System;
using System.Data.Common;
using System.Data;

namespace SERV.Utils.Data
{
	// Seperate the implementation of M$ practices "Database" from the datahelper
	public interface IDBHelper
	{

		void Prepare();

		DataSet ExecuteDataSet(string sql);
		
		DataSet ExecuteDataSetCommand(DbCommand com);
		
		DataTable ExecuteDataTable(string sql);
		
		DataTable ExecuteDataTableCommand(DbCommand com);
		
		int ExecuteNonQuery(DbCommand com);
		
		int ExecuteNonQuery(string sql);
		
		object ExecuteScalar(string sql);
		
		object ExecuteScalar(string sql, DbTransaction trans);
		
		object ExecuteScalarCommand(DbCommand com);
		
		object ExecuteScalarCommand(DbCommand com, DbTransaction trans);

		int ExecuteSQL(string sql);
		
		int ExecuteSQL(string sql, DbTransaction trans);
		
		int ExecuteSQLCommand(DbCommand com);
		
		DbCommand GetStoredProcCommand(string spName);
		
		int ExecuteSQLCommand(DbCommand com, DbTransaction trans);
		
		DbCommand CreateDbCommand(string sql);
		
		void AddCommandParameter(DbCommand com, string name, DbType dbType, object value);
		
		string GetParameterValue(DbCommand com, string name);
		
		void AddOutputParameter(DbCommand com, string name, DbType dbType, Int32 size);
		
		DbTransaction CreateTransaction();
		
		void CommitTransaction(DbTransaction trans);
		
		void RollbackTransaction(DbTransaction trans);
		
	}
}

