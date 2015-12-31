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

		public enum RejectionReason
		{
			TooTired,
			BadRiderChoice,
			AlreadyBusy,
			MechanicalIssues,
			Weather,
			Other
		}

		static Logger log = new Logger();


		public void RejectOrder(int runLogId, RejectionReason reason)
		{
			// log the rejection, DONT clear the order memberid and raise an exception to the human controller.
		}

		public void LogRunPickup(int runLogId)
		{
		}

		public void LogRunHandover(int runLogId)
		{
			// Complete the run log
		}

		public void LogHomeSafe(int runLogId)
		{
		}

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

		public Member GetSystemController()
		{
			SERVDataContract.DbLinq.Member lret = SERVDALFactory.Factory.MemberDAL().GetSystemController();
			if (lret == null)
			{
				return null;
			}
			Member ret = new Member(lret);
			return ret;
		}

		public Member GetByEmail(string email)
		{
			try
			{
				SERVDataContract.DbLinq.Member lret = SERVDALFactory.Factory.MemberDAL().GetByEmail(email);
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

		public Member GetByUserID(int senderUserID)
		{
			try
			{
				SERVDataContract.DbLinq.Member lret = SERVDALFactory.Factory.MemberDAL().GetByUserID(senderUserID);
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
			m.JoinDate = DateTime.Now;
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

		public int Register(Member member)
		{
			Member m = GetByEmail(member.EmailAddress);
			if (m != null) { return -1; }
			member.MemberID = 0;
			int ret = Create(member);
			User u = GetUserForMember(ret);
			try
			{
				new MessageBLL().SendNewMemberEmail(member, u.UserID);
				SendPasswordReset(member.EmailAddress);
			}
			catch(Exception e)
			{
				log.Error(e.Message, e);
			}
			return ret;
		}

		public List<Member> List(string search, bool onlyActive = true)
		{
			List<Member> ret = new List<Member>();
			List<SERVDataContract.DbLinq.Member> members = SERVDALFactory.Factory.MemberDAL().List(search, onlyActive);
			foreach(SERVDataContract.DbLinq.Member m in members)
			{
				Member mem = new Member() 
				{ 
					MemberID = m.MemberID, 
					FirstName = m.FirstName, 
					LastName = m.LastName,
					EmailAddress = m.EmailAddress,
					MobileNumber = m.MobileNumber,
					Town = m.Town,
					PostCode = m.PostCode,
					UserLevelName = m.User[0].UserLevel.UserLevel1,
					LeaveDate = m.LeaveDate
				};
				mem.Tags = new List<Tag>();
				for(int x = 0; x < m.MemberTag.Count; x++)
				{
					mem.Tags.Add(new Tag(m.MemberTag[x].Tag));
				}
				ret.Add(mem);
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

		public User GetUserForMember(int memberId)
		{
			return new User(SERVDALFactory.Factory.MemberDAL().GetUserForMember(memberId));
		}

		public int GetUserIdForMember(int memberId)
		{
			return SERVDALFactory.Factory.MemberDAL().GetUserIdForMember(memberId);
		}

		public void SetPassword(string username, string passwordHash)
		{
			SERVDALFactory.Factory.MemberDAL().SetPasswordHash(username, passwordHash);
		}

		public void SendPasswordReset(string username)
		{
			Member m = GetByEmail(username);
			if (m == null) { return; }
			string newPass = m.MobileNumber.Replace(" ", "") + m.PostCode.Replace(" ", "").ToLower() + new Random().Next(9999);
			newPass = newPass.Replace(" ", "").ToLower();
			string passHash = SERV.Utils.Authentication.Hash(m.EmailAddress.ToLower().Trim() + newPass);
			new MessageBLL().SendPasswordResetEmail(m, newPass, GetUserForMember(m.MemberID).UserID);
			SetPassword(m.EmailAddress, passHash);
		}

		/// <summary>
		/// Each result must have at least one tag match
		/// </summary>
		/// <returns>The mobile numbers with tags.</returns>
		/// <param name="tagsCsv">Tags csv.</param>
		public List<string> ListMobileNumbersWithAnyTagsIn(string tagsCsv)
		{
			List<string> ret = new List<string>();
			List<string> res = SERVDALFactory.Factory.MemberDAL().ListMobileNumbersWithAnyTagsIn(tagsCsv);
			foreach (string m in res)
			{
				string num = m.Replace(" ", "");
				if (num.Length == 11)
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

		/// <summary>
		/// Each result must have all tags
		/// </summary>
		/// <returns>The mobile numbers with all tags.</returns>
		/// <param name="tagsCsv">Tags csv.</param>
		public List<string> ListMobileNumbersWithAllTags(string tagsCsv)
		{
			List<List<string>> seperateTagLists = new List<List<string>>();
			List<string> ret = new List<string>();
			string[] tags = tagsCsv.Split(',');
			foreach(string tag in tags)
			{
				if (tag.Trim() != "")
				{
					seperateTagLists.Add(SERVDALFactory.Factory.MemberDAL().ListMobileNumbersWithTag(tag.Trim()));
				}
			}
			// Primative intersection (I have a cold, there must be a better way . . .)
			if (seperateTagLists.Count > 0)
			{
				foreach (string number in seperateTagLists[0])
				{
					bool ok = true;
					foreach (List<string> l in seperateTagLists)
					{
						bool found = false;
						foreach (string s in l)
						{
							if (s.Trim() == number.Trim())
							{
								found = true;
							}
						}
						if (!found)
						{
							ok = false;
						}
					}
					if (ok)
					{
						ret.Add(number);
					}
				}
			}
			// EO Primative intersection
			List<string> res = new List<string>();
			foreach (string m in ret)
			{
				string num = m.Replace(" ", "");
				if (num.Length == 11)
				{
					if (num.StartsWith("07"))
					{
						num = "44" + num.Substring(1, num.Length - 1);
						res.Add(num);
					}
				}
			}
			return res;
		}

		public List<Member> ListMembersWithAnyTagsIn(string tagsCsv)
		{
			List<Member> ret = new List<Member>();
			List<SERVDataContract.DbLinq.Member> ms = SERVDALFactory.Factory.MemberDAL().ListMembersWithAnyTagsIn(tagsCsv);
			foreach (SERVDataContract.DbLinq.Member m in ms)
			{
				ret.Add(new Member() { MemberID = m.MemberID, FirstName = m.FirstName, LastName = m.LastName });
			}
			return ret;
		}

		public List<Member> ListAdministrators()
		{
			List<Member> ret = new List<Member>();
			List<SERVDataContract.DbLinq.Member> ms = SERVDALFactory.Factory.MemberDAL().ListAdministrators();
			foreach (SERVDataContract.DbLinq.Member m in ms)
			{
				ret.Add(new Member() { MemberID = m.MemberID, FirstName = m.FirstName, LastName = m.LastName, EmailAddress = m.EmailAddress});
			}
			return ret;
		}

		public void SetMemberUserLevel(int memberId, int userLevelId)
		{
			SERVDALFactory.Factory.MemberDAL().SetMemberUserLevel(memberId, userLevelId);
		}
	
		public void GoOnDuty(int memberId)
		{
			SERVDALFactory.Factory.MemberDAL().GoOnDuty(memberId);
		}

		public void GoOffDuty(int memberId)
		{
			SERVDALFactory.Factory.MemberDAL().GoOffDuty(memberId);
		}

		public void UpdateLocation(int memberId, string lat, string lng)
		{
			SERVDALFactory.Factory.MemberDAL().UpdateLocation(memberId, lat, lng);
		}

	}

}


