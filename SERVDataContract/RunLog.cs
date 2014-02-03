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
	public class RunLog
	{

		public RunLog(){}

		public RunLog(SERVDataContract.DbLinq.RunLog metal)
		{
			PropertyMapper.MapProperties(metal, this);
		}

		public int RunLogID { get; set; }

		public int Boxes { get; set; }

		public DateTime? CallDateTime { get; set; }

		public int CallFromLocationID { get; set; }

		public DateTime? CollectDateTime { get; set; }

		public int CollectionLocationID { get; set; }

		public int ControllerMemberID { get; set; }

		public DateTime CreateDate { get; set; }

		public int CreatedByUserID { get; set; }

		public DateTime? DeliverDateTime { get; set; }

		public int DeliverToLocationID { get; set; }

		public string Description { get; set; }

		public DateTime? DutyDate { get; set; }

		public int FinalDestinationLocationID { get; set; }

		public DateTime? HomeSafeDateTime { get; set; }

		public bool IsTransfer { get; set; }

		public string Notes { get; set; }

		public int OriginLocationID { get; set; }

		public int RiderMemberID { get; set; }

		public int Urgency { get; set; }

		public int VehicleTypeID { get; set; }

	}
}

