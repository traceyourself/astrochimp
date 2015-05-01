
using System;

using Foundation;
using UIKit;

using AncestorCloud.Shared;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;

namespace AncestorCloud.Touch
{
	public partial class AddFriendView : BaseViewController
	{
		public AddFriendView () : base ("AddFriendView", null)
		{
		}

		public new AddFriendViewModel ViewModel
		{
			get { return base.ViewModel as AddFriendViewModel; }
			set { base.ViewModel = value; }
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

			this.NavigationController.NavigationBarHidden = false;


			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void CelebritiesButtonTapped (NSObject sender)
		{
			ViewModel.ShowCelebrities();
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

