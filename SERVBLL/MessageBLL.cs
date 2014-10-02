using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;
using System.Net.Mail;
using System.Threading;

namespace SERVBLL
{

	public class MessageBLL : IMessageBLL
	{

		private static System.Net.Mail.SmtpClient c = new SmtpClient(SERVER);
		private static Logger log = new Logger();

		private const string FROM = "noreply@system.servssl.org.uk";
		private const string SERVER = "localhost";
		public static string FOOTER = "\r\n\r\n\r\nThis message was sent from an unattended mailbox by the SERV SSL System.  Do not reply to this mail.  If you need to make contact, please use the Forum to PM Tristan Phillips.\r\n";

		public MessageBLL()
		{
		}

		struct CalendarEmailArgs
		{
			public int MemberId;
			public int CalendarEntryId;
			public int CalendarId;
			public DateTime ShiftDate;
		}

		struct FeedbackEmailArgs
		{
			public User user;
			public string feedback;

		}

		public void SendFeedback(User user, string feedback)
		{
			ParameterizedThreadStart pts = new ParameterizedThreadStart(_SendFeedback);
			FeedbackEmailArgs args = new FeedbackEmailArgs();
			args.feedback = feedback;
			args.user = user;
			Thread t = new Thread(pts);
			t.IsBackground = true;
			t.Start(args);
		}

		public void _SendFeedback(object args)
		{
			FeedbackEmailArgs cargs = (FeedbackEmailArgs)args;
			foreach (Member m in new MemberBLL().ListAdministrators())
			{
				SendEmail(m.EmailAddress, "SERV System - Feedback Sent", 
					string.Format("Hi {0},\r\n\r\n" +
						"A member left feedback on the system or SERV: \r\n\r\n{1}{2}", 
						m.FirstName, cargs.feedback, MessageBLL.FOOTER), cargs.user.UserID);
			}
		}

		public void SendCalendarVolunteerNotificationEmail(int memberID, int calendarEntryID)
		{
			ParameterizedThreadStart pts = new ParameterizedThreadStart(_SendCalendarVolunteerNotificationEmail);
			CalendarEmailArgs args = new CalendarEmailArgs();
			args.MemberId = memberID;
			args.CalendarEntryId = calendarEntryID;
			Thread t = new Thread(pts);
			t.IsBackground = true;
			t.Start(args);
		}

		private void _SendCalendarVolunteerNotificationEmail(object args)
		{
			CalendarEmailArgs cargs = (CalendarEmailArgs)args;
			Member m = new MemberBLL().Get(cargs.MemberId);
			CalendarEntry c = new CalendarBLL().GetCalendarEntry(cargs.CalendarEntryId);
			if (m == null || c == null)
			{
				throw new InvalidOperationException();
			}
			SendEmail(m.EmailAddress, "You have been given a shift on " + c.EntryDateShortStringWithDay, 
				"Hi " + m.FirstName + ",\n\n" +
				"This is an email to let you know that you have been put down for a shift on the " + c.CalendarName + " calendar on " + c.EntryDateShortStringWithDay + ".  This is not a recurring shift.  Why not add this to your personal calendar now?\n\n" +
				"The SERV SSL Calendar can be found here: http://system.servssl.org.uk/Calendar\n\n" +
				"Thanks,\n\n" +
				"SERV SSL System" + FOOTER, new MemberBLL().GetUserIdForMember(cargs.MemberId));
		}

		public void SendCalendarVolunteeringThanksEmail(int memberID, int calendarEntryID)
		{
			ParameterizedThreadStart pts = new ParameterizedThreadStart(_SendCalendarVolunteeringThanksEmail);
			CalendarEmailArgs args = new CalendarEmailArgs();
			args.MemberId = memberID;
			args.CalendarEntryId = calendarEntryID;
			Thread t = new Thread(pts);
			t.IsBackground = true;
			t.Start(args);
		}

