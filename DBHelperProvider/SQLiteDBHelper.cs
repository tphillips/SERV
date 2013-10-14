using System;
using SERV.Utils.Data;
using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;

namespace DBHelperProvider
{
	public class SQLiteDBHelper : IDBHelper
	{

		SqliteConnection con;

		public SQLiteDBHelper()
		{
			string conStr = System.Configuration.ConfigurationManager.AppSettings ["ConnectionString"];
			con = new SqliteConnection(conStr);
			con.Open();
		}

		#region IDBHelper implementation

		public void Prepare()
		{
			throw new NotImplementedException();
		}

		public DataSet ExecuteDataSet(string sql)
		{
			SqliteDataAdapter a = new SqliteDataAdapter(sql, con);
			DataSet ret = new DataSet();
			a.Fill(ret);
			return ret;
		}

		public DataSet ExecuteDataSetCommand(DbCommand com)
		{
			SqliteDataAdapter a = new SqliteDataAdapter((SqliteCommand)com);
			DataSet ret = new DataSet();
			a.Fill(ret);
			return ret;
		}

		public DataTable ExecuteDataTable(string sql)
		{
			throw new NotImplementedException();
		}

		public DataTable ExecuteDataTableCommand(DbCommand com)
		{
			return ExecuteDataSetCommand(com).Tables[0];
		}

		public int ExecuteNonQuery(DbCommand com)
		{
			throw new NotImplementedException();
		}

		public int ExecuteNonQuery(string sql)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalar(string sql)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalar(string sql, DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalarCommand(DbCommand com)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalarCommand(DbCommand com, DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQL(string sql)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQL(string sql, DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQLCommand(DbCommand com)
		{
			throw new NotImplementedException();
		}

		public DbCommand GetStoredProcCommand(string spName)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQLCommand(DbCommand com, DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public DbCommand CreateDbCommand(string sql)
		{
			return new SqliteCommand(sql, con);
		}

		public void AddCommandParameter(DbCommand com, string name, DbType dbType, object value)
		{
			SqliteParameter p = new SqliteParameter(name, (SqlDbType)dbType);
			p.DbType = dbType;
			p.Value = value;
			((SqliteCommand)com).Parameters.Add(p);
		}

		public string GetParameterValue(DbCommand com, string name)
		{
			throw new NotImplementedException();
		}

		public void AddOutputParameter(DbCommand com, string name, DbType dbType, int size)
		{
			throw new NotImplementedException();
		}

		public DbTransaction CreateTransaction()
		{
			throw new NotImplementedException();
		}

		public void CommitTransaction(DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public void RollbackTransaction(DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

