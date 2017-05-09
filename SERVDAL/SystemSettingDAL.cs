namespace SERVDAL
{
	public class SystemSettingDAL
	{

		//static Logger log = new Logger();
		private SERVDataContract.DbLinq.SERVDB db;

		public SystemSettingDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

