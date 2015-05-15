
using System;

using Foundation;
using UIKit;
using AncestorCloud.Shared;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class HelpView : BaseViewController,IMvxModalTouchView
	{

		public People FamilyMember{ get; set;}

		public HelpView () : base ("HelpView", null)
		{
		}

		public new HelpViewModel ViewModel
		{
			get { return base.ViewModel as HelpViewModel; }
			set { base.ViewModel = value; }
		}
//
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

		partial void CrossButtonTapped (NSObject sender)
		{
			this.View.RemoveFromSuperview();
		}

		public void SetNavigation()
		{
			this.Title="Help";
			this.NavigationController.NavigationBarHidden = false;
			this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (64,172,176);


			UIImage image = UIImage.FromFile ("cross_white.png");

			image = image.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);


			this.NavigationItem.SetLeftBarButtonItem(
				new UIBarButtonItem(image
					, UIBarButtonItemStyle.Plain
					, (sender,args) => {
						ViewModel.Close();
					})
				, true);
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

