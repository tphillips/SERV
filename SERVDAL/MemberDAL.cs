using System;
using System.Collections.Generic;
using System.Text;
using SERVIDAL;
using SERVDataContract.DbLinq;
using System.Data;
using System.Data.Common;
using SERV.Utils.Data;
using System.Linq;

namespace SERVDAL
{

	public class MemberDAL : IMemberDAL
	{

		static SERVDataContract.DbLinq.SERVDB db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);

		public Member Get(int memberId)
		{
			SERVDataContract.DbLinq.Member mem = (from m in db.Member where m.MemberID == memberId select m).FirstOrDefault();
			return mem;
		}
		
		public int Create(Member member)
		{
			//db.Member.InsertOnSubmit(member);
			//db.SubmitChanges();
			throw new NotImplementedException();
		}

		public int Save(Member newclass)
		{
			throw new NotImplementedException();
		}

		public int Update(Member member)
		{
			db.SubmitChanges();
			return member.MemberID;
		}

		public List<Member> List(string search)
		{
			throw new NotImplementedException();
		}

		public List<Tag> ListMemberTags(int memberId)
		{
			throw new NotImplementedException();
		}

		public User Login(string username, string passwordHash)
		{
			SERVDataContract.DbLinq.User ret = (from u in db.User where u.Member.EmailAddress == username && u.Member.MobileNumber == passwordHash select u).FirstOrDefault();
			return ret;
		}

	}

}
