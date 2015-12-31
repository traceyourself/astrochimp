using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using GoogleAnalytics.iOS;
using AncestorCloud.Shared;
using TestFairyLib;
using Facebook.CoreKit;

namespace AncestorCloud.Touch
{ 
	[Register("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		UIWindow _window;

		LoaderView _loaderView;

		public IGAITracker Tracker;

		public UIImage UIImageProfilePic;

		public UIViewController currentController;


		public override bool FinishedLaunching(UIApplication application , NSDictionary launchOptions)
		{
			_window = new UIWindow(UIScreen.MainScreen.Bounds);
			_window.BackgroundColor = UIColor.Clear;

			var setup = new Setup(this, _window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();
		
//			var documents = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User) [0];
//			string mydocumentpath = documents.ToString();
//
//			System.Diagnostics.Debug.WriteLine("app dir: "+ mydocumentpath);

			RegisterGoogleAnalytics ();

			RegisterTestFairy ();

			_window.MakeKeyAndVisible();
		
			// This method verifies if you have been logged into the app before, and keep you logged in after you reopen or kill your app.
			return ApplicationDelegate.SharedInstance.FinishedLaunching (application, launchOptions);
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
		}

		#region RegisterTestFairy

		void RegisterTestFairy(){
			TestFairy.Begin (AppConstant.TESTFAIRYTOKEN);
		}

		#endregion

		#region RegisterGoogleAnalytics

		void RegisterGoogleAnalytics()
		{
			// Optional: set Google Analytics dispatch interval to e.g. 20 seconds.
			GAI.SharedInstance.DispatchInterval = 20;

			// Optional: automatically send uncaught exceptions to Google Analytics.
			GAI.SharedInstance.TrackUncaughtExceptions = true;

			// Initialize tracker.
			Tracker = GAI.SharedInstance.GetTracker (AppConstant.GATRACKINGID);
		}
		#endregion

		public void  ShowActivityLoader()
		{
			if (_loaderView == null) {

				_loaderView = new LoaderView ();

				_window.AddSubview (_loaderView.View);

			}

			_loaderView.View.Hidden = false;
		}

		public void HideActivityLoader()
		{
			_loaderView.View.Hidden = true;
		}

		#region


		#endregion
	}
}