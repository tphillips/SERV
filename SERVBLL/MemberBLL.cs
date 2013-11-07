using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{

	public class MemberBLL : IMemberBLL
	{

		public Member Get(int memberId)
		{
			SERVDataContract.DbLinq.Member ret = SERVDALFactory.Factory.MemberDAL().Get(memberId);
			return new Member(ret);
		}

		public int Create(Member member)
		{
			SERVDataContract.DbLinq.Member m = new SERVDataContract.DbLinq.Member();
			PropertyMapper.MapProperties(member, m, false, false);
			return SERVDALFactory.Factory.MemberDAL().Create(m);
		}	
	
		public int Save(Member member)
		{
			SERVIDAL.IMemberDAL dal = SERVDALFactory.Factory.MemberDAL();
			SERVDataContract.DbLinq.Member m = dal.Get(member.MemberID);
			PropertyMapper.MapProperties(member, m, true, true);
			return dal.Update(m);
		}

		public List<Member> List(string search)
		{
			throw new NotImplementedException();
		}

		List<Tag> ListMemberTags(int memberId)
		{
			throw new NotImplementedException();
		}
		
		public User Login(string username, string passwordHash)
		{
			SERVDataContract.DbLinq.User u = SERVDALFactory.Factory.MemberDAL().Login(username, passwordHash);
			return new User(u);
		}
		
	}

}


