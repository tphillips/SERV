package uk.org.serv.servrider;

import android.os.Bundle;
import android.os.Handler;
import android.view.Window;
import android.app.Activity;
import android.content.Intent;

public class SplashActivity extends Activity {

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_splash);
        //ServApplication.getAppState(this).setContext(this);
    }
    
    @Override
    protected void onResume()
    {
        super.onResume();
        
        new Handler().postDelayed(new Runnable()
        {
            //@Override
            public void run()
            {
                //Finish the splash activity so it can't be returned to.
                SplashActivity.this.finish();
                // Create an Intent that will start the main activity.
                Intent mainIntent = new Intent(SplashActivity.this, ServActivity.class);
                SplashActivity.this.startActivity(mainIntent);
            }
        }, 3000);
            
    }
    
}
