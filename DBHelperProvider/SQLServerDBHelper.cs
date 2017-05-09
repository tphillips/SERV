using System;
using System.Data;
using SERV.Utils.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DBHelperProvider
{

	/// <summary>
	/// TP: I would not recommend using this in production just yet. (DEFO not)
	/// Heavily modded to not connect unless we need to.  If we have what we need in the cache, dont bother.
	/// </summary>
	public class SQLServerDBHelper : IDBHelper , IDisposable
	{



		SqlConnection con = new SqlConnection();

		public SQLServerDBHelper()
		{
			con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
			//Connect();
		}

		public IDbConnection GetConnection()
		{
			Connect();
			return con;
		}

		void Connect()
		{
			if (con.State != ConnectionState.Open)
			{
				int tries = 3;
				bool ok = false;
				Exception lastException = null;
				while (!ok && tries > 0)
				{
					try
					{
						con.Open();
						ok = true;
					}
					catch (Exception e)
					{
						lastException = e;
					}
				}
				if (!ok)
				{
					new Logger().Error(string.Format("SQLServerDBHelper cannot connect using '{0}' due to {1}", System.Configuration.ConfigurationManager.AppSettings["ConnectionString"], lastException.Message), lastException);
					throw new Exception("SQLServerDBHelper cannot connect");
				}
			}
		}

		#region IDBHelper implementation

		public void Prepare()
		{
			if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
			{
				con.Open();
			}
		}

		public System.Data.DataSet ExecuteDataSet(string sql)
		{
			DataSet ret = SERV.Utils.Caching.CallStackCaching.GetFromCache<DataSet>(sql);
			if (ret != null) return ret;
			Connect();
			SqlDataAdapter a = new SqlDataAdapter(sql, con);
			ret = new DataSet();
			a.Fill(ret);
			con.Close();
			SERV.Utils.Caching.CallStackCaching.SaveToCache(sql, ret);
			return ret;
		}

		public System.Data.DataSet ExecuteDataSetCommand(System.Data.Common.DbCommand com)
		{
			AppendEvaluatedCommandParametersAsComment(com);
			DataSet ret = SERV.Utils.Caching.CallStackCaching.GetFromCache<DataSet>(com.CommandText);
			if (ret != null) return ret;
			Connect();
			SqlDataAdapter a = new SqlDataAdapter((SqlCommand) com);
			ret = new DataSet();
			a.Fill(ret);
			con.Close();
			SERV.Utils.Caching.CallStackCaching.SaveToCache(com.CommandText, ret);
			return ret;
		}

		private void AppendEvaluatedCommandParametersAsComment(DbCommand com)
		{
			System.Text.StringBuilder b = new System.Text.StringBuilder();
			foreach (DbParameter p in com.Parameters)
			{
				b.Append(string.Format("{0}={1}", p.ParameterName, p.Value != null ? p.Value.ToString() : ""));
			}
			com.CommandText += string.Format("\r\n--{0}", b.ToString());
		}

		public System.Data.DataTable ExecuteDataTable(string sql)
		{
			DataTable t = ExecuteDataSet(sql).Tables[0];
			if (t.Rows.Count == 0)
				return null;
			return t;
		}

		public System.Data.DataTable ExecuteDataTableCommand(System.Data.Common.DbCommand com)
		{
			DataTable t = ExecuteDataSetCommand(com).Tables[0];
			if (t.Rows.Count == 0)
				return null;
			return t;
		}

		public int ExecuteNonQuery(System.Data.Common.DbCommand com)
		{
			Connect();
			return com.ExecuteNonQuery();
		}

		public int ExecuteNonQuery(string sql)
		{
			Connect();
			return new SqlCommand(sql, con).ExecuteNonQuery();
		}

		public object ExecuteScalar(string sql)
		{
			object ret = SERV.Utils.Caching.CallStackCaching.GetFromCache<object>(sql);
			if (ret != null) return ret;
			Connect();
			ret = new SqlCommand(sql, con).ExecuteScalar();
			SERV.Utils.Caching.CallStackCaching.SaveToCache(sql, ret);
			return ret;
		}

		public object ExecuteScalar(string sql, System.Data.Common.DbTransaction trans)
		{
			Connect();
			return new SqlCommand(sql, con, (SqlTransaction)trans).ExecuteScalar();
		}

		public object ExecuteScalarCommand(System.Data.Common.DbCommand com)
		{
			Connect();
			return ((SqlCommand)com).ExecuteScalar();
		}

		public object ExecuteScalarCommand(System.Data.Common.DbCommand com, System.Data.Common.DbTransaction trans)
		{
			Connect();
			//com.Transaction = trans;
			return com.ExecuteScalar();
		}

		public int ExecuteSQL(string sql)
		{
			Connect();
			return new SqlCommand(sql, con).ExecuteNonQuery();
		}

		public int ExecuteSQL(string sql, System.Data.Common.DbTransaction trans)
		{
			Connect();
			return new SqlCommand(sql, con).ExecuteNonQuery();
		}

		public int ExecuteSQLCommand(System.Data.Common.DbCommand com)
		{
			Connect();
			return com.ExecuteNonQuery();
		}

		public System.Data.Common.DbCommand GetStoredProcCommand(string spName)
		{
			Connect();
			SqlCommand c = new SqlCommand(spName, con);
			c.CommandType = CommandType.StoredProcedure;
			return c;
		}

		public int ExecuteSQLCommand(System.Data.Common.DbCommand com, System.Data.Common.DbTransaction trans)
		{
			Connect();
			//com.Transaction = trans;
			return com.ExecuteNonQuery();
		}

		public System.Data.Common.DbCommand CreateDbCommand(string sql)
		{
			Connect();
			return new SqlCommand(sql, con);
		}

		public void AddCommandParameter(System.Data.Common.DbCommand com, string name, System.Data.DbType dbType, object value)
		{
			SqlParameter p = new SqlParameter(name, (SqlDbType)dbType);
			p.DbType = dbType;
			p.Value = value;
			p.Size = -1;
			((SqlCommand)com).Parameters.Add(p);
		}

		public string GetParameterValue(System.Data.Common.DbCommand com, string name)
		{
			return ((SqlCommand) com).Parameters[name].Value.ToString();
		}

		public void AddOutputParameter(System.Data.Common.DbCommand com, string name, System.Data.DbType dbType, int size)
		{
			SqlParameter p = new SqlParameter(name, (SqlDbType)dbType, size);
			p.Direction = ParameterDirection.Output;
			((SqlCommand)com).Parameters.Add(p);
		}

		public System.Data.Common.DbTransaction CreateTransaction()
		{
			Connect();
			return con.BeginTransaction();
		}

		public void CommitTransaction(System.Data.Common.DbTransaction trans)
		{
			trans.Commit();
		}

		public void RollbackTransaction(System.Data.Common.DbTransaction trans)
		{
			trans.Rollback();
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			try { con.Close(); } catch {}
		}

		#endregion
	}
}

