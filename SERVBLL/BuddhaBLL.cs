using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	public class BuddhaBLL : IBuddhaBLL
	{

		public BuddhaBLL()
		{
		}

		public int GetMemberKarmaPoints(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateMiscKarma(int memberID, int points, string reason)
		{
			throw new NotImplementedException();
		}

		public void AllocateBloodRunKarma(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateAdHocBloodRunKarma(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateAARunKarma(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateControllerKarma(int memberID, DateTime shiftDate)
		{
			throw new NotImplementedException();
		}

		public void AllocateEventKarma(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateLoginKarma(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateProfileUpdateKarma(int memberID)
		{
			throw new NotImplementedException();
		}

		public void AllocateOnDutyKarma(int memberID)
		{
			throw new NotImplementedException();
		}

	}
}

