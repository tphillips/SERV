using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;
using System.Net.Mail;

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

		public bool SendTestEmail(string address, int senderUserID)
		{
			System.Net.Mail.SmtpClient c = new SmtpClient("localhost");
			MailMessage m = new MailMessage("NOREPLY@system.servssl.org.uk", address);
			m.Body = "test";
			m.Subject = "test";
			SERVDALFactory.Factory.MessageDAL().LogSentEmailMessage(address, m.Subject + " :: " + m.Body, senderUserID);
			c.Send(m);
			return true;
		}

	}
}

