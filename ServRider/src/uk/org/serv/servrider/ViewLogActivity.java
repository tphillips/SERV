package uk.org.serv.servrider;

import android.app.Activity;
import android.os.Bundle;
import android.widget.EditText;

public class ViewLogActivity extends Activity {
	
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_viewlog);
        ((EditText)findViewById(R.id.editText1)).setText(ServApplication.getLog(this));
    }

}
