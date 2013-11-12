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

		static Logger log = new Logger();

		public Member Get(int memberId)
		{
			try
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
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				return null;
			}
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
			try
			{
				using (SERVIDAL.IMemberDAL dal = SERVDALFactory.Factory.MemberDAL())
				{
					SERVDataContract.DbLinq.Member m = dal.Get(member.MemberID);
					UpdatePolicyAttribute.MapPropertiesWithUpdatePolicy(member, m, user, user.MemberID == member.MemberID);
					return dal.Update(m);
				}
			}
			catch(Exception ex)
			{
				log.Error(ex.Message, ex);
				return -1;
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
			using (SERVIDAL.IMemberDAL dal = SERVDALFactory.Factory.MemberDAL())
			{
				SERVDataContract.DbLinq.User u = dal.Login(username, passwordHash);
				if (u == null)
				{
					return null;
				}
				dal.SetUserLastLoginDate(u);
				return new User(u);
			}
		}

		public void SetPassword(string username, string passwordHash)
		{
			SERVDALFactory.Factory.MemberDAL().SetPasswordHash(username, passwordHash);
		}

		public List<string> ListMobileNumbersWithTags(string tagsCsv)
		{
			List<string> ret = new List<string>();
			List<string> res = SERVDALFactory.Factory.MemberDAL().ListMobileNumbersWithTags(tagsCsv);
			foreach (string m in res)
			{
				string num = m.Replace(" ", "");
				if (num.Length > 5)
				{
					if (num.StartsWith("07"))
					{
						num = "44" + num.Substring(1, num.Length - 1);
						ret.Add(num);
					}
				}
			}
			return ret;
		}

	}

}


