package uk.org.serv.servrider;

import android.app.Activity;
import android.telephony.SmsManager;
import android.widget.Toast;

public class Shared 
{
	
	public static void SendMessage(String to, String message, Activity activity) 
    {
		SmsManager sms = SmsManager.getDefault();
		sms.sendTextMessage(to, null, message, null, null);
		//if (res == android.telephony.smsManager.STATUS_ON_ICC_SENT)
		//{
			Toast.makeText(activity, (activity.getText(R.string.messageSent) + " - " + message), Toast.LENGTH_LONG).show();
		//}
		//else
		//{
			//Toast.makeText(activity, activity.getText(R.string.messageNOTSent), Toast.LENGTH_LONG).show();
		//}
    }
	
	public static void SendMessageToController(String message, Activity activity, String rider) 
    {
		message = rider + ": " + message;
		String to = ServApplication.getAppState(activity).getControllerNo();
		SmsManager sms = SmsManager.getDefault();
		sms.sendTextMessage(to, null, message, null, null);
		Toast.makeText(activity, (activity.getText(R.string.messageSent) + " (" + to + ") - " + message), Toast.LENGTH_LONG).show();
    }
	
}
