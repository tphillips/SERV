using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace SERV.Utils
{
	public class Authentication
	{

		public static bool AcceptAllCertificates(object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error)
		{
			return true;
		}

		public static string CurrentUsername
		{
			get
			{
				
				try
				{
					if (System.Web.HttpContext.Current != null)
					{
						return System.Web.HttpContext.Current.User.Identity.Name;
					}
					WindowsIdentity iden = WindowsIdentity.GetCurrent();
					return iden != null ? iden.Name : "";
				}
				catch (Exception)
				{
					return "";
				}
				
			}
		}
	}
}
