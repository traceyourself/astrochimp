using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using AncestorCloud.Shared.ViewModels;
using Cirrious.CrossCore;
using AncestorCloud.Shared;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;
using System.Drawing;

namespace AncestorCloud.Touch
{
	public partial class FbFamilyView : BaseViewController
	{
		public FbFamilyView () : base ("FbFamilyView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public new FbFamilyViewModel ViewModel
		{
			get { return base.ViewModel as FbFamilyViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetTableView ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}


		public void SetTableView()
		{

			this.Title= "My Family";
			var source = new FbFamilyTableViewSource (fbFamilyTableView);
			//var source = new MvxSimpleTableViewSource(fbFamilyTableView, FbFamilyCell.Key, FbFamilyCell.Key);
			fbFamilyTableView.Source = source;

			fbFamilyTableView.Delegate = new FbFamilyTableViewDelegate (ViewModel.FamilyList);

			var set = this.CreateBindingSet<FbFamilyView , FbFamilyViewModel> ();
			set.Bind (source).To (vm => vm.FamilyList);

			set.Bind (NextButton).To (vm => vm.NextButtonCommand);
			set.Apply ();
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (178, 45, 116);
			this.NavigationItem.SetHidesBackButton (true, false);
			this.NavigationItem.TitleView = new MyTitleView (this.Title,new RectangleF(0,0,150,20));

			UIImage image = UIImage.FromFile ("action_menu.png");

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						{
							//message to show the menu
							var messenger = Mvx.Resolve<IMvxMessenger>();
							messenger.Publish(new ToggleFlyoutMenuMessage(this));
						}

					})
				, true);

		}

//		public override void ViewWillAppear (bool animated)
//		{
//
//			if (!NavigationController.ViewControllers.Contains (this)) {
//				var messenger = Mvx.Resolve<IMvxMessenger> ();
//				messenger.Publish (new NavigationBarHiddenMessage (this, false)); 
//
//			}
//			base.ViewWillDisappear (animated);
//		}
//
	}
}

