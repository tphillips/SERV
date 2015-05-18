using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SERV
{
	[Activity(Label = "Login")]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView (Resource.Layout.Login);
			Button cmdLogin = FindViewById<Button>(Resource.Id.cmdLogin);
			cmdLogin.Click += LoginClicked;
			// Create your application here
		}

		void LoginClicked (object sender, EventArgs e)
		{
			LoginWithDialog(FindViewById<TextView>(Resource.Id.txtEmail).Text, FindViewById<TextView>(Resource.Id.txtPass).Text);

		}

		private void LoginWithDialog(string username, string password)
		{
			ProgressDialog dlg = new ProgressDialog(this);
			dlg.SetTitle("Please wait . . .");
			dlg.SetMessage("Communicating with SERV . . .");
			dlg.SetCancelable(false);
			dlg.Show();
			system.servssl.org.uk.MobileService svc = new SERV.system.servssl.org.uk.MobileService();
			svc.StartSessionCompleted += delegate(object sender, SERV.system.servssl.org.uk.StartSessionCompletedEventArgs args)
			{
				if (args.Result != null)
				{
					ISharedPreferences prefs = Application.Context.GetSharedPreferences("SERV", FileCreationMode.Private); 
					ISharedPreferencesEditor editor = prefs.Edit();
					editor.PutString("username", username);
					editor.PutString("password", password);
					editor.PutString("membername", args.Result.Member.FirstName);
					editor.Commit();
					RunOnUiThread(new Action(delegate
					{
						dlg.Hide();
						var intent = new Intent(this, typeof(MainActivity));
						StartActivity(intent);
						Finish();
					}));
				}
				else
				{
					RunOnUiThread(new Action(delegate
						{
							dlg.Hide();
							Toast t = Toast.MakeText(this, "Invalid email address or password!", ToastLength.Long);
							t.Show();
						}));
				}
			};
			try 
			{
				svc.StartSessionAsync(username, password);
			}
			catch
			{
				RunOnUiThread(new Action(delegate
				{
					dlg.Hide();
					Toast t = Toast.MakeText(this, "Check connection!", ToastLength.Long);
					t.Show();
				}));
			}
		}
	}
}

