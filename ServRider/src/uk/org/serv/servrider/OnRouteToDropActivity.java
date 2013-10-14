package uk.org.serv.servrider;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.CheckBox;

public class OnRouteToDropActivity extends Activity {

	public OnRouteToDropActivity() {
		
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) 
	{
	    super.onCreate(savedInstanceState);
	    setContentView(R.layout.activity_onroutetodrop);
	    ((CheckBox)this.findViewById(R.id.chkProceedToDrop)).setText(getString(R.string.proceedTo) + ServApplication.getAppState(this).getJobTo());
	    /*
	    ((TextView)this.findViewById(R.id.lblLocation)).setText(ServApplication.getAppState(this).getLocation().getLatLon() + "\n" + ServApplication.getAppState(this).getLocation().getAddress());
		ServApplication.getAppState(this).getLocation().setOnUpdate(new OnUpdateListener()
		{
			@Override
			public void updated(String location) {
				((TextView)OnRouteToDropActivity.this.findViewById(R.id.lblLocation)).setText(location);
			}
		});
		*/
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
                	OnRouteToDropActivity.this.finish();
                }
            }
        }).setNegativeButton(getString(R.string.no), null).show();
	}
	
	@Override
	public void onStart()
	{
		super.onStart();
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
	    getMenuInflater().inflate(R.menu.onjobmenu, menu);
	    return true;
	}
	
	@Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
		Intent i = new Intent(this, OnRouteHomeActivity.class);
		startActivity(i);
    	return super.onOptionsItemSelected(item);
    }
	
	public void cmdLateClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.Delayed);
		Shared.SendMessageToController(getString(R.string.runningLateSMS), this, ServApplication.getAppState(this).getRider());
    }
	
	public void cmdHeadingHomeClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.HeadingHome);
		Shared.SendMessageToController(getString(R.string.goingHomeSMS), this, ServApplication.getAppState(this).getRider());
		Intent i = new Intent(this, OnRouteHomeActivity.class);
		startActivity(i);	
    }
	
	public void cmdHeadingBunkhouseClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.HeadingToBunkhouse);
		Shared.SendMessageToController(getString(R.string.goingToBunkhouseSMS), this, ServApplication.getAppState(this).getRider());
		Intent i = new Intent(this, OnRouteHomeActivity.class);
		startActivity(i);
    }
	
	public void cmdWaitingClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.WaitingAtDrop);
		Shared.SendMessageToController(getString(R.string.waitingHereSMS), this, ServApplication.getAppState(this).getRider());
		Intent i = new Intent(this, ServActivity.class);
		i.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(i);
    }
	

}
