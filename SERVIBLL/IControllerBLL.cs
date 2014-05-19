using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IControllerBLL
	{
		bool DivertNumber(int memberID);
		bool DivertNumber(string mobile);
	}
}

