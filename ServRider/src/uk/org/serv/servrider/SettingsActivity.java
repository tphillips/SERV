package uk.org.serv.servrider;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class SettingsActivity extends Activity {
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);
        ((EditText)findViewById(R.id.txtName)).setText(ServApplication.getAppState(this).getRider());
    }
	
	public void cmdSaveClicked(View view)
	{
		if (((EditText)findViewById(R.id.txtName)).getText().toString().equals(""))
		{
			return;
		}
		ServApplication.getAppState(this).setRider(((EditText)findViewById(R.id.txtName)).getText().toString());
		this.finish();
	}

}
