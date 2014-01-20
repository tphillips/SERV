using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IBuddhaBLL
	{
		void AllocateMiscKarma(int memberID, int points, string reason);
		void AllocateBloodRunKarma(int memberID);
		void AllocateAdHocBloodRunKarma(int memberID);
		void AllocateAARunKarma(int memberID);
		void AllocateControllerKarma(int memberID, DateTime shiftDate);
		void AllocateEventKarma(int memberID);
		void AllocateLoginKarma(int memberID);
		void AllocateProfileUpdateKarma(int memberID);
		void AllocateOnDutyKarma(int memberID);
		int GetMemberKarmaPoints(int memberID);
	}
}

