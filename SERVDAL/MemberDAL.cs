using System;
using System.Collections.Generic;
using System.Text;
using SERVIDAL;
using SERVDataContract;
using System.Data;
using System.Data.Common;
using SERV.Utils.Data;

namespace SERVDAL
{

	public class MemberDAL : IMemberDAL
	{
		
		public Member Get(int memberId)
		{
			Member ret = null;
			DataTable t = DBHelperFactory.DBHelper().ExecuteDataTable("Select * from Member " +
				"where MemberID = " + memberId);
			if (t != null)
			{
				ret = GetFromRow(t.Rows[0]);
			}
			return ret;
		}

		public List<Tag> ListMemberTags(int memberId)
		{
			List<Tag> ret = new List<Tag>();
			DataTable t = DBHelperFactory.DBHelper().ExecuteDataTable("Select * from Member_Tag mt " +
				"join Tag t on mt.TagID = t.TagID " +
				"where mt.MemberID = " + memberId);
			if (t != null)
			{
				foreach(DataRow r in t.Rows)
				{
					ret.Add(new Tag() { ID = Convert.ToInt32(r["TagID"]), TagName = r["Tag"].ToString() });
				}
			}
			return ret;
		}

		public int Save(Member member)
		{
			if (member.ID == -1)
			{
				return Create(member);
			}
			else
			{
				Update(member);
				return member.ID;
			}
		}

