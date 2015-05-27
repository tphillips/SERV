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
			//SERV.Utils.Messaging.SendTextMessage("447429386911", "this is a test with some + more ' ! & % characters");
		}

		[Test]
		public void IsWeekA()
		{
			Assert.IsTrue(SERVBLLFactory.Factory.CalendarBLL().IsWeekA(new DateTime(2013,12,30)));
			Assert.IsTrue(SERVBLLFactory.Factory.CalendarBLL().IsWeekA(new DateTime(2013,12,31)));
			Assert.IsFalse(SERVBLLFactory.Factory.CalendarBLL().IsWeekA(new DateTime(2014,01,08)));
			Assert.IsTrue(SERVBLLFactory.Factory.CalendarBLL().IsWeekA(new DateTime(2014,06,18)));
			Assert.IsFalse(SERVBLLFactory.Factory.CalendarBLL().IsWeekA(new DateTime(2014,06,24)));
		}

		[Test]
		public void FindWeekABStarts()
		{
			Assert.IsTrue(SERVBLLFactory.Factory.CalendarBLL().GetCurrentWeekAStartDate().Day == 16);
			Assert.IsTrue(SERVBLLFactory.Factory.CalendarBLL().GetCurrentWeekBStartDate().Day == 23);
		}

		[Test]
		public void GenerateCalendar()
		{
			SERVBLLFactory.Factory.CalendarBLL().GenerateCalendar();
		}

		[Test]
		public void PushBullet()
		{
			SERVBLLFactory.Factory.MessageBLL().PushBullet("This is a Unit Test", "System Notification", "servssl_system");
		}

    }
}

