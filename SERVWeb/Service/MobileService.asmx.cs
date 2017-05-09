using System.Web.Services;
using System.Collections.Generic;
using SERVDataContract;
using SERVBLL;

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
			new MemberBLL().GoOnDuty(CurrentUser().MemberID);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public bool GoOffDuty()
		{
			Authenticate();
			new MemberBLL().GoOffDuty(CurrentUser().MemberID);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public bool UpdateLocation(string lat, string lng)
		{
			Authenticate();
			new MemberBLL().UpdateLocation(CurrentUser().MemberID, lat, lng);
			return true;
		}

		[WebMethod(EnableSession = true)]
		public Member GetCurrentController()
		{
			Authenticate();
			return new ShiftBLL().GetCurrentController();
		}

		[WebMethod(EnableSession = true, CacheDuration=120)]
		public List<string> GetCalendarBulletins()
		{
			Authenticate();
			return new CalendarBLL().GetNextXDaysCalendarBulletins(2);
		}

		[WebMethod(EnableSession = true)]
		public CalendarEntry GetNextShift()
		{
			Authenticate();
			return new CalendarBLL().GetMemberNextShift(CurrentUser().MemberID);
		}

	}
}