		public int Create(Member member)
		{
			const string sql = 
				"INSERT INTO Member (" +
				", FirstName" +
				", LastName" +
				", JoinDate" +
				", EmailAddress" +
				", MobileNumber" +
				", HomeNumber" +
				", Occupation" +
				", MemberTypeID" +
				", MemberStatusID" +
				", AvailabilityID" +
				", RiderAssesmentPassDate" +
				", AdQualPassDate" +
				", AdQualType" +
				", BikeType" +
				", CarType" +
				", Notes" +
				", Address1" +
				", Address2" +
				", Address3" +
				", Town" +
				", County" +
				", PostCode" +
				", BirthYear" +
				", NextOfKin" +
				", MextOfKinAddress" +
				", NextOfKinPhone" +
				", LegalConfirmation" +
				", LeaveDate" +
				", LastGDPGMPDate" + ") values (" +
				", @FirstName" +
				", @LastName" +
				", @JoinDate" +
				", @EmailAddress" +
				", @MobileNumber" +
				", @HomeNumber" +
				", @Occupation" +
				", @MemberTypeID" +
				", @MemberStatusID" +
				", @AvailabilityID" +
				", @RiderAssesmentPassDate" +
				", @AdQualPassDate" +
				", @AdQualType" +
				", @BikeType" +
				", @CarType" +
				", @Notes" +
				", @Address1" +
				", @Address2" +
				", @Address3" +
				", @Town" +
				", @County" +
				", @PostCode" +
				", @BirthYear" +
				", @NextOfKin" +
				", @MextOfKinAddress" +
				", @NextOfKinPhone" +
				", @LegalConfirmation" +
				", @LeaveDate" +
				", @LastGDPGMPDate" +
				"); SELECT @@IDENTITY";
			DbCommand com = DBHelperFactory.DBHelper().CreateDbCommand(sql);			
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@FirstName", DbType.String, SERV.Utils.String.GetStringFromDbMember("FirstName", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@LastName", DbType.String, SERV.Utils.String.GetStringFromDbMember("LastName", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@JoinDate", DbType.DateTime, member.JoinDate);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@EmailAddress", DbType.String, SERV.Utils.String.GetStringFromDbMember("EmailAddress", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@MobileNumber", DbType.String, SERV.Utils.String.GetStringFromDbMember("MobileNumber", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@HomeNumber", DbType.String, SERV.Utils.String.GetStringFromDbMember("HomeNumber", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@Occupation", DbType.String, SERV.Utils.String.GetStringFromDbMember("Occupation", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@MemberTypeID", DbType.Int32, member.MemberTypeID);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@MemberStatusID", DbType.Int32, member.MemberStatusID);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@AvailabilityID", DbType.Int32, member.AvailabilityID);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@RiderAssesmentPassDate", DbType.DateTime, member.RiderAssesmentPassDate);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@AdQualPassDate", DbType.DateTime, member.AdQualPassDate);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@AdQualType", DbType.String, SERV.Utils.String.GetStringFromDbMember("AdQualType", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@BikeType", DbType.String, SERV.Utils.String.GetStringFromDbMember("BikeType", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@CarType", DbType.String, SERV.Utils.String.GetStringFromDbMember("CarType", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@Notes", DbType.String, SERV.Utils.String.GetStringFromDbMember("Notes", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@Address1", DbType.String, SERV.Utils.String.GetStringFromDbMember("Address1", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@Address2", DbType.String, SERV.Utils.String.GetStringFromDbMember("Address2", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@Address3", DbType.String, SERV.Utils.String.GetStringFromDbMember("Address3", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@Town", DbType.String, SERV.Utils.String.GetStringFromDbMember("Town", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@County", DbType.String, SERV.Utils.String.GetStringFromDbMember("County", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@PostCode", DbType.String, SERV.Utils.String.GetStringFromDbMember("PostCode", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@BirthYear", DbType.Int32, member.BirthYear);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@NextOfKin", DbType.String, SERV.Utils.String.GetStringFromDbMember("NextOfKin", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@MextOfKinAddress", DbType.String, SERV.Utils.String.GetStringFromDbMember("MextOfKinAddress", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@NextOfKinPhone", DbType.String, SERV.Utils.String.GetStringFromDbMember("NextOfKinPhone", member));
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@LegalConfirmation", DbType.Boolean, member.LegalConfirmation);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@LeaveDate", DbType.DateTime, member.LeaveDate);
			DBHelperFactory.DBHelper().AddCommandParameter(com, "@LastGDPGMPDate", DbType.DateTime, member.LastGDPGMPDate);
			object ret = DBHelperFactory.DBHelper().ExecuteScalarCommand(com);
			if (ret == DBNull.Value || ret == null) { return -1; }
			return int.Parse(ret.ToString());
		}	
		
		public void Update(Member member)
		{
			// AUTO GENERATED DO NOT EDIT BY HAND
			string sql = 
				#region AutoGenerated
				"UPDATE NewClass set  FirstName = '" + member.FirstName + "'" +
					", LastName = '" + member.LastName + "'" +
					", JoinDate = '" + member.JoinDate.ToString("yyyy-MM-dd") + "'" +
					", EmailAddress = '" + member.EmailAddress + "'" +
					", MobileNumber = '" + member.MobileNumber + "'" +
					", HomeNumber = '" + member.HomeNumber + "'" +
					", Occupation = '" + member.Occupation + "'" +
					", MemberTypeID = '" + member.MemberTypeID + "'" +
					", MemberStatusID = '" + member.MemberStatusID + "'" +
					", AvailabilityID = '" + member.AvailabilityID + "'" +
					", RiderAssesmentPassDate = " + (member.RiderAssesmentPassDate != null ? "'" + ((DateTime)member.RiderAssesmentPassDate).ToString("yyyy-MM-dd") + "'": "null") +
					", AdQualPassDate = " + (member.AdQualPassDate != null ? "'" + ((DateTime)member.AdQualPassDate).ToString("yyyy-MM-dd") + "'": "null")  +
					", AdQualType = '" + member.AdQualType + "'" +
					", BikeType = '" + member.BikeType + "'" +
					", CarType = '" + member.CarType + "'" +
					", Notes = '" + member.Notes + "'" +
					", Address1 = '" + member.Address1 + "'" +
					", Address2 = '" + member.Address2 + "'" +
					", Address3 = '" + member.Address3 + "'" +
					", Town = '" + member.Town + "'" +
					", County = '" + member.County + "'" +
					", PostCode = '" + member.PostCode + "'" +
					", BirthYear = '" + member.BirthYear + "'" +
					", NextOfKin = '" + member.NextOfKin + "'" +
					", NextOfKinAddress = '" + member.NextOfKinAddress + "'" +
					", NextOfKinPhone = '" + member.NextOfKinPhone + "'" +
					", LegalConfirmation = '" + member.LegalConfirmation + "'" +
					", LeaveDate = " + (member.LeaveDate != null ? "'" + ((DateTime)member.LeaveDate).ToString("yyyy-MM-dd") + "'": "null") +
					", LastGDPGMPDate = " + (member.LastGDPGMPDate != null ? "'" + ((DateTime)member.LastGDPGMPDate).ToString("yyyy-MM-dd") + "'": "null") +
					" where MemberID = " + member.ID;
			#endregion
			DBHelperFactory.DBHelper().ExecuteSQL(sql);
		}

		private Member GetFromRow(DataRow r)
		{
			Member ret = new Member();
			ret.ID = (int)r["MemberID"];
			ret.FirstName = r["FirstName"] != DBNull.Value ? (string)r["FirstName"].ToString() : null;
			ret.LastName = r["LastName"] != DBNull.Value ? (string)r["LastName"].ToString() : null;
			ret.EmailAddress = r["EmailAddress"] != DBNull.Value ? (string)r["EmailAddress"].ToString() : null;
			ret.MobileNumber = r["MobileNumber"] != DBNull.Value ? (string)r["MobileNumber"].ToString() : null;
			ret.HomeNumber = r["HomeNumber"] != DBNull.Value ? (string)r["HomeNumber"].ToString() : null;
			ret.Occupation = r["Occupation"] != DBNull.Value ? (string)r["Occupation"].ToString() : null;
			ret.MemberTypeID = (int)r["MemberTypeID"];
			ret.MemberStatusID = (int)r["MemberStatusID"];
			try { ret.AvailabilityID = (int)r["AvailabilityID"]; } catch { ret.AvailabilityID = null; }
			try { ret.RiderAssesmentPassDate = (DateTime?)r["RiderAssesmentPassDate"]; } catch { ret.RiderAssesmentPassDate = null; }
			try { ret.AdQualPassDate = (DateTime?)r["AdQualPassDate"]; }  catch { ret.AdQualPassDate = null; }
			ret.AdQualType = r["AdQualType"] != DBNull.Value ? (string)r["AdQualType"].ToString() : null;
			ret.BikeType = r["BikeType"] != DBNull.Value ? (string)r["BikeType"].ToString() : null;
			ret.CarType = r["CarType"] != DBNull.Value ? (string)r["CarType"].ToString() : null;
			ret.Notes = r["Notes"] != DBNull.Value ? (string)r["Notes"].ToString() : null;
			ret.Address1 = r["Address1"] != DBNull.Value ? (string)r["Address1"].ToString() : null;
			ret.Address2 = r["Address2"] != DBNull.Value ? (string)r["Address2"].ToString() : null;
			ret.Address3 = r["Address3"] != DBNull.Value ? (string)r["Address3"].ToString() : null;
			ret.Town = r["Town"] != DBNull.Value ? (string)r["Town"].ToString() : null;
			ret.County = r["County"] != DBNull.Value ? (string)r["County"].ToString() : null;
			ret.PostCode = r["PostCode"] != DBNull.Value ? (string)r["PostCode"].ToString() : null;
			ret.BirthYear = (int)r["BirthYear"];
			ret.NextOfKin = r["NextOfKin"] != DBNull.Value ? (string)r["NextOfKin"].ToString() : null;
			ret.NextOfKinAddress = r["NextOfKinAddress"] != DBNull.Value ? (string)r["NextOfKinAddress"].ToString() : null;
			ret.NextOfKinPhone = r["NextOfKinPhone"] != DBNull.Value ? (string)r["NextOfKinPhone"].ToString() : null;
			ret.LegalConfirmation = (bool)r["LegalConfirmation"];
			try { ret.JoinDate = (DateTime)r["JoinDate"]; } catch { ret.JoinDate = DateTime.MinValue; }
			try { ret.LeaveDate = (DateTime?)r["LeaveDate"]; } catch { ret.LeaveDate = null; }
			try { ret.LastGDPGMPDate = (DateTime?)r["LastGDPGMPDate"]; } catch { ret.LastGDPGMPDate = null; }
			return ret;
		}

		public List<Member> List(string search)
		{
			List<Member> ret = new List<Member>();
			string sql = String.IsNullOrEmpty(search) ? "Select * from Member" : "Select * from Member where FirstName like '%" + search + "%' or LastName like '%" + search + "%'";
			DataTable t = DBHelperFactory.DBHelper().ExecuteDataTable(sql);
			if (t != null)
			{
				foreach (DataRow r in t.Rows)
				{
					ret.Add(GetFromRow(r));
				}
			}
			return ret;
		}

	}

}
