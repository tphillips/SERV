using System;
using System.Collections.Generic;
using SERV.Utils.Data;
using System.Text;
using System.Runtime.Serialization;
using SERV.Utils;

namespace SERVDataContract
{
	[DataContract]
	[Serializable]
	public class Group
	{

		public Group()
		{
		}

		public Group(SERVDataContract.DbLinq.SERVDBGROUp metal)
		{
			this.GroupID = metal.GroupID;
			this.GroupName = metal.Group;
		}

		[DataMember]
		public int GroupID {get;set;}
		[DataMember]
		public string GroupName {get;set;}

	}
}