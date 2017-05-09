using System;
using System.Runtime.Serialization;

namespace SERVDataContract
{

	[DataContract]
	[Serializable]
	public class RosteredVolunteer
	{

		public RosteredVolunteer()
		{
		}

		public RosteredVolunteer(SERVDataContract.DbLinq.MemberCalendar metal)
		{
			this.CalendarID = metal.CalendarID;
			this.DayNo = (int)metal.SetDayNo;
			this.MemberID = metal.MemberID;
			this.MemberName = string.Format("{0} {1}", metal.Member.FirstName, metal.Member.LastName);
			this.Week = metal.Week.ToCharArray()[0];
		}

		[DataMember]
		public int MemberID { get; set; }

		[DataMember]
		public int CalendarID { get; set; }

		[DataMember]
		public char Week { get; set; }

		[DataMember]
		public int DayNo { get; set; }

		[DataMember]
		public string MemberName { get; set; }

	}

}

