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
		
		public void AddMemberTag(int memberId, string tagName)
		{
			int tagId = Convert.ToInt32(DBHelperFactory.DBHelper().ExecuteScalar("select TagID from Tag where Tag = " + SERV.Utils.String.DBSafeString(tagName)));
			string sql = string.Format("insert into Member_Tag values ({0}, {1})", memberId, tagId);
			DBHelperFactory.DBHelper().ExecuteNonQuery(sql);
		}
		
		public void RemoveMemberTag(int memberId, string tagName)
		{
			string sql = string.Format("delete from Member_Tag where MemberID = {0} and TagID in (Select TagID from Tag where Tag = {1})", memberId, SERV.Utils.String.DBSafeString(tagName));
			DBHelperFactory.DBHelper().ExecuteNonQuery(sql);
		}

		public User Login(string username, string passwordHash)
		{
			SERVDataContract.DbLinq.User ret = (from u in db.User where u.Member.EmailAddress == username && u.Member.MobileNumber == passwordHash select u).FirstOrDefault();
			return ret;
		}

	}

}
