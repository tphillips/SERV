#define DYNMARK
	

using System;

namespace SERV.Utils
{
	public class Messaging
	{

		static Logger log = new Logger();

		public Messaging()
		{
		}

		public static bool SendTextMessages(string numbers, string message)
		{
			numbers = numbers.Trim().Replace(" ", "");
			if (numbers.EndsWith(",")) { numbers = numbers.Substring(0, numbers.Length - 1); }
			SendTextMessage(numbers, message);
			return true;
		}

		public static string SendTextMessage(string to, string message)
		{
			string smsFrom = System.Configuration.ConfigurationManager.AppSettings["SMSFrom"];
			return SendTextMessage(to, message, smsFrom);
		}

		public static string SendTextMessage(string to, string message, string from)
		{
			from = from.Replace(" ","");
			to = to.Replace(" ","");
			if (from.StartsWith("07")){ from = "44" + from.Substring(1, from.Length - 1); }
			if (to.StartsWith("07")) { to = "44" + to.Substring(1, to.Length - 1); }
			message = message.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
			message = System.Web.HttpUtility.UrlEncode(message);
			string provider = System.Configuration.ConfigurationManager.AppSettings["SMSProvider"];
			log.Info(string.Format("SMS using {0} To {1} - {2}",provider, to, message));
			string smsUser = System.Configuration.ConfigurationManager.AppSettings["SMSUser"];
			string smsPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
			string res = "";
			if (provider == "Dynmark")
			{
				try
				{
					res = new System.Net.WebClient().DownloadString(
						string.Format("http://services.dynmark.com/HttpServices/SendMessage.ashx?user={0}&password={1}&to={2}&from={3}&text={4}", 
							smsUser, smsPassword, to, from, message));
				}catch{
				}
			}
			else
			{
				try
				{
					res = new System.Net.WebClient().DownloadString(
						string.Format("http://gw.aql.com/sms/sms_gw.php?username={0}&password={1}&destination={2}&originator={3}&message={4}", 
							smsUser, smsPassword, to, from, message));
				}
			}
			log.Info("SMS: " + res);
			return res;
		}

		public static int GetAQLCreditCount()
		{
			string smsUser = System.Configuration.ConfigurationManager.AppSettings["SMSUser"];
			string smsPassword = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
			string res = new System.Net.WebClient().DownloadString(
				string.Format("http://gw1.aql.com/sms/postmsg.php?username={0}&password={1}&cmd=credit", smsUser, smsPassword));
			if (res.Contains("="))
			{
				int ret = int.Parse(res.Split('=')[1].Trim());
				return ret;
			}
			return -1;
		}

	}
}

