// Uncomment the below define if you want to build using mono.  
// DO NOT COMMIT THIS UNCOMMENTED
//#define MONO

using System;
using System.Data;
using System.Data.Common;

namespace DBHelperProvider
{

	public class ELDBHelper : ODHR.Utils.Data.IDBHelper 
	{

		private readonly Logger Log = new Logger();
		private readonly Microsoft.Practices.EnterpriseLibrary.Data.Database Db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase();
		
		public void Prepare()
		{
		}

		/// <summary>
		/// Exceute an sql statement (or multiple) and return a dataset
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public DataSet ExecuteDataSet(string sql)
		{
			try
			{
				Log.Debug("\tSQL: " + sql);
				DataSet ret = ODHR.Utils.Caching.CallStackCaching.GetFromCache<DataSet>(sql);
				if (ret != null) return ret;
				ret = Db.ExecuteDataSet(CommandType.Text, sql);
				ODHR.Utils.Caching.CallStackCaching.SaveToCache(sql, ret);
				return ret;
			}
			catch (Exception e)
			{
				Log.Fatal("\tExecuteDataSet - " + e.Message + " SQL:["+ (sql ?? "Null") + "]", e);
				throw;
			}
		}

		/// <summary>
		/// Exceute an sql command (or multiple) and return a dataset
		/// </summary>
		/// <param name="com"></param>
		/// <returns></returns>
		public DataSet ExecuteDataSetCommand(DbCommand com)
		{
			AppendEvaluatedCommandParametersAsComment(com);
			try
			{
				AppendEvaluatedCommandParametersAsComment(com);
				Log.Debug("\tSQL(Command): " + com.CommandText);
				DataSet ret = ODHR.Utils.Caching.CallStackCaching.GetFromCache<DataSet>(com.CommandText);
				if (ret != null) return ret;
				ret = Db.ExecuteDataSet(com);
				ODHR.Utils.Caching.CallStackCaching.SaveToCache(com.CommandText, ret);
				return ret;
			}
			catch (Exception e)
			{
				if (com != null)
					Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[" + (com.CommandText ?? "Null") + "]", e);
				else
					Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[DbCommand is NULL]", e);
				throw;
			}
		}

		/// <summary>
		/// Adds /*@ParamName=ParamValue@ParamName=ParamValue*/ to the SQL String so that is has a uniqie signature for caching purposes when parameters are used
		/// </summary>
		/// <param name="com"></param>
		/// <remarks>
		/// Note that the value part of each variable's value may not contain */ or */ otherwise it breaks the SQL comment,
		/// so, logic is added during the parameter Value parsing to remove any */ and replace with [*]/ or /[*].
		/// Tested with:
		/// select 1, 2
		/// /*@thisisavariable=this [*]/ is a value with a newline
		/// go
		/// /[*]
		/// ;
		/// character in it*/
		/// select 2,3
		/// </remarks>
		private void AppendEvaluatedCommandParametersAsComment(DbCommand com)
		{
			System.Text.StringBuilder b = new System.Text.StringBuilder();
			foreach (DbParameter p in com.Parameters)
			{
				b.Append(string.Format("{0}={1}", p.ParameterName, p.Value != null ? p.Value.ToString().Replace("*/", "[*]/").Replace("/*", "/[*]") : ""));
			}
			com.CommandText += string.Format("\r\n/*{0}*/", b.ToString());
		}

