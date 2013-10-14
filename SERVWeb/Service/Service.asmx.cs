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
		public List<Member> ListMembers()
		{
			return SERVBLLFactory.Factory.MemberBLL().List("");
		}

		[WebMethod]
		public List<Member> SearchMembers(string search)
		{
			return SERVBLLFactory.Factory.MemberBLL().List(search);
		}

    }
}

