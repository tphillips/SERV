using System;
using System.Collections.Generic;
using System.Text;
using SERVIDAL;
using SERVDataContract.DbLinq;
using System.Data;
using System.Data.Common;
using SERV.Utils.Data;
using System.Linq;

namespace SERVDAL
{

	public class MemberDAL : IMemberDAL, IDisposable
	{

		static SERVDataContract.DbLinq.SERVDB db;

		public MemberDAL()
		{
			db = new SERVDataContract.DbLinq.SERVDB(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
		}

		public Member Get(int memberId)
		{
			SERVDataContract.DbLinq.Member mem = (from m in db.Member where m.MemberID == memberId select m).FirstOrDefault();
			return mem;
		}
		
		public int Create(Member member)
		{
			db.Member.InsertOnSubmit(member);
			db.SubmitChanges();
			return member.MemberID;
		}

		public int Save(Member newclass)
		{
			throw new NotImplementedException();
		}

		public int Update(Member member)
		{
			db.SubmitChanges();
			return member.MemberID;
		}

		public List<Member> List(string search)
		{
			return (from m in db.Member where m.FirstName.Contains(search) || m.LastName.Contains(search) orderby m.LastName select m).ToList();
		}

		public List<Tag> ListMemberTags(int memberId)
		{
			throw new NotImplementedException();
		}
		
		public void AddMemberTag(int memberId, string tagName)
		{
			string sql = string.Format("insert into Member_Tag values ({0}, {1})", memberId, GetTagId(tagName));
			db.ExecuteCommand(sql);
		}
		
		public void RemoveMemberTag(int memberId, string tagName)
		{
			string sql = string.Format("delete from Member_Tag where MemberID = {0} and TagID = {1}", memberId, GetTagId(tagName));
			db.ExecuteCommand(sql);
		}

		private int GetTagId(string tagName)
		{
			return Convert.ToInt32(DBHelperFactory.DBHelper().ExecuteScalar("select TagID from Tag where Tag = " + SERV.Utils.String.DBSafeString(tagName)));
		}

		public User Login(string username, string passwordHash)
		{
			SERVDataContract.DbLinq.User ret = (from u in db.User where u.Member.EmailAddress == username select u).FirstOrDefault();
			if (ret == null) { return null; }
			if (!String.IsNullOrEmpty(ret.PasswordHash))
			{
				if (passwordHash == ret.PasswordHash)
				{
					return ret;
				}
			}
			else
			{
				string comp = SERV.Utils.Authentication.Hash(ret.Member.EmailAddress.ToLower().Trim() + ret.Member.MobileNumber);
				if (passwordHash == comp)
				{
					return ret;
				}
			}
			return null;
		}

		public void SetPasswordHash(string username, string passwordHash)
		{
			SERVDataContract.DbLinq.User ret = (from u in db.User where u.Member.EmailAddress == username select u).FirstOrDefault();
			ret.PasswordHash = passwordHash;
			db.SubmitChanges();
		}
		
		public void SetUserLastLoginDate(User u)
		{
			u.LastLoginDate = DateTime.Now;
			db.SubmitChanges();
		}
		
		public List<string> ListMobileNumbersWithTags(string tagsCsv)
		{
			List<string> ret = new List<string>();
			string[] tags = tagsCsv.Split(',');
			List<string> tagList = new List<string>();
			foreach (string tag in tags)
			{
				if (tag.Trim() != string.Empty) { tagList.Add(tag); }
			}
			string inClause = "in (";
			foreach (string t in tagList)
			{
				inClause += "'" + t.Trim() + "',";
			}
			inClause = inClause.Substring(0, inClause.Length - 1);
			inClause += ")";
			string sql = "select distinct m.MobileNumber " +
			             "from Member m " +
			             "join Member_Tag mt on mt.MemberID = m.MemberID " +
			             "join Tag t on t.TagID = mt.TagID " +
			             "where t.Tag " + inClause;
			DataTable tbl = DBHelperFactory.DBHelper().ExecuteDataTable(sql);
			if (tbl != null && tbl.Rows.Count > 0)
			{
				foreach (DataRow r in tbl.Rows)
				{
					ret.Add(r[0].ToString());
				}
			}
			return ret;
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}

}
