using System;
using System.Collections.Generic;
using SERV.Utils.Data;
using System.Text;
using System.Runtime.Serialization;
using SERV.Utils;

namespace SERVDataContract
{
	[DataContract]
	[Serializable]
	public class CalendarEntry
	{

		public CalendarEntry(){}

		public CalendarEntry(SERVDataContract.DbLinq.CalendarEntry metal)
		{
			this.AdHoc = metal.AdHoc == 1;
			this.ManuallyAdded = metal.ManuallyAdded == 1;
			this.CalendarEntryID = metal.CalendarEntryID;
			this.CoverCalendarEntryID = metal.CoverCalendarEntryID;
			this.CoverNeeded = metal.CoverNeeded == 1;
			this.EntryDate = metal.EntryDate;
			this.MemberID = metal.MemberID;
			this.CalendarID = metal.CalendarID;
			this.CalendarName = metal.Calendar.Name;
			this.CalendarSortOrder = metal.Calendar.SortOrder != null? (int)metal.Calendar.SortOrder : 9999;
			this.MemberName = string.Format("{0} {1}", metal.Member.FirstName, metal.Member.LastName);
		}

		[DataMember]
		public int CalendarID { get; set; }

		[DataMember]
		public string CalendarName { get; set; }

		[DataMember]
		public int CalendarSortOrder {get;set;}

		[DataMember]
		public int CalendarEntryID { get; set; }

		[DataMember]
		public DateTime EntryDate { get; set; }

		public bool IsToday
		{
			get
			{
				return this.EntryDate == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			}
		}

		[DataMember]
		public string EntryDateShortString 
		{
			get
			{
				return EntryDate.ToString("dd MMM");
			}
		}

		[DataMember]
		public string EntryDateShortStringWithDay
		{
			get
			{
				return EntryDate.ToString("ddd dd MMM");
			}
		}

		[DataMember]
		public string EntryDateLongString
		{
			get
			{
				return EntryDate.ToString("ddd dd MMM yy");
			}
		}

		[DataMember]
		public string EntryDateClrString
		{
			get
			{
				return EntryDate.ToString("yyyy-MM-dd 00:00:00");
			}
		}

		[DataMember]
		public int MemberID { get; set; }

		[DataMember]
		public bool CoverNeeded { get; set; }

		[DataMember]
		public int? CoverCalendarEntryID { get; set; }

		[DataMember]
		public bool AdHoc { get; set; }

		[DataMember]
		public bool ManuallyAdded { get; set; }

		[DataMember]
		public string MemberName { get; set; }

	}
}

