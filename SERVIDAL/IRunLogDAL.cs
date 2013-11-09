using System;
using System.Collections.Generic;
using System.Text;
using SERVDataContract.DbLinq;

namespace SERVIDAL
{
	public interface IRunLogDAL : IDisposable
	{

		bool CreateRawRecord(SERVDataContract.DbLinq.RawRunLog raw);
	
	}
}

