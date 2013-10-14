using System;
using System.Runtime;
using System.Reflection;

namespace SERV.Utils.Data
{
	
	public class DBHelperFactory
	{

		private const string ASSEMBLY_NAME = "DBHelperProvider";
		private static IDBHelper helper;

		public static IDBHelper DBHelper()
		{
			string HelperClassName = "MySQLServerDBHelper";
			bool Singleton = false;
			//try { Singleton = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["DBHelperSingleton"]); } catch{}
			//try { HelperClassName = System.Configuration.ConfigurationManager.AppSettings["DBHelperClassName"]; } catch{}
			if (Singleton)
			{
				if (helper == null)
				{
					helper = GetNewInstance(HelperClassName);
				}
				else
				{
					helper.Prepare();
				}
				return helper;
			}
			else
			{
				return GetNewInstance(HelperClassName);
			}
		}

		private static IDBHelper GetNewInstance(string HelperClassName)
		{
			IDBHelper ret = null;
			Assembly a = Assembly.Load(ASSEMBLY_NAME);
			ret = (IDBHelper)a.CreateInstance(string.Format("{0}.{1}", ASSEMBLY_NAME, HelperClassName));
			if (ret == null)
			{
				throw new Exception(string.Format("No DBHelperProvider named '{0}' found in the local bin directory, has it been deployed?", HelperClassName));
			}
			return ret;
		}

	}
}

