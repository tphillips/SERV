using System;

namespace SERV.Utils.Data
{
	public class DbMemberAttribute : Attribute
	{
		public int MaxLength { get; set; }
		public bool KeyField { get; set; }
		/// <summary>
		/// When set and true, calls SERV.Utils.String.MakeSafeHtmlTags on the value of the property inside SERV.Utils.String.GetStringFromDbMember so that it can be rectified before being written to the database.
		/// </summary>
		public bool EnsureHtmlSafeOnSave { get; set; }
	}
}
