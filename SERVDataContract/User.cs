using System;
using System.Collections.Generic;
using SERV.Utils.Data;
using System.Text;
using System.Runtime.Serialization;

namespace SERVDataContract
{

	[DataContract]
	[Serializable]
    public class User
    {
		[DataMember]
		[DbMember(MaxLength=0)]
		public int ID { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public int MemberID { get; set; }

		[DataMember]
		[DbMember(MaxLength=45)]
		public string PasswordHash { get; set; }

		[DataMember]
		[DbMember(MaxLength=0)]
		public DateTime? LastLoginDate { get; set; }

    }
}

