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
	public class Calendar
	{

		public Calendar(){}

		public Calendar(SERVDataContract.DbLinq.Calendar metal)
		{
			PropertyMapper.MapProperties(metal, this);
			this.SimpleCalendar = metal.SimpleCalendar == 1;
			this.VolunteerRemainsFree = metal.VolunteerRemainsFree == 1;
		}

		[DataMember]
		[UpdatePolicy(ReadOnly = true)]
		public int CalendarID { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public string Name { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public bool SimpleCalendar { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public int SimpleDaysIncrement { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public int SequentialDayCount { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public bool VolunteerRemainsFree { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public int RequiredTagID { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public int DefaultRequirement { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public DateTime? LastGenerated { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public DateTime? GeneratedUpTo { get; set; }

		[DataMember]
		[UpdatePolicy(MinRequiredLevel = UserLevel.Committee)]
		public int SortOrder { get; set; }

		[DataMember]
		public string LastGeneratedString 
		{
			get
			{	
				return LastGenerated == null ? " - " : ((DateTime)LastGenerated).ToString("dd MMM yyyy HH:mm:ss");
			}
		}

		[DataMember]
		public string GeneratedUpToString
		{
			get
			{	
				return GeneratedUpTo == null ? " - " : ((DateTime)GeneratedUpTo).ToString("dd MMM yyyy");
			}
		}


	}
}

