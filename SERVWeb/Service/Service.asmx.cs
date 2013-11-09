using System;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;
using SERVDataContract;
using SERVBLLFactory;


namespace SERVWeb
{

    public class Service : System.Web.Services.WebService
    {

		[WebMethod]
		public bool ImportRawRunLog()
		{
			System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(_ImportRawRunLog));
			t.IsBackground = true;
			t.Start();
			return true;
		}

		private void _ImportRawRunLog()
		{
			SERVBLLFactory.Factory.RunLogBLL().ImportRawRunLog();;
		}

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembers()
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().List("");
		}

		[WebMethod(EnableSession = true)]
		public Member GetMember(int memberId)
		{
			Authenticate();
			if (CurrentUser().UserLevelID <= (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MemberBLL().Get(memberId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveMember(Member member)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().Save(member, CurrentUser()) == member.MemberID;
		}

		public int CreateBlankMember(User user)
		{
			if (user == null || user.UserLevelID < (int)UserLevel.Admin)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			Member m = new Member();
			m.FirstName = "-";
			m.LastName = "-";
			m.EmailAddress = "@";
			m.MobileNumber = "07";
			return SERVBLLFactory.Factory.MemberBLL().Create(m);
		}

		[WebMethod(EnableSession = true)]
		public void TagMember(int memberId, string tagName)
		{
			Authenticate();
			if (CurrentUser().UserLevelID <= (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVBLLFactory.Factory.MemberBLL().AddMemberTag(memberId, tagName);
		}

		[WebMethod(EnableSession = true)]
		public void UnTagMember(int memberId, string tagName)
		{
			Authenticate();
			if (CurrentUser().UserLevelID <= (int)UserLevel.Committee && memberId != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			SERVBLLFactory.Factory.MemberBLL().RemoveMemberTag(memberId, tagName);
		}


		[WebMethod(EnableSession = true)]
		public List<Member> SearchMembers(string search)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().List(search);
		}

		public User Login(string username, string passwordHash)
		{
			return SERVBLLFactory.Factory.MemberBLL().Login(username, passwordHash);
		}

		private User CurrentUser()
		{
			if (System.Web.HttpContext.Current.Session["User"] == null) { return null; }
			return (User)Session["User"];
		}

		private void Authenticate()
		{
			if (CurrentUser() == null)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
		}

    }
}

