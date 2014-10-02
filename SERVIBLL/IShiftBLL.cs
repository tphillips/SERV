using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IShiftBLL
	{
		bool TakeControl(int memberID, string overrideNumber);
		string SwitchController();
	}
}

