using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IMemberBLL
	{
        Member Get(int memberId);
		Member GetByEmail(string email);
        int Create(Member member);
		int Save(Member member, User user);
		List<Member> List(string search, bool onlyActive = true);
		User Login(string username, string passwordHash);
		User GetUserForMember(int memberId);
		void AddMemberTag(int memberId, string tagName);
		void RemoveMemberTag(int memberId, string tagName);
		void SetPassword(string username, string passwordHash);
		List<string> ListMobileNumbersWithTags(string tagsCsv);
		List<Member> ListMembersWithTags(string tagsCsv);
		void SendPasswordReset(string username);
		void SetMemberUserLevel(int memberId, int userLevelId);
	}
}
