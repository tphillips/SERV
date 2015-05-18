using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Android.Locations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERV
{
	[Activity (Label = "SERV", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener
	{

		Location currentLocation;
		LocationManager locationManager;
		string locationProvider;
		private string username;
		private string password;
		private string membername;
		private TextView lblLocation;
		private TextView lblController;
		private TextView lblBulletins;
		private TextView lblShift;
		private Switch sw;
		system.servssl.org.uk.Member controller;
		ProgressDialog dlg;
		CookieContainer cookieContainer = new CookieContainer();
		system.servssl.org.uk.MobileService svc = new SERV.system.servssl.org.uk.MobileService();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			svc.CookieContainer = cookieContainer;
			if (!IsLoggedIn())
			{
				var intent = new Intent(this, typeof(LoginActivity));
				StartActivity(intent);
				Finish();
				return;
			}
			InitializeLocationManager();
			this.Title = "SERV - " + membername;
			lblLocation = FindViewById<TextView> (Resource.Id.lblLocation);
			lblController = FindViewById<TextView> (Resource.Id.lblController);
			lblBulletins = FindViewById<TextView> (Resource.Id.lblBulletins);
			lblShift = FindViewById<TextView> (Resource.Id.lblShift);
			sw = FindViewById<Switch> (Resource.Id.swDuty);
			sw.CheckedChange += OnDutyCheckedChanged;
			new Thread(new ThreadStart(GetCurrentInfo)).Start();
		}

		protected override void OnResume()
		{
			base.OnResume();
			new Thread(new ThreadStart(GetCurrentInfo)).Start();
		}

		void GetCurrentInfo()
		{
			RunOnUiThread(new Action(delegate
			{
				lblController.Text = " . . .";
				lblShift.Text = "";
				lblBulletins.Text = "";
			}));
			try
			{
				if (svc.StartSession(username, password) != null)
				{
					controller = svc.GetCurrentController();
					if (controller != null)
					{
						RunOnUiThread(new Action(delegate
						{
							lblController.Text = controller.FirstName + " " + controller.LastName;
						}));
					}
					system.servssl.org.uk.CalendarEntry cal = svc.GetNextShift();
					if (cal != null)
					{
						RunOnUiThread(new Action(delegate
						{
							lblShift.Text = string.Format("Your next duty is a {0} shift on {1:dddd d MMM}",cal.CalendarName, cal.EntryDate);
						}));
					}
					string[] bulletins = svc.GetCalendarBulletins();
					StringBuilder b = new StringBuilder();
					foreach(string s in bulletins)
					{
						b.AppendLine(string.Format("! {0} !", s));
					}
					RunOnUiThread(new Action(delegate
					{
						lblBulletins.Text = b.ToString();
					}));
				} 
			}
			catch
			{
				RunOnUiThread(new Action(delegate
				{
					Toast t = Toast.MakeText(this, "Check connection!", ToastLength.Long);
					t.Show();
				}));
			}
		}

		bool IsLoggedIn()
		{
			ISharedPreferences prefs = Application.Context.GetSharedPreferences("SERV", FileCreationMode.Private); 
			username = prefs.GetString("username", "");
			password = prefs.GetString("password", "");
			membername = prefs.GetString("membername", "");
			if (membername == "")
			{
				return false;
			}
			return true;
		}

		void InitializeLocationManager()
		{
			locationManager = (LocationManager)GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
				{
					Accuracy = Accuracy.Fine
				};
			IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				locationProvider = String.Empty;
			}
		}

		async public void OnLocationChanged(Location location)
		{
			currentLocation = location;
			try
			{
				new Thread(new ParameterizedThreadStart(_BlindRequestAfterSessionRefresh)).Start(string.Format(
					"http://system.servssl.org.uk/Service/MobileService.asmx/UpdateLocation?lat={0}&lng={1}", 
					location.Latitude, location.Longitude));
			}
			catch{ }
			try
			{
				Geocoder geocoder = new Geocoder(this);
				IList<Address> addressList = await geocoder.GetFromLocationAsync(currentLocation.Latitude, currentLocation.Longitude, 10);
				Address address = addressList.FirstOrDefault();
				if (address != null)
				{
					lblLocation.Text = string.Format("{0}", address.SubLocality);
				}
				else
				{
					lblLocation.Text = "Unable to determine the address.";
				}
			}
			catch
			{
				lblLocation.Text = "Check connection!";
			}
		}

		public void OnProviderDisabled(string provider)
		{
			//throw new NotImplementedException();
		}

		public void OnProviderEnabled(string provider)
		{
			//throw new NotImplementedException();
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
			//throw new NotImplementedException();
		}

		void OnDutyCheckedChanged (object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			Switch swDuty = (Switch)sender;
            if (swDuty.Checked)
            {
				locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
                MakeBlindWebRequestWithDialog("http://system.servssl.org.uk/Service/MobileService.asmx/GoOnDuty");
				lblLocation.Text = " . . .";
            }
            else
            {
				locationManager.RemoveUpdates(this);
                MakeBlindWebRequestWithDialog("http://system.servssl.org.uk/Service/MobileService.asmx/GoOffDuty");
				lblLocation.Text = "Off";
            }
		}

		private void MakeBlindWebRequestWithDialog(string requestUrl)
        {
            dlg = new ProgressDialog(this);
            dlg.SetTitle("Please wait . . .");
            dlg.SetMessage("Communicating with SERV . . .");
            dlg.SetCancelable(false);
            dlg.Show();
			new Thread(new ParameterizedThreadStart(_BlindRequestAfterSessionRefreshAndCloseDialog)).Start(requestUrl);
        }

		private void _BlindRequestAfterSessionRefreshAndCloseDialog(object requestUrl)
		{
			BlindRequestAfterSessionRefresh((string)requestUrl, true);
		}

		private void _BlindRequestAfterSessionRefresh(object requestUrl)
		{
			BlindRequestAfterSessionRefresh((string)requestUrl, false);
		}

		private void BlindRequestAfterSessionRefresh(string requestUrl, bool closeDialog)
		{
			WebClient c = new WebClient();
			try
			{
				c.DownloadString(new Uri(string.Format("http://system.servssl.org.uk/Service/MobileService.asmx/StartSession?username={0}&password={1}", username, password)));
			}
			catch
			{
				RunOnUiThread(new Action(delegate
					{
						var intent = new Intent(this, typeof(LoginActivity));
						StartActivity(intent);
						Finish();
						return;
					}));
				return;
			}
			try
			{
				c.Headers.Add("Cookie", c.ResponseHeaders["Set-Cookie"]);
				c.DownloadString(new Uri((string)requestUrl));
				if (closeDialog)
				{
					RunOnUiThread(new Action(delegate
						{
							dlg.Hide();
						}));
				}
			}
			catch
			{
				RunOnUiThread(new Action(delegate
					{
						if (closeDialog)
						{
							dlg.Hide();
						}
						Toast t = Toast.MakeText(this, "Check connection!", ToastLength.Long);
						t.Show();
					}));
			}
		}

	}
}


