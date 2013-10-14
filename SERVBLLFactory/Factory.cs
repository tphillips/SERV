using SERVIBLL;
using SERVBLL;

namespace SERVBLLFactory
{

	public class Factory
	{
		
		public static IMemberBLL MemberBLL()
		{
			return new SERVBLL.MemberBLL();
		}

	}
}
