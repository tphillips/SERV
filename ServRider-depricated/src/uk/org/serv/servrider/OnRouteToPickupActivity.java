package uk.org.serv.servrider;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.widget.CheckBox;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;

public class OnRouteToPickupActivity extends Activity {

	public OnRouteToPickupActivity() {
		
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) 
	{
	    super.onCreate(savedInstanceState);
	    setContentView(R.layout.activity_onroutetopickup);
		((CheckBox)this.findViewById(R.id.chkProceedToPickup)).setText(getString(R.string.proceedTo) + ServApplication.getAppState(this).getJobFrom());
		/*
		((TextView)this.findViewById(R.id.lblLocation)).setText(ServApplication.getAppState(this).getLocation().getLatLon() + "\n" + ServApplication.getAppState(this).getLocation().getAddress());
		ServApplication.getAppState(this).getLocation().setOnUpdate(new OnUpdateListener()
		{
			@Override
			public void updated(String location) {
				((TextView)OnRouteToPickupActivity.this.findViewById(R.id.lblLocation)).setText(location);
			}
		});
		*/
	}
	
	@Override
	public void onStart()
	{
		super.onStart();
	}
	
	@Override
	public void onBackPressed()
	{
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
	    builder.setMessage(getString(R.string.goBackConfirm)).setPositiveButton(getString(R.string.yes), new DialogInterface.OnClickListener() {
            //@Override
            public void onClick(DialogInterface dialog, int which) {
                switch (which){
                case DialogInterface.BUTTON_POSITIVE:
                	OnRouteToPickupActivity.this.finish();
                }
            }
        }).setNegativeButton(getString(R.string.no), null).show();
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
	    getMenuInflater().inflate(R.menu.onjobmenu, menu);
	    return true;
	}
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
		Intent i = new Intent(this, OnRouteToDropActivity.class);
		startActivity(i);
    	return super.onOptionsItemSelected(item);
    }
	
	public void cmdLateClicked(View sender) 
    {
		
			ServApplication.getAppState(this).LogEvent(EventType.Delayed);
			Shared.SendMessageToController(getString(R.string.runningLateSMS), this, ServApplication.getAppState(this).getRider());
		
    }
	
	public void cmdDelayAtPickupClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.WaitingForPickup);
		Shared.SendMessageToController(getString(R.string.delayAtPickupSMS) + " " + ServApplication.getAppState(this).getJobFrom() + " " + getString(R.string.waiting), this, ServApplication.getAppState(this).getRider());
    }
	
	public void cmdArrivedAtPickupClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.ArrivedAtPickup);
		Shared.SendMessageToController(getString(R.string.arrivedAtPickupSMS) + " " + ServApplication.getAppState(this).getJobFrom(), this, ServApplication.getAppState(this).getRider());
    }
	
	public void cmdPickedUpClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.PickedUp);
		Shared.SendMessageToController(getString(R.string.goingToDropSMS) + " " + ServApplication.getAppState(this).getJobTo(), this, ServApplication.getAppState(this).getRider());
		Intent i = new Intent(this, OnRouteToDropActivity.class);
		startActivity(i);
    }
	
	

}
