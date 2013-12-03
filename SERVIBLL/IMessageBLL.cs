using System;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IMessageBLL
	{
		bool SendSMSMessage(string numbers, string message, int senderUserID);
		bool SendMembershipEmail(Member m, int senderUserId, bool onlyNeverLoggedIn);
		bool SendMembershipEmail(string emailAddress, int senderUserID, bool onlyNeverLoggedIn);
		bool SendAllActiveMembersMembershipEmail(int senderUserID, bool onlyNeverLoggedIn);
	}
}

