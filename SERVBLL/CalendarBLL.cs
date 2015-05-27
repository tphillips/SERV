using System;
using System.Collections.Generic;
using System.Text;
using SERV.Utils;
using SERVDALFactory;
using SERVIDAL;
using SERVDataContract;
using SERVIBLL;
using System.Linq;
using System.Threading;

namespace SERVBLL
{
	public class CalendarBLL : ICalendarBLL
	{
		const int GENERATE_CALENDAR_DAYS = 182;
		const int SHIFT_THRESHOLD_HOUR = 6;

		const int BLOOD_CALENDAR_ID = 1;
		const int DAY_CONTROL_CALENDAR_ID = 3;
		const int NIGHT_CONTROL_CALENDAR_ID = 4;

		static DateTime WEEK_A_START = new DateTime(2013, 12, 30);
		static Logger log = new Logger();

		public CalendarBLL()
		{
		}

		public List<Calendar> ListCalendars()
		{
			List<Calendar> ret = new List<Calendar>();
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.Calendar> cals = dal.ListCalendars();
				foreach (SERVDataContract.DbLinq.Calendar c in cals)
				{
					ret.Add(new Calendar(c));
				}
				return ret;
			}
		}

		public int Create(Calendar cal, User user)
		{
			log.LogStart();
			SERVDataContract.DbLinq.Calendar c = new SERVDataContract.DbLinq.Calendar();
			UpdatePolicyAttribute.MapPropertiesWithUpdatePolicy(cal, c, user, false);
			return SERVDALFactory.Factory.CalendarDAL().Create(c);
		}

