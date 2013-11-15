using SERVIDAL;
using System.Reflection;
using SERVDAL;

namespace SERVDALFactory
{

	public class Factory
	{

		public static IMemberDAL MemberDAL()
        {
			return new MemberDAL();
        }
		
		public static IRunLogDAL RunLogDAL()
		{
			return new RunLogDAL();
		}

		public static IMessageDAL MessageDAL()
		{
			return new MessageDAL();
		}

		public static IListDAL ListDAL()
		{
			return new ListDAL();
		}

	}
}
