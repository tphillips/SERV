using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface ICalendarBLL
	{
		List<Calendar> ListCalendars();
		int Create(Calendar cal, User user);
		Calendar Get(int calendarId);
		bool RosterVolunteer(int calendarId, int memberId, string rosteringWeek, int rosteringDay);
		bool RemoveRotaSlot(int calendarId, int memberId, string rosteringWeek, int rosteringDay);
		List<RosteredVolunteer> ListRosteredVolunteers(int calendarId);
		void GenerateCalendar();
		List<List<CalendarEntry>> ListWeeksCaledarEntries(DateTime dayInWeek);
		List<List<CalendarEntry>> ListWeeksCaledarEntries();
		List<List<CalendarEntry>> ListWeeksCaledarEntries(string dateToParse);
		List<List<CalendarEntry>> ListSpansCaledarEntries(int days);
		List<List<CalendarEntry>> ListSpansCaledarEntries(string dateToParse, int days);
		List<List<CalendarEntry>> ListSpansCaledarEntries(DateTime dayInWeek, int days);
		void _GenerateCalendar();
		bool IsWeekA(DateTime date);
		DateTime GetCurrentWeekAStartDate();
		DateTime GetCurrentWeekBStartDate();
		DateTime FindNextMonday(DateTime date);
		DateTime FindMondayStart(DateTime date);
		List<string> GetCurrentWeekADateStrings(string format);
		List<string> GetCurrentWeekBDateStrings(string format);
		List<string> GetWeekDays(DateTime weekStart, string format);
	}
}

