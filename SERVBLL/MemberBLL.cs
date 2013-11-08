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
			SERVDataContract.DbLinq.Member lret = SERVDALFactory.Factory.MemberDAL().Get(memberId);
			Member ret = new Member(lret);
			ret.Tags = new List<Tag>();
			for(int x = 0; x < lret.MemberTag.Count; x++)
			{
				ret.Tags.Add(new Tag(lret.MemberTag[x].Tag));
			}
			return ret;
		}

		public int Create(Member member)
		{
			SERVDataContract.DbLinq.Member m = new SERVDataContract.DbLinq.Member();
			PropertyMapper.MapProperties(member, m);
			m.User.Add(new SERVDataContract.DbLinq.User(){ PasswordHash = "", UserLevelID =1});
			m.MemberStatusID = 1;
			return SERVDALFactory.Factory.MemberDAL().Create(m);
		}	
	
		public int Save(Member member, User user)
		{
			using (SERVIDAL.IMemberDAL dal = SERVDALFactory.Factory.MemberDAL())
			{
				SERVDataContract.DbLinq.Member m = dal.Get(member.MemberID);
				UpdatePolicyAttribute.MapPropertiesWithUpdatePolicy(member, m, user, user.MemberID == member.MemberID);
				return dal.Update(m);
			}
		}

		public List<Member> List(string search)
		{
			List<Member> ret = new List<Member>();
			List<SERVDataContract.DbLinq.Member> members = SERVDALFactory.Factory.MemberDAL().List(search);
			foreach(SERVDataContract.DbLinq.Member m in members)
			{
				ret.Add(new Member() 
				{ 
					MemberID = m.MemberID, 
					FirstName = m.FirstName, 
					LastName = m.LastName,
					EmailAddress = m.EmailAddress,
					MobileNumber = m.MobileNumber,
					Town = m.Town
				});
			}
			return ret;
		}

		List<Tag> ListMemberTags(int memberId)
		{
			throw new NotImplementedException();
		}
		
		public void AddMemberTag(int memberId, string tagName)
		{
			SERVDALFactory.Factory.MemberDAL().AddMemberTag(memberId, tagName);
		}

		public void RemoveMemberTag(int memberId, string tagName)
		{
			SERVDALFactory.Factory.MemberDAL().RemoveMemberTag(memberId, tagName);
		}

		public User Login(string username, string passwordHash)
		{
			SERVDataContract.DbLinq.User u = SERVDALFactory.Factory.MemberDAL().Login(username, passwordHash);
			if (u == null)
			{
				return null;
			}
			return new User(u);
		}

		public void SetPassword(string username, string passwordHash)
		{
			SERVDALFactory.Factory.MemberDAL().SetPasswordHash(username, passwordHash);
		}
		
	}

}


