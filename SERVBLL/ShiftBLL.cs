using System;
using System.Collections.Generic;
using System.Text;
using SERVIBLL;
using SERVDataContract;
using SERVDALFactory;
using SERV.Utils;

namespace SERVBLL
{
	public class ShiftBLL : IShiftBLL
	{

		public ShiftBLL()
		{
		}

		public int Create(DateTime shiftStartDate, int dutyType)
		{
			throw new NotImplementedException();
		}

		public void SetOnDuty(int memberID, DateTime shiftStartDate, VehicleType vehicle, int maxBoxes, string currentLocation, int useUntilHour, string mobileNumber, int dutyType)
		{
		}
			
		public bool TakeControl(int memberID, string overrideNumber)
		{
			return new ControllerBLL().DivertNumber(memberID, overrideNumber);
		}

	}
}

