using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Drawing;
using Cirrious.CrossCore;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace AncestorCloud.Touch
{
	public partial class CelebritiesView : BaseViewController
	{
		private MvxSubscriptionToken navigationMenuToken;

		public CelebritiesView () : base ("CelebritiesView", null)
		{
		}

		public new CelebritiesViewModel ViewModel
		{
			get { return base.ViewModel as CelebritiesViewModel; }
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
			Search ();

			AddEvents ();


			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			AppDelegate _delegate = (AppDelegate) UIApplication.SharedApplication.Delegate;

			UIImage image = _delegate.UIImageProfilePic ?? UIImage.FromBundle (StringConstants.NOIMAGE);

			MeImage.SetBackgroundImage (image, UIControlState.Normal);

			this.NavigationController.NavigationBarHidden = false;
		}


		public void SetTableView()
		{

			this.Title = Utility.LocalisedBundle ().LocalizedString ("CelebrityText", "");
			var source = new CelebritiesTableSource (CelebritiesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			CelebritiesTableVIew.Source = source;

			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = Themes.TitleTextColor() });

			this.NavigationController.NavigationBar.TintColor=Themes.TitleTextColor();

			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();

			//	this.NavigationItem.BackBarButtonItem.TintColor = UIColor.White;
			
			this.NavigationItem.TitleView = new MyCelebritiesTitleView (this.Title,new RectangleF(0,0,120,20));

			var set = this.CreateBindingSet<CelebritiesView , CelebritiesViewModel> ();
			set.Bind (source).To (vm => vm.CelebritiesList);
			set.Bind (SearchViewController).To (vm => vm.SearchKey).TwoWay();
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


		public void Search()
		{
			
		}


		private void AddEvents ()
		{
			
			var _messenger = Mvx.Resolve<IMvxMessenger>();

			navigationMenuToken = _messenger.SubscribeOnMainThread<MyAddButtonTappedMessage>(message => this.ShowAddEvent(message.FamilyMember));


		}

		public void ShowAddEvent(Celebrity celebs)
		{
			

			ViewModel.CelebrityPlusClickHandler(celebs);
		}

		partial void AddMeButtonTapped (NSObject sender)
		{
			
			//Console.WriteLine("CHECK");
			ViewModel.MePlusClicked();
		}
	}
}

