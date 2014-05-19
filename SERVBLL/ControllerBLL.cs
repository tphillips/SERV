using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	public class ControllerBLL : IControllerBLL
	{

		Logger log = new Logger();

		public ControllerBLL()
		{
		}

		public bool DivertNumber(int memberID)
		{
			throw new NotImplementedException();
		}

		public bool DivertNumber(string mobile)
		{
			string servNow = System.Configuration.ConfigurationManager.AppSettings["FlextelNumber"];
			string pin = System.Configuration.ConfigurationManager.AppSettings["FlextelPin"];
			string format = System.Configuration.ConfigurationManager.AppSettings["FlextelFormat"];
			string res = new System.Net.WebClient().DownloadString(string.Format(format, servNow, pin, mobile));
			log.Info(res);
			return true;
		}

	}
}

