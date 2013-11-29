using System;

namespace SERVIBLL
{
	public interface IMessageBLL
	{
		bool SendSMSMessage(string numbers, string message, int senderUserID);
		bool SendMembershipEmail(string address, int senderUserId, bool onlyNeverLoggedIn);
		bool SendAllActiveMembersMembershipEmail(int senderUserID, bool onlyNeverLoggedIn);
	}
}

