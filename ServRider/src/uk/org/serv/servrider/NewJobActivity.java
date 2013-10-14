package uk.org.serv.servrider;

import uk.org.serv.servrider.R.id;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.Toast;

public class NewJobActivity extends Activity {

	public NewJobActivity() {
		
	}
	
	@Override
	public void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_newjob);
		Spinner spinner = (Spinner) findViewById(R.id.spinFrom);
		ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
		     R.array.locations, android.R.layout.simple_spinner_item);
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner.setAdapter(adapter);
		spinner = (Spinner) findViewById(R.id.spinTo);
		spinner.setAdapter(adapter);
		spinner = (Spinner) findViewById(R.id.spinBoxes);
		adapter = ArrayAdapter.createFromResource(this,
		     R.array.noBoxes, android.R.layout.simple_spinner_item);
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		spinner.setAdapter(adapter);
	}
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
	    getMenuInflater().inflate(R.menu.onjobmenu, menu);
	    return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item)
	{
		if (!setDetails()) { return false; }
		Intent i = new Intent(this, OnRouteToPickupActivity.class);
		startActivity(i);
		return super.onOptionsItemSelected(item);
	}
    
	private boolean setDetails()
	{
		ServApplication.getAppState(this).setJobFrom(((Spinner)this.findViewById(id.spinFrom)).getSelectedItem().toString());
		ServApplication.getAppState(this).setJobTo(((Spinner)this.findViewById(id.spinTo)).getSelectedItem().toString());
		if (ServApplication.getAppState(this).getJobTo().equals(ServApplication.getAppState(this).getJobFrom()))
		{
			Toast.makeText(this, getText(R.string.destinationMustDiffer), Toast.LENGTH_LONG).show();
			return false;
		}
		return true;
	} 
	
	public void cmdDepartClicked(View sender) 
	{
		if (!setDetails()) { return ; }
		//String message = getString(R.string.goingToPickupSMS) + " " + ServApplication.getAppState(this).getJobFrom();
		//Shared.SendMessageToController(message, this, ServApplication.getAppState(this).getRider());
		Intent i = new Intent(this, OnRouteToPickupActivity.class);
		startActivity(i);
	}

}