		/// <summary>
		/// Executes an SQL statement and returns a table of results, null if no results.
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public DataTable ExecuteDataTable(string sql)
		{
			DataSet ds = ExecuteDataSet(sql);
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					return ds.Tables[0];
				}
			}
			return null;
		}

		/// <summary>
		/// Executes an SQL command object and returns a table of results, null if no results.
		/// </summary>
		/// <param name="com"></param>
		/// <returns></returns>
		public DataTable ExecuteDataTableCommand(DbCommand com)
		{
			DataSet ds = ExecuteDataSetCommand(com);
			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows.Count > 0)
				{
					return ds.Tables[0];
				}
			}
			return null;
		}

        /// <summary>
        /// Execute an SQL command (statement) and return the number of rows affected
        /// </summary>
        public int ExecuteNonQuery(DbCommand com)
        {
			Log.Debug("\tSQL Command: " + com.CommandText);
            return Db.ExecuteNonQuery(com);
        }

		/// <summary>
		/// Execute an SQL statement and return the number of rows affected
		/// </summary>
		public int ExecuteNonQuery(string sql)
		{
			Log.Debug("\tSQL: " + sql);
			return Db.ExecuteNonQuery(CommandType.Text, sql);
		}

		/// <summary>
		/// Return the scalar value from an SQL query
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public object ExecuteScalar(string sql)
		{
			return ExecuteScalar(sql, null);
		}

		/// <summary>
		/// Return the scalar value from an SQL query
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		public object ExecuteScalar(string sql, DbTransaction trans)
		{
			Log.Debug("\tSQL: " + sql);
			try
			{
				if (trans != null)
				{
					return Db.ExecuteScalar(trans, CommandType.Text, sql);
				}
				else
				{
					object ret = ODHR.Utils.Caching.CallStackCaching.GetFromCache<object>(sql);
					if (ret != null) return ret;
					ret = Db.ExecuteScalar(CommandType.Text, sql);
					ODHR.Utils.Caching.CallStackCaching.SaveToCache(sql, ret);
					return ret;
				}
			}
			catch (Exception e)
			{
				Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[" + (sql ?? "Null") + "]", e);
				throw;
			}
		}

		/// <summary>
		/// Return the scalar value from an SQL command
		/// </summary>
		/// <param name="com"></param>
		/// <returns></returns>
		public object ExecuteScalarCommand(DbCommand com)
		{
			return ExecuteScalarCommand(com, null);
		}

		/// <summary>
		/// Return the scalar value from an SQL command
		/// </summary>
		/// <param name="com"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		public object ExecuteScalarCommand(DbCommand com, DbTransaction trans)
		{
			try
			{
				Log.Debug("\tSQL(Command): " + com.CommandText);
				if (trans != null)
				{
					return Db.ExecuteScalar(com, trans);
				}
				else
				{
					AppendEvaluatedCommandParametersAsComment(com);
					object ret = ODHR.Utils.Caching.CallStackCaching.GetFromCache<object>(com.CommandText);
					if (ret != null) return ret;
					ret = Db.ExecuteScalar(com);
					ODHR.Utils.Caching.CallStackCaching.SaveToCache(com.CommandText, ret);
					return ret;
				}
			}
			catch (Exception e)
			{
				if (com != null)
					Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[" + (com.CommandText ?? "Null") + "]", e);
				else
					Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[DbCommand is NULL]", e);

				throw;
			}
		}

		/// <summary>
		/// Execute a query and return nothing
		/// </summary>
		/// <param name="sql"></param>
		public int ExecuteSQL(string sql)
		{
			return ExecuteSQL(sql, null);
		}

		/// <summary>
		/// Execute a query and returns number of rows affected (can return -1 without -1 being an error)
		/// </summary>
		/// <param name="sql"></param>
		/// <param name="trans"></param>
		public int ExecuteSQL(string sql, DbTransaction trans)
		{
			Log.Debug("\tSQL: " + sql);
			try
			{
				int CountRowsAffected = -1;
				if (trans != null)
				{
					CountRowsAffected = Db.ExecuteNonQuery(trans, CommandType.Text, sql);
				}
				else
				{
					CountRowsAffected = Db.ExecuteNonQuery(CommandType.Text, sql);
				}
				return CountRowsAffected;
			}
			catch (Exception e)
			{
				Log.Fatal("ExecuteSQL - " + e.Message + " SQL:[" + (sql ?? "Null") + "]", e);
				throw;
			}
		}

		/// <summary>
		/// Executes an sql command and returns number of rows affected (can return -1 without -1 being an error)
		/// </summary>
		/// <param name="com"></param>
		public int ExecuteSQLCommand(DbCommand com)
		{
			return ExecuteSQLCommand(com, null);
		}

		/// <summary>
		/// get sp command
		/// </summary>
		/// <param name="com"></param>
		public DbCommand GetStoredProcCommand(string spName)
		{
			return Db.GetStoredProcCommand(spName);
		}

		/// <summary>
		/// Executes an sql command and returns nothing
		/// </summary>
		/// <param name="com"></param>
		/// <param name="trans"></param>
		public int ExecuteSQLCommand(DbCommand com, DbTransaction trans)
		{
			try
			{
				Log.Debug("\tSQL(Command): " + com.CommandText);
				int CountRowsAffected = -1;
				if (trans != null)
				{
					CountRowsAffected = Db.ExecuteNonQuery(com, trans);
				}
				else 
				{
					CountRowsAffected = Db.ExecuteNonQuery(com);
				}
				return CountRowsAffected;
			}
			catch (Exception e)
			{
				if (com != null)
					Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[" + (com.CommandText ?? "Null") + "]", e);
				else
					Log.Fatal("ExecuteScalar - " + e.Message + " SQL:[DbCommand is NULL]", e);
				throw;
			}
		}

		/// <summary>
		/// Creates a DBCommand for an SQL query
		/// </summary>
		/// <param name="sql"></param>
		public DbCommand CreateDbCommand(string sql)
		{
			return Db.GetSqlStringCommand(sql);
		}

		/// <summary>
		/// Adds a parameter and value to a DBCommand
		/// </summary>
		/// <param name="com"></param>
		/// <param name="name"></param>
		/// <param name="dbType"></param>
		/// <param name="value"></param>
		public void AddCommandParameter(DbCommand com, string name, DbType dbType, object value)
		{
			Db.AddInParameter(com, name, dbType, value);
		}

		public string GetParameterValue(DbCommand com, string name)
		{
			string returnedValue;
			returnedValue = Db.GetParameterValue(com, name).ToString();
			return returnedValue;
		}

		/// <summary>
		/// Adds an out parameter and value to a DBCommand
		/// </summary>
		/// <param name="com"></param>
		/// <param name="name"></param>
		/// <param name="dbType"></param>
		/// <param name="value"></param>
		public void AddOutputParameter(DbCommand com, string name, DbType dbType, Int32 size)
		{
			Db.AddOutParameter(com, name, dbType, size);
		}
		/// <summary>
		/// Opens a connection and transaction.
		/// Use CommitTransaction or Rollback Transaction from this class to manage the returned transaction object.
		/// Either method will close the connection created.
		/// The transaction is created using the DEFAULT isolation level.
		/// </summary>
		/// <returns></returns>
		public DbTransaction CreateTransaction()
		{
			DbConnection c = Db.CreateConnection();
			c.Open();
			return c.BeginTransaction();
		}

		/// <summary>
		/// Commits the transaction, and closes its connection
		/// </summary>
		/// <param name="trans"></param>
		public void CommitTransaction(DbTransaction trans)
		{
			DbConnection c = trans.Connection;
			trans.Commit();
			c.Close();
		}

		/// <summary>
		/// Rollsback the transaction, and closes its connection
		/// </summary>
		/// <param name="trans"></param>
		public void RollbackTransaction(DbTransaction trans)
		{
			DbConnection c = trans.Connection;
			trans.Rollback();
			c.Close();
		}

	}

}
