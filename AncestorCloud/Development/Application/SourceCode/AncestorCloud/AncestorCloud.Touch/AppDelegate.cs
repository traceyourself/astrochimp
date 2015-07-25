using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using GoogleAnalytics.iOS;
using AncestorCloud.Shared;

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

			_window.MakeKeyAndVisible();
		
			return true;
		}
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