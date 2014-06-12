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

		public static IRunLogBLL RunLogBLL()
		{
			return new SERVBLL.RunLogBLL();
		}

		public static IMessageBLL MessageBLL()
		{
			return new SERVBLL.MessageBLL();
		}

		public static IListBLL ListBLL()
		{
			return new SERVBLL.ListBLL();
		}

		public static ILocationBLL LocationBLL()
		{
			return new SERVBLL.LocationBLL();
		}

		public static IShiftBLL ShiftBLL()
		{
			return new SERVBLL.ShiftBLL();
		}

	}
}
