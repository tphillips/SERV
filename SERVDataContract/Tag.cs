using System;
using System.Collections.Generic;
using SERV.Utils.Data;
using System.Text;
using System.Runtime.Serialization;

namespace SERVDataContract
{
	[DataContract]
    public class Tag
    {
		[DataMember]
		public int ID { get; set; }
		[DataMember]
		public string TagName { get; set; }
    }
}

