using System;
using System.Runtime.Serialization;

namespace SERVDataContract
{
	[DataContract]
	[Serializable]
	public class VehicleType
	{

		public VehicleType()
		{
		}

		public VehicleType(SERVDataContract.DbLinq.VehicleType metal)
		{
			this.VehicleTypeID = metal.VehicleTypeID;
			this.VehicleTypeName = metal.VehicleType1;
		}

		[DataMember]
		public int VehicleTypeID {get;set;}
		[DataMember]
		public string VehicleTypeName {get;set;}

	}
}