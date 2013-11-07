using System;

namespace SERVDataContract.DbLinq
{
	public class SERVUser
	{
		public int ID { get; set; }
		public int MemberId { get; set; }
		public Member Member { get; set; }
		public int UserLevel { get; set; }
		public DateTime? LastLoginDate { get; set; }
	}
}

