using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared.ViewModels;
using System.Drawing;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;
using Cirrious.CrossCore;

namespace AncestorCloud.Touch
{
	public partial class FacebookFriendView : BaseViewController
	{
		public FacebookFriendView () : base ("FacebookFriendView", null)
		{
		}

		public new FacebookFriendViewModel ViewModel
		{
			get { return base.ViewModel as FacebookFriendViewModel; }
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
			SetTableView ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetTableView()
		{

			this.Title = Utility.LocalisedBundle ().LocalizedString ("FbFriendText","");
			this.NavigationController.NavigationBar.TintColor=UIColor.FromRGB(255,255,255);
			var source = new FacebookFriendTableSource (FacebookFriendTableView);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			FacebookFriendTableView.Source = source;

			//this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title,new RectangleF(0,0,150,20));

			var set = this.CreateBindingSet<FacebookFriendView , FacebookFriendViewModel> ();
			this.NavigationItem.TitleView = new MyFacebookFriendTitleView (this.Title,new RectangleF(0,0,150,20));
			set.Bind (source).To (vm => vm.FacebookFriendList);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.CheckFacebookFriendCommand);
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();

			MeImage.Layer.CornerRadius = 22f;
			MeImage.ClipsToBounds = true;

		}

		public override void ViewWillDisappear (bool animated)
		{
			if (NavigationController == null) {
				base.ViewWillDisappear (animated);
				return;
			}
			if (!NavigationController.ViewControllers.Contains (this)) {
				var messenger = Mvx.Resolve<IMvxMessenger> ();
				messenger.Publish (new NavigationBarHiddenMessage (this, true)); 

			}
			base.ViewWillDisappear (animated);


		}


		partial void AddButtonTapped (NSObject sender)
		{

			ViewModel.MePlusClicked();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			AppDelegate _delegate = (AppDelegate) UIApplication.SharedApplication.Delegate;

			UIImage image = _delegate.UIImageProfilePic ?? UIImage.FromBundle (StringConstants.NOIMAGE);

			MeImage.SetBackgroundImage (image, UIControlState.Normal);

			this.NavigationController.NavigationBarHidden = false;
		}
	}


}

