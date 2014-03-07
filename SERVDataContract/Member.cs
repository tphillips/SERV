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
		public string JoinDateString { 
			get { return JoinDate.ToString("dd MMM yyyy"); } 
			set 
			{
				DateTime val; 
				if (DateTime.TryParse(value, out val))
				{
					JoinDate = val;
				}
			}
		}

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
		//[UpdatePolicy(AllowOwner = false)]
		public int MemberStatusID { get; set; }

		[DataMember]
		//[UpdatePolicy]
		public int? AvailabilityID { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? RiderAssesmentPassDate { get; set; }
		[DataMember]
		public string RiderAssesmentPassDateString { 
			get { return RiderAssesmentPassDate != null ? ((DateTime)RiderAssesmentPassDate).ToString("dd MMM yyyy") : "-"; } 
			set 
			{
				DateTime val; 
				if (DateTime.TryParse(value, out val))
				{
					RiderAssesmentPassDate = val;
				}
			}
		}

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? AdQualPassDate { get; set; }
		public string AdQualPassDateString { 
			get { return AdQualPassDate != null ? ((DateTime)AdQualPassDate).ToString("dd MMM yyyy") : "-"; } 
			set 
			{
				DateTime val; 
				if (DateTime.TryParse(value, out val))
				{
					AdQualPassDate = val;
				}
			}
		}

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public string AdQualType { get; set; }

		[DataMember]
		//[UpdatePolicy]
		public string BikeType { get; set; }

		[DataMember]
		//[UpdatePolicy]
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
		//[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public bool LegalConfirmation { get; set; }

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? LeaveDate { get; set; }
		public string LeaveDateString { 
			get { return LeaveDate != null ? ((DateTime)LeaveDate).ToString("dd MMM yyyy") : "-"; } 
			set 
			{
				DateTime val; 
				if (DateTime.TryParse(value, out val))
				{
					LeaveDate = val;
				}
			}
		}

		[DataMember]
		[UpdatePolicy(AllowOwner = false, MinRequiredLevel = UserLevel.Committee)]
		public DateTime? LastGdpgmpdAte { get; set; }
		public string LastGDPGMPDateString { 
			get { return LastGdpgmpdAte != null ? ((DateTime)LastGdpgmpdAte).ToString("dd MMM yyyy") : "-"; } 
			set 
			{
				DateTime val; 
				if (DateTime.TryParse(value, out val))
				{
					LastGdpgmpdAte = val;
				}
			}
		}

		[DataMember]
		public List<Tag> Tags { get; set; }

		[DataMember]
		public string TagsText
		{
			get
			{
				string ret = "";
				if (Tags != null)
				{
					foreach (Tag t in Tags)
					{
						ret += t.TagName + " | ";
					}
					if (ret.Length > 2)
					{
						ret = ret.Substring(0, ret.Length - 2);
					}
				}
				return ret;
			}
		}

	}

}


