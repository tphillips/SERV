package uk.org.serv.servrider;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;

public class OnRouteHomeActivity extends Activity {

	public OnRouteHomeActivity() {
		
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) 
	{
	    super.onCreate(savedInstanceState);
	    setContentView(R.layout.activity_onroutehome);
	    /*
	    ((TextView)this.findViewById(R.id.lblLocation)).setText(ServApplication.getAppState(this).getLocation().getLatLon() + "\n" + ServApplication.getAppState(this).getLocation().getAddress());
		ServApplication.getAppState(this).getLocation().setOnUpdate(new OnUpdateListener()
		{
			@Override
			public void updated(String location) {
				((TextView)OnRouteHomeActivity.this.findViewById(R.id.lblLocation)).setText(location);
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
                	OnRouteHomeActivity.this.finish();
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
		Intent i = new Intent(this, ServActivity.class);
		i.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(i);
    	return super.onOptionsItemSelected(item);
    }
	
	public void cmdHomeClicked(View sender) 
    {
		ServApplication.getAppState(this).LogEvent(EventType.ArrivedHome);
		Shared.SendMessageToController(getString(R.string.arrivedHomeSMS), this, ServApplication.getAppState(this).getRider());
		Intent i = new Intent(this, ServActivity.class);
		i.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
		startActivity(i);
    }
	

}