		private void _SendCalendarVolunteeringThanksEmail(object args)
		{
			CalendarEmailArgs cargs = (CalendarEmailArgs)args;
			Member m = new MemberBLL().Get(cargs.MemberId);
			CalendarEntry c = new CalendarBLL().GetCalendarEntry(cargs.CalendarEntryId);
			if (m == null || c == null)
			{
				throw new InvalidOperationException();
			}
			SendEmail(m.EmailAddress, "You volunteered for a shift on " + c.EntryDateShortStringWithDay + ", Thanks!", 
				"Hi " + m.FirstName + ",\n\n" +
				"You are a star!  Thank you very much for putting your name down on the " + c.CalendarName + " calendar on " + c.EntryDateShortStringWithDay + ".  Why not add this to your personal calendar now?\n\n" +
				"The SERV SSL Calendar can be found here: http://system.servssl.org.uk/Calendar\n\n" +
				"Thanks,\n\n" +
				"SERV SSL System" + FOOTER, new MemberBLL().GetUserIdForMember(cargs.MemberId));
		}

		public void SendShiftSwapNeededEmail(int memberID, int calendarID, DateTime date)
		{
			bool SendSwapEmails = false;
			object setting = System.Configuration.ConfigurationManager.AppSettings["SendSwapNeededEmails"];
			if (setting != null)
			{
				SendSwapEmails = bool.Parse(setting.ToString());
			}
			if (SendSwapEmails)
			{
				ParameterizedThreadStart pts = new ParameterizedThreadStart(_SendShiftSwapNeededEmail);
				CalendarEmailArgs args = new CalendarEmailArgs();
				args.MemberId = memberID;
				args.CalendarId = calendarID;
				args.ShiftDate = date;
				Thread t = new Thread(pts);
				t.IsBackground = true;
				t.Start(args);
			}
		}

		private void _SendShiftSwapNeededEmail(object args)
		{
			CalendarEmailArgs cargs = (CalendarEmailArgs)args;
			Member shiftSwapper = new MemberBLL().Get(cargs.MemberId);
			Calendar calendar = new CalendarBLL().Get(cargs.CalendarId);
			if (shiftSwapper == null || calendar == null)
			{
				throw new InvalidOperationException();
			}
			List<Member> distroList = new MemberBLL().ListMembersWithTags("Blood,AA,Water,Controller,Milk");
			foreach (Member recipient in distroList)
			{
				if (recipient.MemberID != shiftSwapper.MemberID)
				{
					Member mailToMember = new MemberBLL().Get(recipient.MemberID);
					if (mailToMember != null && !string.IsNullOrEmpty(mailToMember.EmailAddress) && mailToMember.EmailAddress.Trim() != "@")
					{
						SendEmail(mailToMember.EmailAddress, shiftSwapper.Name + " needs your help!", 
							"Hi " + mailToMember.FirstName + ",\n\n" +
							"Sadly, " + shiftSwapper.FirstName + " cannot perform his/her " + calendar.Name + " duty on " + cargs.ShiftDate.ToString("ddd dd MMM") + ".\n\n" +
							"We really could use your help!  Are you free?  If you have a few hours spare please put your name down.\n\n" +
							"The SERV SSL Calendar can be found here: http://system.servssl.org.uk/Calendar\n\n" +
							"Thanks very much in advance!\n\n" +
							"SERV SSL System" + FOOTER, new MemberBLL().GetUserIdForMember(shiftSwapper.MemberID));
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(shiftSwapper.EmailAddress) && shiftSwapper.EmailAddress.Trim() != "@")
					{
						SendEmail(shiftSwapper.EmailAddress, "A swap request has been sent out for you", 
							"Hi " + shiftSwapper.FirstName + ",\n\n" +
							"As requested, you have been removed from " + calendar.Name + " duty on " + cargs.ShiftDate.ToString("ddd dd MMM") + ".\n\n" +
							"An email has been sent out to see if someone can cover for you.\n\n" +
							"The SERV SSL Calendar can be found here: http://system.servssl.org.uk/Calendar\n\n" +
							"Thanks,\n\n" +
							"SERV SSL System" + FOOTER, new MemberBLL().GetUserIdForMember(cargs.MemberId));
					}
				}
			}
		}

