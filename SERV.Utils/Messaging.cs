using System;

namespace SERV.Utils
{
	public class Messaging
	{
		public Messaging()
		{
		}
		public static string SendTextMessage(string to, string message)
		{
			string smsUser = System.Configuration.ConfigurationManager.AppSettings["SMSUser"];
			string smsPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
			string smsFrom = System.Configuration.ConfigurationManager.AppSettings["SMSFrom"];
			string res = new System.Net.WebClient().DownloadString(
				string.Format("http://services.dynmark.com/HttpServices/SendMessage.ashx?user={0}&password={1}&to={2}&from={3}&text={4}", 
				smsUser, smsPassword, to, smsFrom, message));
			return res;
		}
	}
}

