using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract;

namespace SERVIBLL
{
	public interface IControllerBLL
	{
		bool DivertNumber(Member member, string overrideNumber);
		bool DivertNumber(string mobile);
	}
}

