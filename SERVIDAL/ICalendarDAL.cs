using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface ICalendarDAL : IDisposable
	{
		int Create(SERVDataContract.DbLinq.Calendar c);
		bool SaveCalendarProps(int calendarId, string calendarName, int sortOrder, int requiredTagId, int defaultRequirement);
		List<SERVDataContract.DbLinq.Calendar> ListCalendars();
		List<SERVDataContract.DbLinq.CalendarEntry> ListCalendarEntries(DateTime date);
		List<SERVDataContract.DbLinq.CalendarEntry> ListCalendarEntries(DateTime startDate, DateTime endDate);
		int CreateCalendarEntry(int calendarID, int memberID, DateTime date, bool adHoc);
		void RemoveCalendarEntry(int calendarEntryID);
		void SetCalendarLastGenerateDate(DateTime upTo);
		SERVDataContract.DbLinq.CalendarEntry GetCalendarEntry(DateTime date, int calendarId, int memberId, int adHoc);
		SERVDataContract.DbLinq.CalendarEntry GetCalendarEntry(DateTime date, int calendarId, int memberId);
		SERVDataContract.DbLinq.CalendarEntry GetCalendarEntry(int calendarEntryId);
		SERVDataContract.DbLinq.CalendarEntry GetMemberNextShift(int memberID);
		SERVDataContract.DbLinq.Calendar Get(int calendarId);
		void RosterVolunteer(int calendarId, int memberId, string rosteringWeek, int rosteringDay);
		void RemoveRotaSlot(int calendarId, int memberId, string rosteringWeek, int rosteringDay);
		List<MemberCalendar> ListRosteredVolunteers();
		List<MemberCalendar> ListRosteredVolunteers(int calendarId);
		List<MemberCalendar> ListRosteredVolunteers(string week, int day);
		bool MarkShiftSwapNeeded(int calendarId, int memberId, DateTime shiftDate);
		bool MarkShiftSwapNoLongerNeeded(int calendarEntryID);
	}
}

