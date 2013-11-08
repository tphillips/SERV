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
		int Save(Member member, User user);
        List<Member> List(string search);
		User Login(string username, string passwordHash);
		void AddMemberTag(int memberId, string tagName);
		void RemoveMemberTag(int memberId, string tagName);
		void SetPassword(string username, string passwordHash);
	}
}
