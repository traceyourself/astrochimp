
using System;

using Foundation;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class HelpView : BaseViewController
	{
		public HelpView () : base ("HelpView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetNavigation ();

			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void CrossButonTapped (NSObject sender)
		{
			this.View.RemoveFromSuperview();
		}

		public void SetNavigation()
		{
			this.Title="Help";
			this.NavigationController.NavigationBarHidden = false;



		}
		public override void ViewWillDisappear (bool animated)
		{

			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 

			}
			base.ViewWillDisappear (animated);
		}

	
	}
}