		public bool SendSMSMessage(string numbers, string message, int senderUserID, bool fromServ)
		{
			numbers = numbers.Trim().Replace(" ", "");
			if (numbers.EndsWith(",")) { numbers = numbers.Substring(0, numbers.Length - 1); }
			SERVDALFactory.Factory.MessageDAL().LogSentSMSMessage(numbers, message, senderUserID);
			if (!fromServ)
			{
				Member sender = new MemberBLL().GetByUserID(senderUserID);
				SERV.Utils.Messaging.SendTextMessage(numbers, message, sender.MobileNumber);
				return true;
			}
			SERV.Utils.Messaging.SendTextMessage(numbers, message);
			return true;
		}

		public bool SendMemberUpdateEmail(Member m, User u, int senderUserID)
		{
			DateTime? lastLoginDate = u.LastLoginDate;
			m = new MemberBLL().Get(m.MemberID);
			string tags = "";
			foreach (Tag t in m.Tags)
			{
				tags += "\"" + t.TagName + "\"   ";
			}
			string body = "Hi " + m.FirstName + ",\r\n\r\n" +
			              "This is a periodical email from the SERV SSL Membership System to confirm the data we hold about you.\r\n\r\n" +
					(
				    lastLoginDate == null ? 
						"We notice you have not yet logged into your account.  " +
				              "It is essential that you do so to keep your contact details and volunteering preferences up to date.\r\n\r\n  " +
				              "Please goto http://system.servssl.org.uk ASAP.\r\n" +
				              "More information regarding the new Membership System and login instructions can be found in this SERV Forum topic: http://servssl.org.uk/members/index.php?/topic/1437-serv-system-action-required-by-all/\r\n\r\n" 
						: 
					    "You last logged onto the system on " + ((DateTime)lastLoginDate).ToString("dd MMM yy HH:mm") + "\r\n\r\n"
				    ) +
				"Here are the details we currently hold for you.  Please check them and update them in the system if they are incorrect.\r\n\r\n" +
			              "First Name: " + m.FirstName + "\r\n" +
			              "Last Name: " + m.LastName + "\r\n" +
			              "Email: " + m.EmailAddress + "\r\n" +
			              "Mobile: " + m.MobileNumber + "\r\n" +
			              "Home Phone: " + m.HomeNumber + "\r\n" +
			              "Birth Year: " + m.BirthYear + "\r\n" +
			              "Occupation: " + m.Occupation + "\r\n" +
			              "Address: " + m.Address1 + " " + m.Address2 + " " + m.Address3 + " " + m.Town + " " + m.County + "\n" +
			              "Post Code: " + m.PostCode + "\r\n" +
			              "Next of Kin (NOK): " + m.NextOfKin + "\r\n" +
			              "NOK Address: " + m.NextOfKinAddress + "\r\n" +
			              "NOK Phone: " + m.NextOfKinPhone + "\r\n\r\n" +
				"Your preferences (Tags):\r\n\r\n" +
				tags + "\r\n\r\n" +
				"Thanks,\r\n\r\nSERV SSL System" + FOOTER;
			return SendEmail(m.EmailAddress, "SERV SSL Member Update", body, senderUserID);
		}

		public bool SendAllActiveMembersMembershipEmailInBackground(int senderUserID, bool onlyNeverLoggedIn)
		{
			ParameterizedThreadStart pts = new ParameterizedThreadStart(_SendAllActiveMembersMembershipEmail);
			SendMembershipEmailArgs args = new SendMembershipEmailArgs();
			args.CurrentUserID = senderUserID;
			args.OnlyNeverLoggedIn = onlyNeverLoggedIn;
			Thread t = new Thread(pts);
			t.IsBackground = true;
			t.Start(args);
			return true;
		}

		struct SendMembershipEmailArgs
		{
			public int CurrentUserID;
			public bool OnlyNeverLoggedIn;
		}

		private void _SendAllActiveMembersMembershipEmail(object args)
		{
			SendMembershipEmailArgs a = (SendMembershipEmailArgs)args;
			SendAllActiveMembersMembershipEmail(a.CurrentUserID, a.OnlyNeverLoggedIn);
		}

		public bool SendAllActiveMembersMembershipEmail(int senderUserID, bool onlyNeverLoggedIn)
		{
			DateTime s = log.LogStart();
			List<Member> members = new MemberBLL().List("", true);
			foreach (Member m in members)
			{
				try
				{
					log.Debug(string.Format("Sending membership update to {0}", m.EmailAddress));
					SendMembershipEmail(m, senderUserID, onlyNeverLoggedIn);
					Thread.Sleep(20);
				}
				catch(Exception e)
				{
					log.Debug(string.Format("Membership update to {0} failed because {1}", m.EmailAddress, e.Message));
				}
			}
			log.LogEnd();
			log.LogPerformace(s);
			return true;
		}

