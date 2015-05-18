package uk.org.serv.servrider;

import java.util.List;
import java.util.Locale;
import android.content.Context;
import android.location.Address;
import android.location.Geocoder;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;

interface OnUpdateListener {
	void updated(String location);
} 

public class RiderLocation implements LocationListener
{

	private LocationManager locmgr = null;
	private String latLon = "Location unknown";
	private String address = "Address unknown";
	private Context theContext;
	private OnUpdateListener onUpdate;
	
	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}
	
	public String getLatLon() {
		return latLon;
	}

	public void setLatLon(String latLon) {
		this.latLon = latLon;
	}
	
	public RiderLocation(Context context)
	{
		theContext = context;
		locmgr = (LocationManager) context.getSystemService(Context.LOCATION_SERVICE);
		Resume();
	}
	
	public void onLocationChanged(Location loc) 
    {
		latLon = "Lat: " + loc.getLatitude() + "\nLong: " + loc.getLongitude();   
		if (loc != null)
    	{
	       	Geocoder gc = new Geocoder(theContext, Locale.getDefault()); 
	   		try 
	   		{ 
	    		List<Address> addresses = gc.getFromLocation(loc.getLatitude(), loc.getLongitude(), 1); 
	    		StringBuilder sb = new StringBuilder(); 
	    		if(addresses.size()>0) 
	    		{ 
	    			Address address = (Address) addresses.get(0); 
	    			sb.append(address.getAddressLine(0)).append("\n"); 
	    			sb.append(address.getLocality()).append("\n"); 
	    			sb.append(address.getPostalCode()).append("\n"); 
	    			sb.append(address.getCountryName()); 
	    		} 
	    		address = sb.toString();
	    		String ret = latLon + "\n" + address;
	    		if (onUpdate != null) { onUpdate.updated(ret); }
	   		} 
	   		catch (Exception e)
	   		{
	   			address = "Address unknown";
	   			String ret = latLon + "\n" + address;
	   			if (onUpdate != null) { onUpdate.updated(ret); }
	   		}
    	}
    }
      
    public void onProviderDisabled(String provider) {
    // required for interface, not used
    }
      
    public void onProviderEnabled(String provider) {
    // required for interface, not used
    }
      
    public void onStatusChanged(String provider, int status,
    		Bundle extras) {
    // required for interface, not used
    }
    
    public void Pause()
    {
    	locmgr.removeUpdates(this);
    }
    
    public void Resume()
    {
    	locmgr.requestLocationUpdates(LocationManager.GPS_PROVIDER, 0, 100, this);
    }

	public OnUpdateListener getOnUpdate() {
		return onUpdate;
	}

	public void setOnUpdate(OnUpdateListener onUpdate) {
		this.onUpdate = onUpdate;
	}
	
}
