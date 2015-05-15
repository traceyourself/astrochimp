
using System;

using Foundation;
using UIKit;

using AncestorCloud.Shared;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;
using Cirrious.MvvmCross.Touch.Views;

namespace AncestorCloud.Touch
{
	public partial class AddFriendView : BaseViewController,IMvxModalTouchView
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
			this.Title="Select Someone to Match";

			UIImage image = UIImage.FromFile ("cross_white.png");

			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (68, 172, 176);

			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });


			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.Close();

					})
				, true);
	
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void CelebritiesButtonTapped (NSObject sender)
		{
			ViewModel.ShowCelebrities();
			ViewModel.CloseCommand.Execute(null);
		}

		partial void FacebookButtonTapped (NSObject sender)
		{
			ViewModel.ShowFacebookFriend();
			ViewModel.CloseCommand.Execute(null);
		}

		partial void ContactButtonTapped (NSObject sender)
		{
			ViewModel.ShowContacts();
			ViewModel.CloseCommand.Execute(null);
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

