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

		public Member Get(int newclassID)
		{
			return SERVDALFactory.Factory.MemberDAL().Get(newclassID);
		}

		public int Create(Member newclass)
		{
			return SERVDALFactory.Factory.MemberDAL().Create(newclass);
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
	}

}


