using System;

namespace SERVIBLL
{
	public interface IMessageBLL
	{
		bool SendSMSMessage(string numbers, string message, int senderUserID);
		bool SendTestEmail(string address, int senderUserId);
	}
}

