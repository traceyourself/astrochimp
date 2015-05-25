using System;
using Cirrious.MvvmCross.Droid.Views;
using AncestorCloud.Shared;
using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Android.Gms.Analytics;
using Android.Views;
using Android.Views.InputMethods;
using Android.Content;
using Android.Content.PM;


namespace AncestorCloud.Droid
{
	public class BaseActivity : MvxActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			try{
				base.OnCreate (bundle);
				Utilities.CurrentActiveActivity = this;
			}catch(Exception e){
				System.Diagnostics.Debug.WriteLine (e.InnerException);
			}

			try{
				Tracker t = GetTracker ();
				//t.SetScreenName (this.Title);
			}catch(Exception e){
				Mvx.Trace(e.StackTrace);
			}

			Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

			//Window.SetSoftInputMode(WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);

		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			HideKeyboard (this);
			return false;
		}

		public static void HideKeyboard(Activity act) {
			InputMethodManager inputMethodManager = act.GetSystemService(Context.InputMethodService) as InputMethodManager;
			inputMethodManager.HideSoftInputFromWindow(act.CurrentFocus.WindowToken, HideSoftInputFlags.None);
		}

		public Tracker GetTracker()
		{	
			try{
				GoogleAnalytics analytics = GoogleAnalytics.GetInstance(this);
				return analytics.NewTracker (Resource.Xml.global_tracker);
			}catch(Exception e){
				return null;
				Mvx.Trace(e.StackTrace);
			}
		}

	}
}