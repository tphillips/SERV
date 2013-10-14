package uk.org.serv.servrider;

import android.net.Uri;
import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import android.widget.ToggleButton;

public class ServActivity extends Activity {

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_serv);
    }
    
    @Override
    protected void onResume()
    {
    	super.onResume();
    	if (ServApplication.getAppState(this).getRider() == null || ServApplication.getAppState(this).getRider().equals(""))
    	{
    		Intent i = new Intent(this, SettingsActivity.class);
    		startActivity(i);
    	}
    	this.setTitle(getString(R.string.title_activity_serv) + " [" + ServApplication.getAppState(this).getRider() + "]");
    	((EditText)this.findViewById(R.id.txtControllerNo)).setText(ServApplication.getAppState(this).getControllerNo());
        ((ToggleButton)this.findViewById(R.id.cmdOnDuty)).setChecked(ServApplication.getAppState(this).isOnDuty());
        ((Button)this.findViewById(R.id.cmdNewJob)).setClickable(ServApplication.getAppState(this).isOnDuty());
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_serv, menu);
        return true;
    }
    
    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
    	Intent i = new Intent(this, ViewLogActivity.class);
		startActivity(i);
    	return super.onOptionsItemSelected(item);
    }
    
    DialogInterface.OnClickListener dialogClickListener = new DialogInterface.OnClickListener() {
        //@Override
        public void onClick(DialogInterface dialog, int which) {
            switch (which){
            case DialogInterface.BUTTON_POSITIVE:
            	Intent callIntent = new Intent(Intent.ACTION_CALL);
    			callIntent.setData(Uri.parse(getString(R.string.servNowNumber)));
    			startActivity(callIntent);
                break;
            case DialogInterface.BUTTON_NEGATIVE:
                break;
            }
        }
    };
    
    public void onDutyClicked(View sender) 
    {
    	if (((ToggleButton)sender).isChecked())
    	{
    	    AlertDialog.Builder builder = new AlertDialog.Builder(this);
    	    builder.setMessage(getString(R.string.callServConfirmation)).setPositiveButton(getString(R.string.yes), dialogClickListener)
    	        .setNegativeButton(getString(R.string.callServNo), dialogClickListener).show();
    	    ((Button)this.findViewById(R.id.cmdNewJob)).setClickable(true);
    	    ServApplication.getAppState(this).setOnDuty(true);
    	}
    	else
    	{
    		((Button)this.findViewById(R.id.cmdNewJob)).setClickable(false);
    		ServApplication.getAppState(this).setOnDuty(false);
    	}
    }
    
    public void lastCallClicked(View sender) 
    {
    	try
    	{
    	String[] strFields = {
    	        android.provider.CallLog.Calls.NUMBER, 
    	        android.provider.CallLog.Calls.TYPE,
    	        android.provider.CallLog.Calls.CACHED_NAME,
    	        android.provider.CallLog.Calls.CACHED_NUMBER_TYPE
    	        };
    	String strOrder = android.provider.CallLog.Calls.DATE + " DESC"; 
    	Cursor mCallCursor = getContentResolver().query(
    	        android.provider.CallLog.Calls.CONTENT_URI,
    	        strFields,
    	        null,
    	        null,
    	        strOrder
    	        );
    	mCallCursor.moveToFirst();
    	((EditText)this.findViewById(R.id.txtControllerNo)).setText(mCallCursor.getString(0));
    	} catch(Exception e){}
    }
    
    public void newCallClicked(View sender) 
    {
    	ServApplication.getAppState(this).NewJob();
    	String no = ((EditText)this.findViewById(R.id.txtControllerNo)).getText().toString();
    	if (no.equals(""))
    	{
    		Toast.makeText(this, getText(R.string.enterControllersNumber), Toast.LENGTH_LONG).show();
    		return;
    	}
    	ServApplication.getAppState(this).setControllerNo(no);
    	Intent i = new Intent(this, NewJobActivity.class);
		startActivity(i);
    }
    
}
