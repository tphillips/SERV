using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void MemberTest()
        {
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().List("").Count > 0);
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().List("Phillips").Count > 0);
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().List("dasdasdasdasdasd").Count == 0);
        }
    }
}

