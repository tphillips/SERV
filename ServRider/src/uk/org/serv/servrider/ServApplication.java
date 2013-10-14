package uk.org.serv.servrider;
import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Date;
import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.preference.PreferenceManager;

enum EventType
{
	DepartingToPickup,
	Delayed,
	ArrivedAtPickup,
	WaitingForPickup,
	PickedUp,
	DepartingToDrop,
	Dropped,
	HeadingHome,
	HeadingToBunkhouse,
	WaitingAtDrop,
	ArrivedHome
}

public class ServApplication extends Application {

	private Boolean onDuty = false;
	private RiderLocation location;
	private Context context;
	
	public RiderLocation getLocation() {
		return location;
	}

	public void setLocation(RiderLocation location) {
		this.location = location;
	}
	
	public Boolean isOnDuty() {
		return onDuty;
	}

	public void setOnDuty(Boolean onDuty) {
		this.onDuty = onDuty;
	}
	
	public String getControllerNo() {
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		return pref.getString("controller", "");
	}

	public void setControllerNo(String controllerNo) {
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		Editor e = pref.edit();
		e.putString("controller", controllerNo);
		e.commit();
	}
	
	public String getJobFrom() {
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		return pref.getString("jobFrom", "");
	}

	public void setJobFrom(String from) {
		Log("Pickup set to " + from);
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		Editor e = pref.edit();
		e.putString("jobFrom", from);
		e.commit();
	}
	
	public String getJobTo() {
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		return pref.getString("jobTo", "");
	}

	public void setJobTo(String to) {
		Log("Drop set to " + to);
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		Editor e = pref.edit();
		e.putString("jobTo", to);
		e.commit();
	}
	
	public void NewJob()
	{
		Log("New Job Started");
	}
	
	public ServApplication() {
		// TODO Auto-generated constructor stub		
	}
	
	@Override
    public void onCreate() {
        super.onCreate();
        
    }
	
	public static ServApplication getAppState(Activity activity)
	{
		Application a = activity.getApplication();
		((ServApplication) a).setContext(activity);
		return ((ServApplication) a);
	}

	public Context getContext() {
		return context;
	}

	private void setContext(Context context) {
		this.context = context;
		//location = new RiderLocation(context);
	}

	public String getRider() {
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		return pref.getString("rider", "");
	}

	public void setRider(String val) {
		SharedPreferences pref = PreferenceManager.getDefaultSharedPreferences(context);
		Editor e = pref.edit();
		e.putString("rider", val);
		e.commit();
	}
	
	public void LogEvent(EventType et)
	{
		String string = et.toString();
		Log(string);
	}
	
	private void Log(String s)
	{
		FileOutputStream fos;
		try {
			fos = context.openFileOutput("log.txt", Context.MODE_APPEND);
		} catch (FileNotFoundException e1) {
			e1.printStackTrace();
			return;
		}
		try {
			fos.write((new Date().toLocaleString() + " - " + s + "\r\n").getBytes());
			fos.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
	}
	
	public static String getLog(Context context)
	{
		String ret = "";
		FileInputStream fin;
		try {
			fin = context.openFileInput("log.txt");
		} catch (FileNotFoundException e1) {
			e1.printStackTrace();
			return "";
		}
		InputStreamReader isr = new InputStreamReader(fin);
		BufferedReader r = new BufferedReader(isr);
		String line = "";
		while (line != null)
		{
			try {
				line = r.readLine();
			} catch (IOException e) {
				e.printStackTrace();
				line = null;
			}
			if (line != null)
			{
				ret += line + "\r\n";
			}
		}
		return ret;
	}

}
