using NUnit.Framework;
using System;
using SERVDataContract.DbLinq;

namespace SERVTests
{
    [TestFixture()]
    public class Tests
    {

        [Test()]
        public void ListMemberTest()
        {
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().List("").Count > 0);
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().List("Phillips").Count > 0);
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().List("dasdasdasdasdasd").Count == 0);
        }

		[Test()]
		public void LoginMemberTest()
		{
			SERVUser u = SERVBLLFactory.Factory.MemberBLL().Login(username:"tris.phillips@gmail.com", passwordHash:"");
			Assert.IsNotNull(u);
			Assert.IsNotNull(u.Member);
		}

		/*

		[Test()]
		public void ChangeUserPassword()
		{
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().ChangeUserPassword(userId:123, oldPasswordHash:"test", newPassword:"test123"));
		}

		[Test()]
		public void CreateMember()
		{
			Member newMember = new Member();
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().Create(member:newMember) > 0);
		}

		[Test()]
		public void UpdateMember()
		{
			Member m = SERVBLLFactory.Factory.MemberBLL().Get(1234);
			Assert.IsNotNull(m);
			m.LastName = "test";
			Assert.IsTrue(SERVBLLFactory.Factory.MemberBLL().Update(member:m));
		}

*/

    }
}

