using System;
using System.Data;
using SERV.Utils.Data;
using MySql.Data.MySqlClient;

namespace DBHelperProvider
{

	public class MySQLServerDBHelper : IDBHelper , IDisposable
	{
		
		public void Prepare()
		{
			throw new NotImplementedException();
		}

		public DataSet ExecuteDataSet(string sql)
		{
			using (MySqlConnection c = Connect())
			{
				MySqlDataAdapter a = new MySqlDataAdapter(sql, c);
				DataSet t =new DataSet();
				a.Fill(t);
				return t;
			}
		}
	
		public IDbConnection GetConnection()
		{
			return Connect();
		}

		public DataSet ExecuteDataSetCommand(System.Data.Common.DbCommand com)
		{
			throw new NotImplementedException();
		}

		public DataTable ExecuteDataTableCommand(System.Data.Common.DbCommand com)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalar(string sql, System.Data.Common.DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalarCommand(System.Data.Common.DbCommand com)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalarCommand(System.Data.Common.DbCommand com, System.Data.Common.DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQL(string sql)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQL(string sql, System.Data.Common.DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQLCommand(System.Data.Common.DbCommand com)
		{
			throw new NotImplementedException();
		}

		public System.Data.Common.DbCommand GetStoredProcCommand(string spName)
		{
			throw new NotImplementedException();
		}

		public int ExecuteSQLCommand(System.Data.Common.DbCommand com, System.Data.Common.DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public System.Data.Common.DbCommand CreateDbCommand(string sql)
		{
			throw new NotImplementedException();
		}

		public void AddCommandParameter(System.Data.Common.DbCommand com, string name, DbType dbType, object value)
		{
			throw new NotImplementedException();
		}

		public string GetParameterValue(System.Data.Common.DbCommand com, string name)
		{
			throw new NotImplementedException();
		}

		public void AddOutputParameter(System.Data.Common.DbCommand com, string name, DbType dbType, int size)
		{
			throw new NotImplementedException();
		}

		public System.Data.Common.DbTransaction CreateTransaction()
		{
			throw new NotImplementedException();
		}

		public void CommitTransaction(System.Data.Common.DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		public void RollbackTransaction(System.Data.Common.DbTransaction trans)
		{
			throw new NotImplementedException();
		}

		private MySqlConnection Connect()
		{
			MySqlConnection ret = new MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["RawConnectionString"]);
			ret.Open();
			return ret;
		}

		public DataTable ExecuteDataTable(string sql)
		{
			using (MySqlConnection c = Connect())
			{
				MySqlDataAdapter a = new MySqlDataAdapter(sql, c);
				DataTable t =new DataTable();
				a.Fill(t);
				return t;
			}
		}

		public int ExecuteNonQuery(string sql)
		{
			using (MySqlConnection c = Connect())
			{
				MySqlCommand com = new MySqlCommand(sql, c);
				return com.ExecuteNonQuery();
			}
		}

		public int ExecuteNonQuery(System.Data.Common.DbCommand com)
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalar(string sql)
		{
			using (MySqlConnection c = Connect())
			{
				MySqlCommand com = new MySqlCommand(sql, c);
				return com.ExecuteScalar();
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

	}

}





