using System;

namespace SERV.Utils
{
	public class Date
	{
		/// <summary>
		/// Given a string (2013-04-08 10:14:23.767 CDT) returns a string (2013-04-08 10:14:23.767 +5:00) which can be converter to datetime succesfully. Otherwise just returns the provided string.
		/// </summary>
		public static string WorldTimeSuffixesToUTCAdjustments(string dateInString)
		{
			if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "CDT", "+5:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "IDT", "+0:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "CST", "+6:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "Guam", "-10:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "HI", "+10:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "AK", "+9:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "PST", "+8:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "MST", "+7:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "EST", "+5:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "AST", "+4:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "PDT", "+7:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "MDT", "+6:00"))
				return dateInString;
			else if (WorldTimeSuffixesToUTCAdjustmentsHelper(ref dateInString, "EDT", "+4:00"))
				return dateInString;

			return dateInString;
		}

		/// <summary>
		/// In order to efficiently ignore case, this helper function exists
		/// </summary>
		private static bool WorldTimeSuffixesToUTCAdjustmentsHelper(ref string dateInString, string find, string replace)
		{
			int inStart = dateInString.IndexOf(find, StringComparison.InvariantCultureIgnoreCase);
			if (inStart > -1)
			{
				dateInString = dateInString.Substring(0, inStart) + replace + dateInString.Substring(inStart + find.Length);
				return true;
			}
			return false;
		}
	}
}
