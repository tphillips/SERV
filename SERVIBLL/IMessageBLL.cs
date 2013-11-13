using System;

namespace SERVIBLL
{
	public interface IMessageBLL
	{
		bool SendSMSMessage(string numbers, string message, int senderUserID);
	}
}

