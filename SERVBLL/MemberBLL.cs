using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;

namespace SERVBLL
{

	public class MemberBLL : IMemberBLL
	{

		public Member Get(int memberId)
		{
			return SERVDALFactory.Factory.MemberDAL().Get(memberId);
		}

		public int Create(Member member)
		{
			return SERVDALFactory.Factory.MemberDAL().Create(member);
		}	

		public List<Member> List(string search)
		{
			List<Member> ret = SERVDALFactory.Factory.MemberDAL().List(search);
			foreach (Member m in ret)
			{
				m.Tags = ListMemberTags(m.ID);
			}
			return ret;
		}

		List<Tag> ListMemberTags(int memberId)
		{
			return SERVDALFactory.Factory.MemberDAL().ListMemberTags(memberId);
		}
		
		public SERVUser Login(string username, string passwordHash)
		{
			SERVUser u = SERVDALFactory.Factory.MemberDAL().Login(username, passwordHash);
			if (u != null)
			{
				u.Member = Get(u.MemberId);
			}
			return u;
		}
		
	}

}


