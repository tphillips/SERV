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

		static Logger log = new Logger();

		private const int SCHEDULED_RUN_HOUR = 22;

		public ControllerBLL()
		{
		}

		public int OrderRun(int requestorMemberId, RunLog order, List<int> products)
		{
			new RunLogBLL().CreateOrder(order, products);
			throw new NotImplementedException();
		}

		public void ProcessQueuedOrders()
		{
			List<RunLog> queue = new RunLogBLL().ListQueuedOrders();
			foreach (RunLog order in queue)
			{
				// See if rider needs to be assigned
				// decide on bike / car / hooleygan based on number of boxes, time and final destination
				// Choose rider from on shift resources
				// Request rider
				// Set rider id on order and save
			}
		}

		public void CheckActiveRunsProgress()
		{
			// Check for delayed pickups, drop offs, home safes
			// raise exception to human
		}

		public void HandleIncomingMessage(Message message)
		{
			// List all orders which have the riderid assigned, parse and process
		}

		public bool DivertNumber(Member member, string overrideNumber)
		{
			string number = string.IsNullOrEmpty(overrideNumber) ? member.MobileNumber : overrideNumber;
			return DivertNumber(number);
		}

		public bool DivertNumber(string mobile)
		{
			log.LogStart();
			System.Net.ServicePointManager.ServerCertificateValidationCallback += SERV.Utils.Authentication.AcceptAllCertificates;
			string num = mobile.Replace(" ","");
			string servNow = System.Configuration.ConfigurationManager.AppSettings["FlextelNumber"];
			string pin = System.Configuration.ConfigurationManager.AppSettings["FlextelPin"];
			string format = System.Configuration.ConfigurationManager.AppSettings["FlextelFormat"];
			try
			{
				log.Info("Diverting flextel to " + num);
				string res = new System.Net.WebClient().DownloadString(string.Format(format, servNow, pin, num));
				log.Info(res);
				res = res.Trim();
				if (res.Contains(","))
				{
					return res.Split(',')[0].Replace(" ","") == num;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

	}
}

