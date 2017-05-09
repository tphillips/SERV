namespace SERVDAL
{
	public class BuddhaDAL
	{

		//static Logger log = new Logger();
		private SERVDataContract.DbLinq.SERVDB db;

		public BuddhaDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

