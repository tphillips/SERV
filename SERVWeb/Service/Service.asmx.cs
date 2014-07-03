using System;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;
using SERVDataContract;
using SERVBLLFactory;
using System.Data;
using System.Threading;

namespace SERVWeb
{

    public class Service : System.Web.Services.WebService
    {
		static Logger log = new Logger();

		[Obsolete]
		[WebMethod]
		public bool ImportRawRunLog()
		{
			log.LogStart();
			System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(_ImportRawRunLog));
			t.IsBackground = true;
			t.Start();
			log.LogEnd();
			return true;
		}

		[Obsolete]
		private void _ImportRawRunLog()
		{
			log.LogStart();
			try
			{
				SERVBLLFactory.Factory.RunLogBLL().ImportRawRunLog();
			}
			catch(Exception e)
			{
				log.Error(e.Message, e);
			}
			log.LogEnd();
		}

		[WebMethod(EnableSession = true)]
		public CalendarEntry GetNextShift()
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().GetMemberNextShift(CurrentUser().MemberID);
		}

		[WebMethod(EnableSession = true)]
		public List<RunLog> ListRecentRuns()
		{
			Authenticate();
			return SERVBLLFactory.Factory.RunLogBLL().ListRecent(10);
		}

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembers()
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().List("");
		}

		[WebMethod(EnableSession = true)]
		public Member GetMember(int memberId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MemberBLL().Get(memberId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveMember(Member member)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().Save(member, CurrentUser()) == member.MemberID;
		}

		[WebMethod]
		public int Register(Member member)
		{
			return SERVBLLFactory.Factory.MemberBLL().Register(member);
		}

		public int CreateBlankMember(User user)
		{
			if (user == null || user.UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			Member m = new Member();
			m.FirstName = "-";
			m.LastName = "-";
			m.EmailAddress = "@";
			m.MobileNumber = "07";
			return SERVBLLFactory.Factory.MemberBLL().Create(m);
		}

		[WebMethod(EnableSession = true)]
		public void TagMember(int memberId, string tagName)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVBLLFactory.Factory.MemberBLL().AddMemberTag(memberId, tagName);
		}

		[WebMethod(EnableSession = true)]
		public void UnTagMember(int memberId, string tagName)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVBLLFactory.Factory.MemberBLL().RemoveMemberTag(memberId, tagName);
		}

		[WebMethod(EnableSession = true)]
		public void SetMemberUserLevel(int memberId, int userLevelId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVBLLFactory.Factory.MemberBLL().SetMemberUserLevel(memberId, userLevelId);
		}

		[WebMethod(EnableSession = true)]
		public List<Member> SearchMembers(string search, bool onlyActive)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().List(search, onlyActive);
		}

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembersWithTags(string tagsCsv)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().ListMembersWithTags(tagsCsv);
		}

		[WebMethod(EnableSession = true)]
		public List<string> ListMobileNumbersWithTags(string tagsCsv)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MemberBLL().ListMobileNumbersWithTags(tagsCsv);
		}

		[WebMethod(EnableSession = true)]
		public List<Location> ListLocations()
		{
			Authenticate();
			return SERVBLLFactory.Factory.LocationBLL().ListLocations("");
		}

		[WebMethod(EnableSession = true)]
		public List<SERVDataContract.Calendar> ListCalendars()
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().ListCalendars();
		}

		[WebMethod(EnableSession = true)]
		public SERVDataContract.Calendar GetCalendar(int calendarId)
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().Get(calendarId);
		}

		[WebMethod(EnableSession = true)]
		public Location GetLocation(int locationId)
		{
			Authenticate();
			return SERVBLLFactory.Factory.LocationBLL().Get(locationId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveLocation(Location location)
		{
			Authenticate();
			return SERVBLLFactory.Factory.LocationBLL().Save(location, CurrentUser()) == location.LocationID;
		}

		[WebMethod(EnableSession = true)]
		public int GetSMSCreditCount()
		{
			Authenticate();
			return SERV.Utils.Messaging.GetAQLCreditCount();
		}

		[WebMethod(EnableSession = true)]
		public bool KeepAlive()
		{
			Authenticate();
			return true;
		}
			
		[WebMethod]
		public List<string> GetCurrentWeekADateStrings(string format)
		{
			return SERVBLLFactory.Factory.CalendarBLL().GetCurrentWeekADateStrings(format);
		}

		[WebMethod]
		public List<string> GetCurrentWeekBDateStrings(string format)
		{
			return SERVBLLFactory.Factory.CalendarBLL().GetCurrentWeekBDateStrings(format);
		}

		public int CreateBlankLocation(User user)
		{
			if (user == null || user.UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			Location m = new Location();
			m.LocationName = "-";
			return SERVBLLFactory.Factory.LocationBLL().Create(m);
		}

		[WebMethod(EnableSession = true)]
		public List<VehicleType> ListVehicleTypes()
		{
			Authenticate();
			return SERVBLLFactory.Factory.ListBLL().ListVehicleTypes();
		}

		[WebMethod(EnableSession = true)]
		public bool LogRun(int runLogID, string callDateTime, int callFromLocationId, string collectDateTime, int collectionLocationId, 
			int controllerMemberId, string deliverDateTime, int deliverToLocationId, string dutyDate, 
			int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, 
			int vehicleTypeId, string productIdCsv, string homeSafeDateTime, string notes)
		{
			Authenticate();
			// Fix dates and times
			DateTime _dutyDate = DateTime.Parse(dutyDate);
			DateTime _callDateTime = DateTime.Parse(callDateTime);
			DateTime _collectDateTime = DateTime.MinValue;
			DateTime _deliverDateTime = DateTime.MinValue;
			DateTime? _homeSafeDateTime = null;
			if (riderMemberId > 0)
			{
				_collectDateTime = DateTime.Parse(collectDateTime);
				_deliverDateTime = DateTime.Parse(deliverDateTime);
				if (!string.IsNullOrEmpty(homeSafeDateTime.Replace(":","").Trim())){ _homeSafeDateTime = DateTime.Parse(homeSafeDateTime); }
			}
			// Log it
			bool res = SERVBLLFactory.Factory.RunLogBLL().CreateRunLog(_callDateTime, callFromLocationId, _collectDateTime, collectionLocationId, 
				controllerMemberId, CurrentUser().UserID, _deliverDateTime, deliverToLocationId, _dutyDate, 
				finalDestinationLocationId, originLocationId, riderMemberId, urgency, vehicleTypeId, productIdCsv, _homeSafeDateTime, notes);
			// If we were updating a record, delete the old one
			if (runLogID > 0)
			{
				SERVBLLFactory.Factory.RunLogBLL().DeleteRun(runLogID);
			}
			return res;
		}

		[WebMethod(EnableSession = true)]
		public RunLog GetRunLog(int runLogID)
		{
			return SERVBLLFactory.Factory.RunLogBLL().Get(runLogID);
		}

		[WebMethod(EnableSession = true)]
		public bool LogAARun(string dutyDate, string collectDateTime, string deliverDateTime, string returnDateTime,
			int controllerMemberId, int riderMemberId, 
			int vehicleTypeId, string boxesOutCsv, string boxesInCsv, string notes)
		{
			Authenticate();
			DateTime _collectDateTime = DateTime.Parse(collectDateTime);
			DateTime _deliverDateTime = DateTime.Parse(deliverDateTime);
			DateTime _returnDateTime = DateTime.Parse(returnDateTime);
			DateTime _dutyDate = DateTime.Parse(dutyDate);
			return SERVBLLFactory.Factory.RunLogBLL().CreateAARunLog(_dutyDate, _collectDateTime,  
				controllerMemberId, CurrentUser().UserID, _deliverDateTime, _returnDateTime,
				riderMemberId, vehicleTypeId, boxesOutCsv, boxesInCsv, notes);
		}

		[WebMethod(EnableSession = true)]
		public bool SendSMSMessage(string numbers, string message)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MessageBLL().SendSMSMessage(numbers, message, CurrentUser().UserID);
		}

		[WebMethod(EnableSession = true)]
		public bool SendTestEmail(string address)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MessageBLL().SendMembershipEmail(address, CurrentUser().UserID, false);
		}

		[WebMethod(EnableSession = true)]
		public bool SendMembershipEmails(string sure, bool onlyNeverLoggedIn)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			if (sure != "YES"){ throw new InvalidExpressionException("You don't seem to be sure about that"); }
			SERVBLLFactory.Factory.MessageBLL().SendAllActiveMembersMembershipEmailInBackground(CurrentUser().MemberID, onlyNeverLoggedIn);
			return true;
		}

		public User Login(string username, string passwordHash)
		{
			return SERVBLLFactory.Factory.MemberBLL().Login(username, passwordHash);
		}

		[WebMethod(EnableSession = true)]
		public bool Impersonate(int memberId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVGlobal.User = SERVBLLFactory.Factory.MemberBLL().GetUserForMember(memberId);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public bool TakeControl(string overrideNumber)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.ShiftBLL().TakeControl(CurrentUser().MemberID, overrideNumber);
		}

		[WebMethod(EnableSession = true)]
		public bool RosterVolunteer(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.CalendarBLL().RosterVolunteer(calendarId, memberId, rosteringWeek, rosteringDay);
		}

		[WebMethod(EnableSession = true)]
		public bool RemoveRotaSlot(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.CalendarBLL().RemoveRotaSlot(calendarId, memberId, rosteringWeek, rosteringDay);
		}

		[WebMethod(EnableSession = true)]
		public List<RosteredVolunteer> ListRosteredVolunteers(int calendarId)
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().ListRosteredVolunteers(calendarId);
		}

		[WebMethod]
		public void GenerateCalendar()
		{
			SERVBLLFactory.Factory.CalendarBLL().GenerateCalendar();
		}

		[WebMethod(EnableSession = true)]
		public List<List<CalendarEntry>> ListWeeksCaledarEntries()
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().ListWeeksCaledarEntries();
		}

		[WebMethod(EnableSession = true)]
		public List<List<CalendarEntry>> ListSpansCaledarEntries(int days)
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().ListSpansCaledarEntries(days);
		}

		[WebMethod(EnableSession = true)]
		public bool MarkShiftSwapNeeded(int calendarId, int memberId, DateTime shiftDate)
		{
			Authenticate();
			if (CurrentUser().MemberID != memberId && CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.CalendarBLL().MarkShiftSwapNeeded(calendarId, memberId, shiftDate);
		}

		[WebMethod(EnableSession = true)]
		public bool AddVolunteerToCalendar(int calendarId, int memberId, DateTime shiftDate)
		{
			Authenticate();
			if (CurrentUser().MemberID != memberId && CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.CalendarBLL().AddVolunteerToCalendar(calendarId, memberId, shiftDate, memberId == CurrentUser().MemberID);
		}

		private User CurrentUser()
		{
			if (System.Web.HttpContext.Current.Session["User"] == null) { return null; }
			return (User)Session["User"];
		}

		private void Authenticate()
		{
			if (CurrentUser() == null)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
		}

    }
}

