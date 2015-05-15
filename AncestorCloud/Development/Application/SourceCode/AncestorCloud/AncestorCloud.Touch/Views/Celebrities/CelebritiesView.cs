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


		public void SetTableView()
		{

			this.Title="Celebrities";
			var source = new CelebritiesTableSource (CelebritiesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			CelebritiesTableVIew.Source = source;

			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = UIColor.FromRGB (255,255,255) });

			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (68, 172, 176);

			//	this.NavigationItem.BackBarButtonItem.TintColor = UIColor.White;
			
			this.NavigationItem.TitleView = new MyCelebritiesTitleView (this.Title,new RectangleF(0,0,120,20));

			var set = this.CreateBindingSet<CelebritiesView , CelebritiesViewModel> ();
			set.Bind (source).To (vm => vm.CelebritiesList);
			set.Bind (SearchViewController).To (vm => vm.SearchKey).TwoWay();
			//set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			this.NavigationController.NavigationBarHidden = false;

			MeImage.Layer.CornerRadius = 22f;
			MeImage.ClipsToBounds = true;

		}

		public override void ViewWillDisappear (bool animated)
		{
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
			System.Diagnostics.Debug.WriteLine ("ADD BUTTON TAPPED :"  + celebs);

			ViewModel.CelebrityPlusClickHandler(celebs);
		}

		partial void AddMeButtonTapped (NSObject sender)
		{
			System.Diagnostics.Debug.WriteLine("MeButtonTapped");

			ViewModel.MePlusClicked();
		}
	}
}

