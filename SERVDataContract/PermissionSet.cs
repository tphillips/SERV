using System;

namespace SERVDataContract
{
	public class PermissionSet
	{

		public bool CanBulkSMS {get; set;}
		public bool CanListOtherMembers {get; set;}
		public bool CanViewOtherMembersHighLevel {get; set;}
		public bool CanViewOtherMembersDetail {get; set;}
		public bool CanEditOtherMembers {get; set;}
		public bool CanCreateMembers {get; set;}
		public bool IsSuperUser {get; set;}
		public bool IsController {get; set;}
		public bool IsAdmin {get; set;}

		public PermissionSet()
		{
			// Default permissions
			this.CanEditOtherMembers = false;
			this.CanListOtherMembers = false;
			this.CanViewOtherMembersDetail = false;
			this.CanViewOtherMembersHighLevel = false;
			this.IsSuperUser = false;
			this.CanBulkSMS = false;
			this.IsAdmin = false;
			this.IsController = false;
		}

	}
}

