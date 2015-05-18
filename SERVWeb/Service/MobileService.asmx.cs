using System;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;
using SERVDataContract;
using SERVBLLFactory;
using System.Data;
using System.Threading;

namespace SERVWeb
{
	public class MobileService : System.Web.Services.WebService
	{

		private User CurrentUser()
		{
			if (System.Web.HttpContext.Current.Session["User"] == null) { return null; }
			return (User)System.Web.HttpContext.Current.Session["User"];
		}

		public void Authenticate()
		{
			if (CurrentUser() == null)
			{
				throw new System.Security.Authentication.AuthenticationException();
			}
		}

		[WebMethod(EnableSession = true)]
		public SERVDataContract.User StartSession(string username, string password)
		{
			SERVDataContract.User u = new Service().Login(username, SERV.Utils.Authentication.Hash(username.ToLower().Trim() + password));
			if (u != null)
			{
				SERVGlobal.User = u;
				return SERVGlobal.User;
			}
			return null;
		}

		[WebMethod(EnableSession = true)]
		public bool GoOnDuty()
		{
			Authenticate();
			SERVBLLFactory.Factory.MemberBLL().GoOnDuty(CurrentUser().MemberID);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public bool GoOffDuty()
		{
			Authenticate();
			SERVBLLFactory.Factory.MemberBLL().GoOffDuty(CurrentUser().MemberID);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public bool UpdateLocation(string lat, string lng)
		{
			Authenticate();
			SERVBLLFactory.Factory.MemberBLL().UpdateLocation(CurrentUser().MemberID, lat, lng);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public Member GetCurrentController()
		{
			Authenticate();
			return SERVBLLFactory.Factory.ShiftBLL().GetCurrentController();
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<string> GetCalendarBulletins()
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().GetNextXDaysCalendarBulletins(2);
		}

		[WebMethod(EnableSession = true)]
		public CalendarEntry GetNextShift()
		{
			Authenticate();
			return SERVBLLFactory.Factory.CalendarBLL().GetMemberNextShift(CurrentUser().MemberID);
		}

	}
}

