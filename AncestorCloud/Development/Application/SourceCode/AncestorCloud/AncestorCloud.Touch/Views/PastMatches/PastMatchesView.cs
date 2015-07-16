using System;
using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared.ViewModels;
using System.Drawing;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using AncestorCloud.Core;

namespace AncestorCloud.Touch
{
	public partial class PastMatchesView : BaseViewController
	{

		private MvxSubscriptionToken PastMatchToken;

		IMvxMessenger _messenger = Mvx.Resolve<IMvxMessenger>();

		public PastMatchesView () : base ("PastMatchesView", null)
		{
		}

		public new PastMatchesViewModel ViewModel
		{
			get { return base.ViewModel as PastMatchesViewModel; }
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
			ViewModel.GetPastMatchesData ();
			AddEvent ();
			//LoadView ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetTableView()
		{

			this.Title = Utility.LocalisedBundle ().LocalizedString ("PastMatchText","");
			var source = new PastMatchesTableSoure (PastMatchesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			PastMatchesTableVIew.Source = source;

			float width = (float)UIScreen.MainScreen.ApplicationFrame.Size.Width;


			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes ()
				{ TextColor = Themes.TitleTextColor() });

			this.NavigationController.NavigationBar.TintColor=Themes.TitleTextColor();

			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();


			if (width <= 320f) {
				this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title,new RectangleF(0,0,130,20));
			} else if (width >= 321f && width <=375) {
				this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title, new RectangleF (0, 0, 150, 20));
			} else {
				this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title, new RectangleF (0, 0, 180, 20));
			}
	

		}

		private void AddEvent()
		{
			PastMatchToken = _messenger.SubscribeOnMainThread<PastMatchesLoadedMessage>(Message => this.LoadView());
		}

		void RemoveMessengers()
		{
			if(PastMatchToken != null)
				_messenger.Unsubscribe<PastMatchesLoadedMessage> (PastMatchToken);
		}

		private void LoadView()
		{
			
			if (ViewModel.PastMatchesList != null) {
				CreateMyPastMatchTable ();
			} else {
				NoMatchesView _NoMatchesView = new  NoMatchesView ();
				this.View.AddSubview (_NoMatchesView.View);
			}



		}

		#region Binding

		private void CreateMyPastMatchTable()
		{
			var source = new PastMatchesTableSoure (PastMatchesTableVIew);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			PastMatchesTableVIew.Source = source;

			var set = this.CreateBindingSet<PastMatchesView , PastMatchesViewModel> ();
			set.Bind (source).To (vm => vm.PastMatchesList);
			set.Apply ();
		}

		#endregion

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.NavigationBarHidden = false;
			App.controllerTypeRef = ControllerType.Secondary;

		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			this.NavigationController.NavigationBarHidden = true;
			RemoveMessengers ();
		}
	}
}

