using System;

namespace SERV.Utils
{
	public class Math
	{
		/// <summary>
		/// Rounds up to the next int if any remainder exists (1.1 -> 2)
		/// </summary>
		/// <remarks>
		/// Used for total pages calculations.
		/// </remarks>
		public static int DivRoundUp(int dividend, int divisor)
		{
			if (divisor == 0) throw new Exception("Attempt to divide by zero.");
			if (divisor == -1 && dividend == Int32.MinValue) throw new Exception("Result would be out of range.");

			int RoundedTowardsZeroQuotient = dividend / divisor;
			bool DividedEvenly = (dividend % divisor) == 0;
			if (DividedEvenly)
				return RoundedTowardsZeroQuotient;

			bool WasRoundedDown = ((divisor > 0) == (dividend > 0));
			if (WasRoundedDown)
				return RoundedTowardsZeroQuotient + 1;
			else
				return RoundedTowardsZeroQuotient;
		}
	}
}
