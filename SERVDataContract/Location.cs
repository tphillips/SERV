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
			this.BloodBank = metal.BloodBank == 1;
			this.ChangeOver = metal.Changeover == 1;
			this.Enabled = metal.Enabled == 1;
			this.Hospital = metal.Hospital == 1;
			this.Lat = metal.Lat;
			this.Lng = metal.Lng;
		}

		[DataMember]
		public int LocationID {get;set;}
		[DataMember]
		public string LocationName {get;set;}

		[DataMember]
		public bool BloodBank {get;set;}

		[DataMember]
		public bool ChangeOver {get;set;}

		[DataMember]
		public bool Hospital {get;set;}

		[DataMember]
		public bool Enabled {get;set;}

		[DataMember]
		public string Lat {get;set;}

		[DataMember]
		public string Lng {get;set;}

	}
}

