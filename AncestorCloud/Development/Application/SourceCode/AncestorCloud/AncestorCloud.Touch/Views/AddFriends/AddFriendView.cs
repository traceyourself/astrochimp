
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
		#region Life Cycle Methods
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

			SetNavBar ();

			SetLeftBarButtonItem ();
	
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillDisappear (bool animated)
		{
			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 

			}
			base.ViewWillDisappear (animated);

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			App.controllerTypeRef = ControllerType.Secondary;
		}

		#endregion

		#region Navigation Bar Methods

		private void SetNavBar()
		{
			this.NavigationController.NavigationBarHidden = false;
			this.Title = Utility.LocalisedBundle ().LocalizedString ("AddFriendText", "");
			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor ();
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = Themes.TitleTextColor()});
		}

		private void SetLeftBarButtonItem()
		{
			UIImage image = UIImage.FromFile (StringConstants.WHITECROSS);

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);

			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.Close();

					})
				, true);
		}

		#endregion

		#region Button Tap handler

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

		#endregion
	}
}

