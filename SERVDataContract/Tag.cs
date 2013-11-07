using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SERV.Utils;
using SERV.Utils.Data;

namespace SERVDataContract
{
	[DataContract]
	public class Tag
    {

		public Tag(){}

		public Tag(SERVDataContract.DbLinq.Tag metal)
		{
			this.TagID = metal.TagID;
			this.TagName = metal.Tag1;
		}

		[DataMember]
		public int TagID { get; set; }
		[DataMember]
		public string TagName { get; set; }
    }
}

