using System;
using System.Collections.Generic;
using System.Text;
using SERVIDAL;
using SERVDataContract.DbLinq;
using System.Data;
using System.Data.Common;
using SERV.Utils.Data;
using System.Linq;

namespace SERVDAL
{
	public class CalendarDAL : ICalendarDAL
	{

		static Logger log = new Logger();
		static SERVDataContract.DbLinq.SERVDB db;

		public CalendarDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public Calendar Get(int calendarId)
		{
			SERVDataContract.DbLinq.Calendar cal = (from c in db.Calendar where c.CalendarID == calendarId select c).FirstOrDefault();
			return cal;
		}

		public int CreateCalendarEntry(int calendarID, int memberID, DateTime date)
		{
			CalendarEntry e = new CalendarEntry()
			{
				CreateDateTime = DateTime.Now,
				CalendarID = calendarID,
				EntryDate = date,
				MemberID = memberID,
				CoverNeeded = 0,
				CoverCalendarEntryID = null,
				AdHoc = 0
			};
			db.CalendarEntry.InsertOnSubmit(e);
			db.SubmitChanges();
			return e.CalendarEntryID;
		}

		public List<CalendarEntry> ListCalendarEntries(DateTime date)
		{
			return (from e in db.CalendarEntry where e.EntryDate == date orderby e.CalendarID select e).ToList();
		}

		public List<CalendarEntry> ListCalendarEntries(DateTime startDate, DateTime endDate)
		{
			return (from e in db.CalendarEntry where e.EntryDate >= startDate && e.EntryDate <= endDate orderby e.CalendarID, e.EntryDate select e).ToList();
		}

		public void RemoveCalendarEntry(int calendarEntryID)
		{
			log.LogStart();
			string sql = string.Format("delete from CalendarEntry where CalendarEntryID = {0}", calendarEntryID);
			db.ExecuteCommand(sql);
		}

		public CalendarEntry GetCalendarEntry(DateTime date, int calendarId, int memberId, int adHoc)
		{
			return (from e in db.CalendarEntry where e.EntryDate == date && e.CalendarID == calendarId && e.MemberID == memberId && e.AdHoc == adHoc select e).FirstOrDefault();
		}
			
		public int Create(Calendar c)
		{
			log.LogStart();
			db.Calendar.InsertOnSubmit(c);
			db.SubmitChanges();
			return c.CalendarID;
		}

		public void RosterVolunteer(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			log.LogStart();
			db.MemberCalendar.InsertOnSubmit(new MemberCalendar(){ MemberID = memberId, CalendarID = calendarId, SetDayNo = rosteringDay, Week = rosteringWeek });
			db.SubmitChanges();
		}

		public void RemoveRotaSlot(int calendarId, int memberId, string rosteringWeek, int rosteringDay)
		{
			log.LogStart();
			string sql = string.Format("delete from Member_Calendar where MemberID = {0} and CalendarID = {1} and Week = '{2}' and SetDayNo = {3}", memberId, calendarId, rosteringWeek, rosteringDay);
			db.ExecuteCommand(sql);
		}

		public List<Calendar> ListCalendars()
		{
			return (from c in db.Calendar select c).ToList();
		}

		public List<MemberCalendar> ListRosteredVolunteers(int calendarId)
		{
			return (from mc in db.MemberCalendar
			        where mc.CalendarID == calendarId
			        orderby mc.Week, mc.SetDayNo
			        select mc).ToList();
		}

		public List<MemberCalendar> ListRosteredVolunteers(string week, int day)
		{
			return (from mc in db.MemberCalendar
				where mc.Week == week && mc.SetDayNo == day
				select mc).ToList();
		}

		public void SetCalendarLastGenerateDate(DateTime upTo)
		{
			log.LogStart();
			string sql = string.Format("update Calendar set LastGenerated = NOW(), GeneratedUpTo ='{0}'", upTo.ToString("yyyy-MM-dd"));
			db.ExecuteCommand(sql);
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}

