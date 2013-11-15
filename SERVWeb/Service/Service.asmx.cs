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
		static Logger log = new Logger();

		[WebMethod]
		public bool ImportRawRunLog()
		{
			log.LogStart();
			System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(_ImportRawRunLog));
			t.IsBackground = true;
			t.Start();
			log.LogEnd();
			return true;
		}

		private void _ImportRawRunLog()
		{
			log.LogStart();
			try
			{
				SERVBLLFactory.Factory.RunLogBLL().ImportRawRunLog();
			}
			catch(Exception e)
			{
				log.Error(e.Message, e);
			}
			log.LogEnd();
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

		[WebMethod(EnableSession = true)]
		public List<Member> ListMembersWithTags(string tagsCsv)
		{
			Authenticate();
			return SERVBLLFactory.Factory.MemberBLL().ListMembersWithTags(tagsCsv);
		}

		[WebMethod(EnableSession = true)]
		public List<string> ListMobileNumbersWithTags(string tagsCsv)
		{
			Authenticate();
			if (CurrentUser().UserLevelID <= (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MemberBLL().ListMobileNumbersWithTags(tagsCsv);
		}

		[WebMethod(EnableSession = true)]
		public List<Location> ListLocations()
		{
			Authenticate();
			return SERVBLLFactory.Factory.ListBLL().ListLocations();
		}

		[WebMethod(EnableSession = true)]
		public List<VehicleType> ListVehicleTypes()
		{
			Authenticate();
			return SERVBLLFactory.Factory.ListBLL().ListVehicleTypes();
		}

		[WebMethod(EnableSession = true)]
		public bool LogRun(string callDateTime, int callFromLocationId, string collectDateTime, int collectionLocationId, 
			int controllerMemberId, string deliverDateTime, int deliverToLocationId, string dutyDate, 
			int finalDestinationLocationId, int originLocationId, int riderMemberId, int urgency, int vehicleTypeId, string productIdCsv)
		{
			Authenticate();
			DateTime _callDateTime = DateTime.Parse(callDateTime);
			DateTime _collectDateTime = DateTime.Parse(collectDateTime);
			DateTime _deliverDateTime = DateTime.Parse(deliverDateTime);
			DateTime _dutyDate = DateTime.Parse(dutyDate);
			return SERVBLLFactory.Factory.RunLogBLL().CreateRunLog(_callDateTime, callFromLocationId, _collectDateTime, collectionLocationId, 
				controllerMemberId, CurrentUser().UserID, _deliverDateTime, deliverToLocationId, _dutyDate, 
				finalDestinationLocationId, originLocationId, riderMemberId, urgency, vehicleTypeId, productIdCsv);
		}

		[WebMethod(EnableSession = true)]
		public bool SendSMSMessage(string numbers, string message)
		{
			Authenticate();
			if (CurrentUser().UserLevelID <= (int)UserLevel.Controller)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
			return SERVBLLFactory.Factory.MessageBLL().SendSMSMessage(numbers, message, CurrentUser().UserID);
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

