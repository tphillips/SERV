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
        List<Member> List(string search);
		SERVUser Login(string username, string passwordHash);
	}
}
