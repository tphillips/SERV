using System;
using System.Web.Services;
using System.Collections.Generic;
using SERVDataContract;
using SERVBLL;
using System.Data;

namespace SERVWeb
{

	public class Service : System.Web.Services.WebService
    {
		
		private SERVDataContract.User CurrentUser()
		{
			if (System.Web.HttpContext.Current.Session["User"] == null) { return null; }
			return (SERVDataContract.User)System.Web.HttpContext.Current.Session["User"];
		}

		public void Authenticate()
		{
			if (CurrentUser() == null)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
		}

		[WebMethod(EnableSession = true)]
		public bool SendTestTweetEmail()
		{
			Authenticate();
			return new MessageBLL().SendTestTweet(CurrentUser().MemberID);
		}

		[WebMethod]
		public bool TweetYesterdaysSummary()
		{
			return new MessageBLL().TweetYesterdaysSummary();
		}

		[WebMethod]
		public string GetYesterdaysSummary()
		{
			return new RunLogBLL().GetYesterdaysSummary();
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<RunLog> ListRecentRuns()
		{
			Authenticate();
			return new RunLogBLL().ListRecent(10);
		}

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembers()
		{
			Authenticate();
			return new MemberBLL().List("");
		}

		[WebMethod(EnableSession = true)]
		public string ExecuteSQL(string sql)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new RunLogBLL().ExecuteSQL(sql);
		}

		[WebMethod(EnableSession = true)]
		public Member GetMember(int memberId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new MemberBLL().Get(memberId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveMember(Member member)
		{
			Authenticate();
			return new MemberBLL().Save(member, CurrentUser()) == member.MemberID;
		}

		[WebMethod]
		public int Register(Member member)
		{
			return new MemberBLL().Register(member);
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
			m.GroupID = user.Member.GroupID;
			return new MemberBLL().Create(m);
		}

		[WebMethod(EnableSession = true)]
		public void TagMember(int memberId, string tagName)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			new MemberBLL().AddMemberTag(memberId, tagName);
		}

		[WebMethod(EnableSession = true)]
		public void UnTagMember(int memberId, string tagName)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			new MemberBLL().RemoveMemberTag(memberId, tagName);
		}

		[WebMethod(EnableSession = true)]
		public void SetMemberUserLevel(int memberId, int userLevelId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			new MemberBLL().SetMemberUserLevel(memberId, userLevelId);
		}

		[WebMethod(EnableSession = true)]
		public List<Member> SearchMembers(string search, bool onlyActive)
		{
			Authenticate();
			return new MemberBLL().List(search, onlyActive);
		}

		[WebMethod(EnableSession = true, CacheDuration=60)]
		public List<Member> ListMembersWithTags(string tagsCsv)
		{
			Authenticate();
			return new MemberBLL().ListMembersWithAnyTagsIn(tagsCsv);
		}

		[WebMethod(EnableSession = true, CacheDuration=60)]
		public List<string> ListMobileNumbersWithTags(string tagsCsv)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new MemberBLL().ListMobileNumbersWithAllTags(tagsCsv);
		}

		[WebMethod(EnableSession = true, CacheDuration=60)]
		public List<Location> ListLocations()
		{
			Authenticate();
			return new LocationBLL().ListLocations("");
		}
			
