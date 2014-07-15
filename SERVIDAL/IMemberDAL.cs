using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface IMemberDAL : IDisposable
	{
		Member Get(int memberId);
		Member GetByEmail(string email);
		Member GetByUserID(int userID);
		int Create(Member member);
		int Save(Member member);
		int Update(Member member);
		List<Member> List(string search, bool onlyActive=true);
		List<Tag> ListMemberTags(int memberId);
		User Login(string username, string passwordHash);
		User GetUserForMember(int memberId);
		int GetUserIdForMember(int memberId);
		void AddMemberTag(int memberId, string tagName);
		void RemoveMemberTag(int memberId, string tagName);
		void SetPasswordHash(string username, string passwordHash);
		void SetUserLastLoginDate(SERVDataContract.DbLinq.User u);
		List<string> ListMobileNumbersWithTags(string tagsCsv);
		List<Member> ListMembersWithTags(string tagsCsv);
		List<Member> ListAdministrators();
		void SetMemberUserLevel(int memberId, int userLevelId);
	}
}
