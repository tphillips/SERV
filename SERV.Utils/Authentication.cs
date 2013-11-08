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

		public static string Hash(string toHash)
		{
			System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] bytesIn = System.Text.ASCIIEncoding.ASCII.GetBytes(toHash);
			byte[] hash = md5.ComputeHash(bytesIn);
			string ret = "";
			foreach (byte b in hash)
			{
				ret += Convert.ToString(b, 16).PadLeft(2, '0');
			}
			return ret;
		}

	}
}
