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
	public class User
    {

		public User(){}

		public User(SERVDataContract.DbLinq.User metal)
		{
			PropertyMapper.MapProperties(metal, this, false, false);
			this.Member = new Member();
			PropertyMapper.MapProperties(metal.Member, this.Member, false, false);
			this.Permissions = new PermissionSet();
		}

		public int UserID { get; set; }
		public int MemberID { get; set; }
		public Member Member { get; set; }
		public int UserLevel { get; set; }
		public DateTime? LastLoginDate { get; set; }
		public PermissionSet Permissions {get; set;}

    }
}

