using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	public class MessageBLL : IMessageBLL
	{
		public MessageBLL()
		{
		}

		public bool SendSMSMessage(string numbers, string message, int senderUserID)
		{
			numbers = numbers.Trim().Replace(" ", "");
			if (numbers.EndsWith(",")) { numbers = numbers.Substring(0, numbers.Length - 1); }
			SERVDALFactory.Factory.MessageDAL().LogSentSMSMessage(numbers, message, senderUserID);
			SERV.Utils.Messaging.SendTextMessage(numbers, message);
			return true;
		}

	}
}

