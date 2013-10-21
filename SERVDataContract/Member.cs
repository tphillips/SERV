using System;
using System.Collections.Generic;
using SERV.Utils.Data;
using System.Text;
using System.Runtime.Serialization;

namespace SERVDataContract
{

	[DataContract]
	[Serializable]
    public class Member
	{

		[DataMember]
		[DbMember(MaxLength=0)]
		public int ID { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string FirstName { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string LastName { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public DateTime JoinDate { get; set; }

		[DataMember]
		[DbMember(MaxLength=60)]
		public string EmailAddress { get; set; }

		[DataMember]
		[DbMember(MaxLength=12)]
		public string MobileNumber { get; set; }

		[DataMember]
		[DbMember(MaxLength=12)]
		public string HomeNumber { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string Occupation { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public int MemberTypeID { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public int MemberStatusID { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public int? AvailabilityID { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public DateTime? RiderAssesmentPassDate { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public DateTime? AdQualPassDate { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public string AdQualType { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string BikeType { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string CarType { get; set; }

		[DataMember]
		[DbMember(MaxLength=400)]
		public string Notes { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string Address1 { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string Address2 { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string Address3 { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string Town { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string County { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string PostCode { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public int BirthYear { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public string NextOfKin { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public string NextOfKinAddress { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public string NextOfKinPhone { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public bool LegalConfirmation { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public DateTime? LeaveDate { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public DateTime? LastGDPGMPDate { get; set; }

		[DataMember]
		public List<Tag> Tags { get; set; }

	}

}