		[WebMethod(EnableSession = true)]
		public Location GetLocation(int locationId)
		{
			Authenticate();
			return new LocationBLL().Get(locationId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveLocation(Location location)
		{
			Authenticate();
			return new LocationBLL().Save(location, CurrentUser()) == location.LocationID;
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

		public int CreateBlankLocation(User user)
		{
			if (user == null || user.UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			Location m = new Location();
			m.LocationName = "-";
			return new LocationBLL().Create(m);
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<VehicleType> ListVehicleTypes()
		{
			Authenticate();
			return new ListBLL().ListVehicleTypes();
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<Group> ListGroups()
		{
			Authenticate();
			return new ListBLL().ListGroups();
		}

		[WebMethod(EnableSession = true)]
		public bool LogRun(int runLogID, string callDateTime, int callFromLocationId, string collectDateTime, int collectionLocationId, 
			int controllerMemberId, string deliverDateTime, int deliverToLocationId, string dutyDate, 
			int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, 
			int vehicleTypeId, string productIdCsv, string homeSafeDateTime, string notes, string callerNumber, string callerExt)
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
			bool res = new RunLogBLL().CreateRunLog(_callDateTime, callFromLocationId, _collectDateTime, collectionLocationId, 
				controllerMemberId, CurrentUser().UserID, _deliverDateTime, deliverToLocationId, _dutyDate, 
				finalDestinationLocationId, originLocationId, riderMemberId, urgency, vehicleTypeId, productIdCsv, _homeSafeDateTime, notes, callerNumber, callerExt);
			// If we were updating a record, delete the old one
			if (runLogID > 0)
			{
				new RunLogBLL().DeleteRun(runLogID);
			}
			return res;
		}

		[WebMethod(EnableSession = true)]
		public bool DeleteRun(int runLogID)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			new RunLogBLL().DeleteRun(runLogID);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public RunLog GetRunLog(int runLogID)
		{
			return new RunLogBLL().Get(runLogID);
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
			return new RunLogBLL().CreateAARunLog(_dutyDate, _collectDateTime,  
				controllerMemberId, CurrentUser().UserID, _deliverDateTime, _returnDateTime,
				riderMemberId, vehicleTypeId, boxesOutCsv, boxesInCsv, notes);
		}

		[WebMethod(EnableSession = true)]
		public bool SendSMSMessage(string numbers, string message, bool fromServ)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new MessageBLL().SendSMSMessage(numbers, message, CurrentUser().UserID, fromServ);
		}

		[WebMethod(EnableSession = true)]
		public bool SendFeedback(string feedback)
		{
			Authenticate();
			new MessageBLL().SendFeedback(CurrentUser(), feedback);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public bool SendTestEmail(string address)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new MessageBLL().SendMembershipEmail(address, CurrentUser().UserID, false);
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
			new MessageBLL().SendAllActiveMembersMembershipEmailInBackground(CurrentUser().MemberID, onlyNeverLoggedIn);
			return true;
		}

		public User Login(string username, string passwordHash)
		{
			return new MemberBLL().Login(username, passwordHash);
		}

		[WebMethod(EnableSession = true)]
		public bool Impersonate(int memberId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVGlobal.User = new MemberBLL().GetUserForMember(memberId);
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
			return new ShiftBLL().TakeControl(CurrentUser().MemberID, overrideNumber);
		}


		[WebMethod(EnableSession = true)]
		public string[] GetMemberUniqueRuns()
		{
			Authenticate();
			return new RunLogBLL().GetMemberUniqueRuns(CurrentUser().MemberID);
		}

		[WebMethod]
		public string SwitchController()
		{
			return new ShiftBLL().SwitchController();
		}
			
		[WebMethod]
		public void GCCollect()
		{
			GC.Collect();
		}

		[WebMethod(EnableSession = true)]
		public List<List<CalendarEntry>> ListWeeksCaledarEntries()
		{
			Authenticate();
			return new CalendarBLL().ListWeeksCaledarEntries();
		}

		[WebMethod(EnableSession = true)]
		public List<List<CalendarEntry>> ListSpansCaledarEntries(int days, int page)
		{
			Authenticate();
			return new CalendarBLL().ListSpansCaledarEntries(days, page);
		}

		[WebMethod(EnableSession = true)]
		public bool RosterVolunteer(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new CalendarBLL().RosterVolunteer(calendarId, memberId, rosteringWeek, rosteringDay);
		}

		[WebMethod(EnableSession = true)]
		public bool RemoveRotaSlot(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new CalendarBLL().RemoveRotaSlot(calendarId, memberId, rosteringWeek, rosteringDay);
		}

		[WebMethod(EnableSession = true)]
		public List<RosteredVolunteer> ListRosteredVolunteers(int calendarId)
		{
			Authenticate();
			return new CalendarBLL().ListRosteredVolunteers(calendarId);
		}

		[WebMethod]
		public void GenerateCalendar()
		{
			new CalendarBLL().GenerateCalendar();
		}

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembersOnBloodShift()
		{
			return ListMembersOnShift(1);
		}

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembersOnShift(int calendarId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new CalendarBLL().ListMembersOnShift(calendarId);
		}

		[WebMethod(EnableSession = true)]
		public bool MarkShiftSwapNeeded(int calendarId, int memberId, DateTime shiftDate)
		{
			Authenticate();
			if (CurrentUser().MemberID != memberId && CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new CalendarBLL().MarkShiftSwapNeeded(calendarId, memberId, shiftDate);
		}

		[WebMethod(EnableSession = true)]
		public bool AddVolunteerToCalendar(int calendarId, int memberId, DateTime shiftDate)
		{
			Authenticate();
			if (CurrentUser().MemberID != memberId && CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new CalendarBLL().AddVolunteerToCalendar(calendarId, memberId, shiftDate, memberId == CurrentUser().MemberID);
		}

		[WebMethod(EnableSession = true)]
		public CalendarEntry GetNextShift()
		{
			Authenticate();
			return new CalendarBLL().GetMemberNextShift(CurrentUser().MemberID);
		}

		[WebMethod]
		public List<string> GetCurrentWeekADateStrings(string format)
		{
			return new CalendarBLL().GetCurrentWeekADateStrings(format);
		}

		[WebMethod]
		public List<string> GetCurrentWeekBDateStrings(string format)
		{
			return new CalendarBLL().GetCurrentWeekBDateStrings(format);
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<SERVDataContract.Calendar> ListCalendars()
		{
			Authenticate();
			return new CalendarBLL().ListCalendars();
		}

		[WebMethod(EnableSession = true)]
		public SERVDataContract.Calendar GetCalendar(int calendarId)
		{
			Authenticate();
			return new CalendarBLL().Get(calendarId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveCalendarProps(int calendarId, string calendarName, int sortOrder, int requiredTagId, int defaultRequirement)
		{
			Authenticate();
			if (CurrentUser().UserLevelID < (int)UserLevel.Committee)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return new CalendarBLL().SaveCalendarProps(calendarId, calendarName, sortOrder, requiredTagId, defaultRequirement);
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<string> GetNextXDaysCalendarBulletins(int days)
		{
			Authenticate();
			return new CalendarBLL().GetNextXDaysCalendarBulletins(days);
		}

		[WebMethod]
		public bool SendCalendarDayBulletinsNotification()
		{
			new CalendarBLL().SendCalendarDayBulletinsNotification();
			return true;
		}

    }
}

