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
			PropertyMapper.MapProperties(metal, this);
		}

		[DataMember]
		[UpdatePolicy(ReadOnly = true)]
		public int MemberID { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string FirstName { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string LastName { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false)]
		public DateTime JoinDate { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string EmailAddress { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string MobileNumber { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string HomeNumber { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string Occupation { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false)]
		public int MemberTypeID { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false)]
		public int MemberStatusID { get; set; }

		[DataMember]
		[UpdatePolicy]
		public int? AvailabilityID { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? RiderAssesmentPassDate { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? AdQualPassDate { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public string AdQualType { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string BikeType { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string CarType { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public string Notes { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string Address1 { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string Address2 { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string Address3 { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string Town { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string County { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string PostCode { get; set; }

		[DataMember]
		[UpdatePolicy]
		public int BirthYear { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string NextOfKin { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string NextOfKinAddress { get; set; }

		[DataMember]
		[UpdatePolicy]
		public string NextOfKinPhone { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public bool LegalConfirmation { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? LeaveDate { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? LastGDPGMPDate { get; set; }

		[DataMember]
		public List<Tag> Tags { get; set; }

	}

}


