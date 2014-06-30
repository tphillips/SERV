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
		bool SendAllActiveMembersMembershipEmailInBackground(int senderUserID, bool onlyNeverLoggedIn);
		void SendPasswordResetEmail(Member m, string newPass, int userID);
		void SendCalendarVolunteerNotificationEmail(int memberID, int calendarEntryID);
		void SendCalendarVolunteeringThanksEmail(int memberID, int calendarEntryID);
		void SendShiftSwapNeededEmail(int memberID, int calendarID, DateTime shiftDate);
	}
}

