using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface IMemberDAL
	{

		Member Get(int memberId);
		int Create(Member member);
		int Save(Member member);
		int Update(Member member);
		List<Member> List(string search);
		List<Tag> ListMemberTags(int memberId);
		User Login(string username, string passwordHash);

	}
}
