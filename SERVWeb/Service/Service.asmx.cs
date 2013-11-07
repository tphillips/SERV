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

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembers()
		{
			return SERVBLLFactory.Factory.MemberBLL().List("");
		}

		[WebMethod(EnableSession = true)]
		public Member GetMember(int memberId)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().Get(memberId);
		}

		[WebMethod(EnableSession = true)]
		public bool SaveMember(Member member)
		{
			Authenticate();
			if (!CurrentUser().Permissions.IsAdmin && !CurrentUser().Permissions.CanEditOtherMembers && member.MemberID != CurrentUser().MemberID)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MemberBLL().Save(member) == member.MemberID;
		}

		[WebMethod(EnableSession = true)]
		public List<Member> SearchMembers(string search)
		{
			return SERVBLLFactory.Factory.MemberBLL().List(search);
		}

		public User Login(string username, string passwordHash)
		{
			return SERVBLLFactory.Factory.MemberBLL().Login(username, passwordHash);
		}

		private User CurrentUser()
		{
			if (Session["User"] == null) { return null; }
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

