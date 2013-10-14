using System.Collections;

namespace SERV.Utils
{
	public class Array
	{
		public static bool IsObjectInArray(IEnumerable stuff, object obj)
		{
			foreach(object o in stuff)
			{
				if(o.Equals(obj))
				{
					return true;
				}
			}
			return false;
		}
	}
}