		public bool SendEmail(string address, string subject, string body, int senderUserID)
		{
			log.LogStart();
			log.Debug(string.Format("{0} >> {1} :: {2} sender: {3}", address, subject, body, senderUserID));
			if (address == "@")
			{
				return true;
			}
			MailMessage m = new MailMessage(FROM, address);
			m.Body = body;
			m.Subject = subject;
			SERVDALFactory.Factory.MessageDAL().LogSentEmailMessage(address, m.Subject + " :: " + m.Body, senderUserID);
			ParameterizedThreadStart pts = new ParameterizedThreadStart(_DoSendMail);
			Thread t = new Thread(pts);
			t.IsBackground = true;
			t.Start(m);
			return true;
		}

		private void _DoSendMail(object mail)
		{
			MailMessage m = (MailMessage)mail;
			try
			{
				c.Send(m);
				log.Debug(string.Format("Sent a mail to {0}", m.To));
			}
			catch (Exception e)
			{
				log.Error(e.Message, e);
			}
		}

		public bool SendMembershipEmail(string emailAddress, int senderUserID, bool onlyNeverLoggedIn)
		{
			Member m = new MemberBLL().GetByEmail(emailAddress);
			return SendMembershipEmail(m, senderUserID, onlyNeverLoggedIn);
		}

		public bool SendMembershipEmail(Member m, int senderUserID, bool onlyNeverLoggedIn)
		{
			if (m != null)
			{
				User u = new MemberBLL().GetUserForMember(m.MemberID);
				if (u != null)
				{
					if (u.LastLoginDate == null || !onlyNeverLoggedIn)
					{
						return SendMemberUpdateEmail(m, u, senderUserID);
					}
					else
					{
						log.Debug("Skipping as member has logged in");
						return true;
					}
				}
			}
			return false;
		}

		public void SendPasswordResetEmail(Member m, string newPass, int userID)
		{
			SendEmail(m.EmailAddress, "SERV System Password Reset", 
				string.Format("Hi {0},\r\n\r\n" + 
					"Somebody requested a new password for your SERV SSL System account (NOT the Forum). Your new password is:\r\n\r\n" + 
					"{1}\r\n\r\n" + "If you did not request this, please note that this IS STILL your new password.  This may have been done for you by a system administrator.  If you think this was done in error, please report it on the forum.\r\n\r\n" + 
					"We suggest you now change your password to something more memorable.\r\n\r\n" + 
					"Please goto http://system.servssl.org.uk ASAP.\r\n\r\n" +
					"Thanks,\r\n\r\n" + 
					"SERV SSL System {2}", 
					m.FirstName, newPass, MessageBLL.FOOTER), userID);
		}

		public void SendNewMemberEmail(Member member, int userId)
		{
		
			foreach (Member m in new MemberBLL().ListAdministrators())
			{
				SendEmail(m.EmailAddress, "New System Member Registration", 
					string.Format("Hi {0},\r\n\r\n" +
						"{1} has just signed up as a new volunteer!\r\n\r\n" +
					"Thanks,\r\n\r\n" +
						"SERV SSL System {2}", 
						m.FirstName, member.Name, MessageBLL.FOOTER), userId);
			}

			SendEmail(member.EmailAddress, "Welcome to SERV", 
				string.Format("Hi {0},\r\n\r\n" + 
					"Welcome to SERV. You will be sent a new password in a separate email.\r\n\r\n" +
					"Volunteering for SERV as a Rider, Driver, Controller or Fundraiser is a very rewarding endeavour and supports a service that makes a real difference.\r\n\r\n" +
					"If you have not yet attended an induction session, a member of our training team will be in touch shortly.  If you have, then our calendar manager will contact you regarding your availability.\r\n\r\n" +
					"Thanks for your interest and support,\r\n\r\n" + 
					"SERV SSL System {1}", 
					member.FirstName, MessageBLL.FOOTER), userId);

		}

	}
}

