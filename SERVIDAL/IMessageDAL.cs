using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface IMessageDAL : IDisposable
	{
		void LogSentSMSMessage(string numbers, string message, int senderUserID);
	}
}

