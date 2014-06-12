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

		public bool DivertNumber(int memberID, string overrideNumber)
		{
			Member controller = new MemberBLL().Get(memberID);
			string number = string.IsNullOrEmpty(overrideNumber) ? controller.MobileNumber : overrideNumber;
			return DivertNumber(number);
		}

		public bool DivertNumber(string mobile)
		{
			System.Net.ServicePointManager.ServerCertificateValidationCallback += SERV.Utils.Authentication.AcceptAllCertificates;
			string num = mobile.Replace(" ","");
			string servNow = System.Configuration.ConfigurationManager.AppSettings["FlextelNumber"];
			string pin = System.Configuration.ConfigurationManager.AppSettings["FlextelPin"];
			string format = System.Configuration.ConfigurationManager.AppSettings["FlextelFormat"];
			string res = new System.Net.WebClient().DownloadString(string.Format(format, servNow, pin, num));
			log.Info(res);
			res = res.Trim();
			if (res.Contains(","))
			{
				return res.Split(',')[0].Replace(" ","") == num;
			}
			return false;
		}

	}
}

