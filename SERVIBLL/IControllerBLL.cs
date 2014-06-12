using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IControllerBLL
	{
		bool DivertNumber(int memberID, string overrideNumber);
		bool DivertNumber(string mobile);
	}
}

