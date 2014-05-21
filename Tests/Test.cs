using NUnit.Framework;
using System;
using SERVDataContract.DbLinq;

namespace SERVTests
{
    [TestFixture()]
    public class Tests
    {

		[Test]
		public void SMS()
		{
			SERV.Utils.Messaging.SendTextMessage("447429386911", "this is a test with some + more ' ! & % characters");
		}

    }
}

