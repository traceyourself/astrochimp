
using System;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using UIKit;
using FlyoutNavigation;
using MonoTouch.Dialog; 
using System.Linq;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{

	public partial class FamilyView : BaseViewController
	{
 
		public FamilyView () : base ("FamilyView", null)
		{
			
		}
		public new FamilyViewModel ViewModel
		{
			get { return base.ViewModel as FamilyViewModel; }
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
			SetFamilyItem ();
			//Flyout ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetFamilyItem()
		{
			
			this.Title = "My Family";
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			this.NavigationItem.SetHidesBackButton (true, false);
			//this.NavigationItem.TitleView = new MyTitleView ();
			this.NavigationController.NavigationBarHidden = true;

		}

		#region MatchTapped
		partial void AddFamilyButtonTapped (NSObject sender)
		{

			ViewModel.ShowEditViewModel();
			ViewModel.Close();
		}

		#endregion

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

