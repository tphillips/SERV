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
	public class Location
	{

		public Location()
		{
		}

		public Location(SERVDataContract.DbLinq.Location metal)
		{
			this.LocationID = metal.LocationID;
			this.LocationName = metal.Location1;
		}

		[DataMember]
		public int LocationID {get;set;}
		[DataMember]
		public string LocationName {get;set;}

	}
}

