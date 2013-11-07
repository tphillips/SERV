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
    public class Member
	{

		public Member(){}

		public Member(SERVDataContract.DbLinq.Member metal)
		{
			PropertyMapper.MapProperties(metal, this, false, false);
		}

		[DataMember]
		public int MemberID { get; set; }

		[DataMember]
		public string FirstName { get; set; }

		[DataMember]
		public string LastName { get; set; }

		[DataMember]
		public DateTime JoinDate { get; set; }

		[DataMember]
		public string EmailAddress { get; set; }

		[DataMember]
		public string MobileNumber { get; set; }

		[DataMember]
		public string HomeNumber { get; set; }

		[DataMember]
		public string Occupation { get; set; }

		[DataMember]
		public int MemberTypeID { get; set; }

		[DataMember]
		public int MemberStatusID { get; set; }

		[DataMember]
		public int? AvailabilityID { get; set; }

		[DataMember]
		public DateTime? RiderAssesmentPassDate { get; set; }

		[DataMember]
		public DateTime? AdQualPassDate { get; set; }

		[DataMember]
		public string AdQualType { get; set; }

		[DataMember]
		public string BikeType { get; set; }

		[DataMember]
		public string CarType { get; set; }

		[DataMember]
		public string Notes { get; set; }

		[DataMember]
		public string Address1 { get; set; }

		[DataMember]
		public string Address2 { get; set; }

		[DataMember]
		public string Address3 { get; set; }

		[DataMember]
		public string Town { get; set; }

		[DataMember]
		public string County { get; set; }

		[DataMember]
		public string PostCode { get; set; }

		[DataMember]
		public int BirthYear { get; set; }

		[DataMember]
		public string NextOfKin { get; set; }

		[DataMember]
		public string NextOfKinAddress { get; set; }

		[DataMember]
		public string NextOfKinPhone { get; set; }

		[DataMember]
		public bool LegalConfirmation { get; set; }

		[DataMember]
		public DateTime? LeaveDate { get; set; }

		[DataMember]
		public DateTime? LastGDPGMPDate { get; set; }

		[DataMember]
		public List<Tag> Tags { get; set; }

	}

}