		public Calendar Get(int calendarId)
		{
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				SERVDataContract.DbLinq.Calendar lret = dal.Get(calendarId);
				Calendar ret = new Calendar(lret);
				return ret;
			}
		}

		public bool SaveCalendarProps(int calendarId, string calendarName, int sortOrder, int requiredTagId, int defaultRequirement)
		{
			return SERVDALFactory.Factory.CalendarDAL().SaveCalendarProps(calendarId, calendarName, sortOrder, requiredTagId, defaultRequirement);
		}

		public bool RosterVolunteer(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			SERVDALFactory.Factory.CalendarDAL().RosterVolunteer(calendarId, memberId, rosteringWeek, rosteringDay);
			return true;
		}

		public bool RemoveRotaSlot(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			SERVDALFactory.Factory.CalendarDAL().RemoveRotaSlot(calendarId, memberId, rosteringWeek, rosteringDay);
			return true;
		}

		public List<RosteredVolunteer> ListRosteredVolunteers(int calendarId)
		{
			List<RosteredVolunteer> ret = new List<RosteredVolunteer>();
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.MemberCalendar> lret = dal.ListRosteredVolunteers(calendarId);
				foreach (SERVDataContract.DbLinq.MemberCalendar mc in lret)
				{
					ret.Add(new RosteredVolunteer(mc));
				}
				return ret;
			}
		}

		public List<RosteredVolunteer> ListRosteredVolunteers(string week, int day)
		{
			List<RosteredVolunteer> ret = new List<RosteredVolunteer>();
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.MemberCalendar> lret = dal.ListRosteredVolunteers(week, day);
				foreach (SERVDataContract.DbLinq.MemberCalendar mc in lret)
				{
					ret.Add(new RosteredVolunteer(mc));
				}
				return ret;
			}
		}

		public List<Member> ListMembersOnBloodShift()
		{
			return ListMembersOnShift(BLOOD_CALENDAR_ID);
		}

		public Member GetCurrentDayController()
		{
			List<Member> l = ListMembersOnShift(DAY_CONTROL_CALENDAR_ID);
			if (l.Count > 0)
			{
				return l[0];
			}
			return null;
		}

		public Member GetCurrentNightController()
		{
			List<Member> l = ListMembersOnShift(NIGHT_CONTROL_CALENDAR_ID);
			if (l.Count > 0)
			{
				return l[0];
			}
			return null;
		}

		public List<Member> ListMembersOnShift(int calendarId)
		{
			List<Member> ret = new List<Member>();
			DateTime shiftDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			if (DateTime.Now.Hour < SHIFT_THRESHOLD_HOUR){ shiftDate = shiftDate.AddDays(-1); }
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.Member> lret = dal.ListMembersOnShift(calendarId, shiftDate);
				foreach (SERVDataContract.DbLinq.Member m in lret)
				{
					ret.Add(new Member(m));
				}
				return ret;
			}
		}

		public List<RosteredVolunteer> ListRosteredVolunteers()
		{
			List<RosteredVolunteer> ret = new List<RosteredVolunteer>();
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.MemberCalendar> lret = dal.ListRosteredVolunteers();
				foreach (SERVDataContract.DbLinq.MemberCalendar mc in lret)
				{
					ret.Add(new RosteredVolunteer(mc));
				}
				return ret;
			}
		}

		public CalendarEntry GetCalendarEntry(DateTime date, int calendarId, int memberId, bool adhoc)
		{
			DateTime cleanDate = new DateTime(date.Year, date.Month, date.Day);
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				SERVDataContract.DbLinq.CalendarEntry entry = dal.GetCalendarEntry(cleanDate, calendarId, memberId, adhoc ? 1 : 0);
				if (entry == null)
				{
					return null;
				}
				return new CalendarEntry(entry);
			}
		}

		public CalendarEntry GetCalendarEntry(int calendarEntryId)
		{
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				SERVDataContract.DbLinq.CalendarEntry entry = dal.GetCalendarEntry(calendarEntryId);
				if (entry == null)
				{
					return null;
				}
				return new CalendarEntry(entry);
			}
		}

		public CalendarEntry GetCalendarEntry(DateTime date, int calendarId, int memberId)
		{
			DateTime cleanDate = new DateTime(date.Year, date.Month, date.Day);
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				SERVDataContract.DbLinq.CalendarEntry entry = dal.GetCalendarEntry(cleanDate, calendarId, memberId);
				if (entry == null)
				{
					return null;
				}
				return new CalendarEntry(entry);
			}
		}

		public List<CalendarEntry> ListCalendarEntries(DateTime date)
		{
			List<CalendarEntry> ret = new List<CalendarEntry>();
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.CalendarEntry> lret = dal.ListCalendarEntries(date);
				foreach (SERVDataContract.DbLinq.CalendarEntry e in lret)
				{
					ret.Add(new CalendarEntry(e));
				}
				return ret;
			}
		}

		public List<CalendarEntry> ListCalendarEntries(DateTime date, int days)
		{
			List<CalendarEntry> ret = new List<CalendarEntry>();
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				List<SERVDataContract.DbLinq.CalendarEntry> lret = dal.ListCalendarEntries(date, date.AddDays(days));
				foreach (SERVDataContract.DbLinq.CalendarEntry e in lret)
				{
					ret.Add(new CalendarEntry(e));
				}
				return ret;
			}
		}

		public List<List<CalendarEntry>> ListWeeksCaledarEntries()
		{
			return ListWeeksCaledarEntries(DateTime.Now);
		}

		public List<List<CalendarEntry>> ListWeeksCaledarEntries(string dateToParse)
		{
			DateTime parsed = DateTime.Parse(dateToParse);
			return ListWeeksCaledarEntries(parsed);
		}

		public List<List<CalendarEntry>> ListWeeksCaledarEntries(DateTime dayInWeek)
		{
			return ListSpansCaledarEntries(dayInWeek, 7);
		}

		public List<List<CalendarEntry>> ListSpansCaledarEntries(int days)
		{
			return ListSpansCaledarEntries(DateTime.Now, days);
		}

		public List<List<CalendarEntry>> ListSpansCaledarEntries(int days, int page)
		{
			return ListSpansCaledarEntries(DateTime.Now.AddDays(page * days), days);
		}

		public List<List<CalendarEntry>> ListSpansCaledarEntries(string dateToParse, int days)
		{
			DateTime parsed = DateTime.Parse(dateToParse);
			return ListSpansCaledarEntries(parsed, days);
		}

		public List<List<CalendarEntry>> ListSpansCaledarEntries(DateTime dayInWeek, int days)
		{
			DateTime clean = new DateTime(dayInWeek.Year, dayInWeek.Month, dayInWeek.Day);
			List<List<CalendarEntry>> ret = new List<List<CalendarEntry>>();
			DateTime curDay = FindMondayStart(clean);
			List<CalendarEntry> all = ListCalendarEntries(curDay, days);
			for (int x = 0; x < days; x++)
			{	
				List<CalendarEntry> toAdd = (from ce in all
				                             where ce.EntryDate == curDay.AddDays(x)
											orderby ce.CalendarSortOrder, ce.CoverNeeded descending
				                             select ce).ToList();
				if (toAdd.Count == 0)
				{
					toAdd.Add(new CalendarEntry() { EntryDate = curDay.AddDays(x), MemberName = "Nada, zero, not a sausage." });
				}
				ret.Add(toAdd);
			}
			return ret;
		}

		public List<string> GetCalendarDayBulletins(DateTime date)
		{
			return GetCalendarDayBulletins(date, null);
		}

		public List<string> GetNextXDaysCalendarBulletins(int days)
		{
			return GetCalendarDayBulletins(DateTime.Now, days);
		}

		public List<string> GetNext5DaysCalendarBulletins()
		{
			return GetCalendarDayBulletins(DateTime.Now, 5);
		}

		public List<string> GetNext7DaysCalendarBulletins()
		{
			return GetCalendarDayBulletins(DateTime.Now, 7);
		}

		public List<string> GetCalendarDayBulletins(DateTime date, int days)
		{
			List<Calendar> calendars = ListCalendars();
			List<string> ret = new List<string>();
			for(int x = 0; x < days; x++)
			{
				ret.AddRange(GetCalendarDayBulletins(date.AddDays(x), calendars));
			}
			return ret;
		}

		public List<string> GetCalendarDayBulletins(DateTime date, List<Calendar> calendars)
		{
			List<string> ret = new List<string>();
			DateTime clean = new DateTime(date.Year, date.Month, date.Day);
			List<CalendarEntry> entries = ListCalendarEntries(clean);
			if (calendars == null) { calendars = ListCalendars(); }
			string dateDesc = string.Format("on {0:ddd dd MMM}", clean);
			if (clean == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
			{
				dateDesc = "today!";
			}
			foreach (Calendar c in calendars)
			{
				int volunteers = (from ce in entries
				                  where ce.CalendarID == c.CalendarID && ce.CoverNeeded == false
				                  select ce).Count();
				if (volunteers < c.DefaultRequirement)
				{
					if (volunteers == 0)
					{
						ret.Add(string.Format("There is no {0} cover {1}", c.Name, dateDesc));
					}
					else
					{
						ret.Add(string.Format("There is only {0} on {1} cover {2}", volunteers, c.Name, dateDesc));
					}
				}
			}
			return ret;
		}

		public void SendCalendarDayBulletinsNotification()
		{
			List<string> notifications = GetNextXDaysCalendarBulletins(3);
			if (notifications != null)
			{
				foreach (string n in notifications)
				{
					new MessageBLL().PushBullet("servssl_calendar", "Calendar Bulletin", n);
				}
			}
		}

		public CalendarEntry GetMemberNextShift(int memberID)
		{
			using (ICalendarDAL dal = SERVDALFactory.Factory.CalendarDAL())
			{
				SERVDataContract.DbLinq.CalendarEntry lret = dal.GetMemberNextShift(memberID);
				if (lret != null)
				{
					return new CalendarEntry(lret);
				}
				return null;
			}
		}

		public int CreateCalendarEntry(int calendarID, int memberID, DateTime date)
		{
			return CreateCalendarEntry(calendarID, memberID, date, false);
		}

		public int CreateCalendarEntry(int calendarID, int memberID, DateTime date, bool adHoc)
		{
			DateTime cleanDate = new DateTime(date.Year, date.Month, date.Day);
			CalendarEntry e = GetCalendarEntry(cleanDate, calendarID, memberID);
			if (e != null)
			{
				MarkShiftSwapNoLongerNeeded(e.CalendarEntryID);
				return e.CalendarEntryID;
			}
			int ret = SERVDALFactory.Factory.CalendarDAL().CreateCalendarEntry(calendarID, memberID, cleanDate, adHoc);
			return ret;
		}

		void MarkShiftSwapNoLongerNeeded(int calendarEntryID)
		{
			SERVDALFactory.Factory.CalendarDAL().MarkShiftSwapNoLongerNeeded(calendarEntryID);
		}

		public bool MarkShiftSwapNeeded(int calendarId, int memberId, DateTime shiftDate)
		{
			bool ret = SERVDALFactory.Factory.CalendarDAL().MarkShiftSwapNeeded(calendarId, memberId, shiftDate);
			if (ret)
			{
				new MessageBLL().SendShiftSwapNeededEmail(memberId, calendarId, shiftDate);
				new MessageBLL().SendShiftSwapNeededPushNotification(memberId, calendarId, shiftDate);
			}
			return true;
		}

		public bool AddVolunteerToCalendar(int calendarId, int memberId, DateTime shiftDate, bool memberIsMember)
		{
			int ret = CreateCalendarEntry(calendarId, memberId, shiftDate, true);
			if (ret > 0)
			{
				if (memberIsMember)
				{
					new MessageBLL().SendCalendarVolunteeringThanksEmail(memberId, ret);
				}
				else
				{
					new MessageBLL().SendCalendarVolunteerNotificationEmail(memberId, ret);
				}
			}
			return ret > 0;
		}

		public void RemoveCalendarEntry(int calendarEntryID)
		{
			SERVDALFactory.Factory.CalendarDAL().RemoveCalendarEntry(calendarEntryID);
		}
			
		public void GenerateCalendar()
		{
			Thread t = new Thread(new ThreadStart(_GenerateCalendar));
			t.IsBackground = true;
			t.Start();
		}

		/// <summary>
		/// Generates ALL calendars from the MemberCalendar table. Checks existing entries and removes if needed
		/// Ad-Hoc entries will not be removed
		/// The generation code can be run at any time, and processes from today onwards for GENERATE_CALENDAR_DAYS days.
		/// </summary>
		/// <returns><c>true</c>, if calendar was generated, <c>false</c> otherwise.</returns>
		public void _GenerateCalendar()
		{
			DateTime start = log.LogStart();
			DateTime curDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			List<RosteredVolunteer> allScheduledSlots = ListRosteredVolunteers(); // cache scheduled slots
			List<CalendarEntry> allUpcomingCalendarEntries = ListCalendarEntries(curDay, GENERATE_CALENDAR_DAYS + 1); // cache calendar entries
			// Work on each day individually from today for GENERATE_CALENDAR_DAYS days
			for (int x = 0; x < GENERATE_CALENDAR_DAYS; x++)
			{
				log.Debug("Processing calendar day " + x);
				string week = IsWeekA(curDay) ? "A" : "B";
				int day = (int)curDay.DayOfWeek;
				if (day == 0){ day = 7; }
				// find rostered slots and make sure they exist
				List<RosteredVolunteer> scheduledSlots = (from rv in allScheduledSlots
				                                          where rv.Week == week.ToCharArray()[0] && rv.DayNo == day
				                                          select rv).ToList();
				foreach (RosteredVolunteer rv in scheduledSlots)
				{
					// See if it exists, if not create it
					CalendarEntry e = (from ce in allUpcomingCalendarEntries
					                   where ce.EntryDate == curDay && ce.CalendarID == rv.CalendarID && ce.MemberID == rv.MemberID && ce.AdHoc == false
					                   select ce).FirstOrDefault();
					if (e == null)
					{
						CreateCalendarEntry(rv.CalendarID, rv.MemberID, curDay);
					}
					//Thread.Sleep(100); if (x > 14){ Thread.Sleep(200); }
				}
				// Check calendar to make sure there are not schedules that should not be there (after removing a rostered slot for example, make sure its not an ad-hoc)
				foreach (CalendarEntry e in (from ce in allUpcomingCalendarEntries where ce.EntryDate == curDay select ce).ToList())
				{
					if (!e.AdHoc && !e.ManuallyAdded) // Ignore adhocs
					{
						// Make sure this calendar entry(e) is supposed to exist, does that member have a scheduled slot for this calendar
						bool ok = (from rv in scheduledSlots
							where rv.MemberID == e.MemberID && rv.CalendarID == e.CalendarID
						           select e).Count() > 0;
						if (!ok)
						{
							RemoveCalendarEntry(e.CalendarEntryID);
						}
					}
					//Thread.Sleep(100); if (x > 14){ Thread.Sleep(200); }
				}
				// move on
				curDay = curDay.AddDays(1);
				//Thread.Sleep(100); if (x > 14){ Thread.Sleep(200); }
			}
			SetCalendarLastGenerateDate(DateTime.Now, curDay);
			new MessageBLL().PushBullet("servssl_calendar", "Calendars Generated", "The calendars have been refreshed!");
			GC.Collect();
			log.LogEnd();
			log.LogPerformace(start);
		}

		void SetCalendarLastGenerateDate(DateTime now, DateTime curDay)
		{
			SERVDALFactory.Factory.CalendarDAL().SetCalendarLastGenerateDate(curDay);
		}

		public DateTime GetCurrentWeekAStartDate()
		{
			if (IsWeekA(DateTime.Now))
			{
				return FindMondayStart(DateTime.Now);
			}
			else
			{
				return FindNextMonday(DateTime.Now);
			}
		}

		public DateTime GetCurrentWeekBStartDate()
		{
			if (IsWeekA(DateTime.Now))
			{
				return FindNextMonday(DateTime.Now);
			}
			else
			{
				return FindMondayStart(DateTime.Now);
			}
		}

		public bool IsWeekA(DateTime date)
		{
			DateTime d = FindMondayStart(date);
			int weeks = ((int)((TimeSpan) (d - WEEK_A_START)).TotalDays) / 7;
			return weeks % 2 == 0;
		}

		public DateTime FindMondayStart(DateTime date)
		{
			DateTime d = date;
			while (d.DayOfWeek != DayOfWeek.Monday)
			{
				d = d.AddDays(-1);
			}
			return d;
		}

		public DateTime FindNextMonday(DateTime date)
		{
			DateTime d = date;
			if (d.DayOfWeek == DayOfWeek.Monday)
			{
				return d.AddDays(7);
			}
			while (d.DayOfWeek != DayOfWeek.Monday)
			{
				d = d.AddDays(1);
			}
			return d;
		}

		public List<string> GetCurrentWeekADateStrings(string format)
		{
			DateTime weekStart = GetCurrentWeekAStartDate();
			return GetWeekDays(weekStart, format);
		}

		public List<string> GetCurrentWeekBDateStrings(string format)
		{
			DateTime weekStart = GetCurrentWeekBStartDate();
			return GetWeekDays(weekStart, format);
		}

		public List<string> GetWeekDays(DateTime weekStart, string format)
		{
			List<string> ret = new List<string>();
			for (int x = 0; x < 7; x++)
			{
				ret.Add(weekStart.AddDays(x).ToString(format));
			}
			return ret;
		}
	}
}

