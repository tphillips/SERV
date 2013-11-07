using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IMemberBLL
	{
        Member Get(int memberId);
        int Create(Member member);
		int Save(Member member);
        List<Member> List(string search);
		User Login(string username, string passwordHash);
	}
}
