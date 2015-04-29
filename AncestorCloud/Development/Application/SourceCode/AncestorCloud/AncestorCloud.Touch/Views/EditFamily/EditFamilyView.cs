
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class EditFamilyView : UIViewController
	{
		public EditFamilyView () : base ("EditFamilyView", null)
		{
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
			SetNavigation ();

			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void SetNavigation()
		{
			//this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB (255, 255, 255);
		}

		partial void CrossButtonTapped (NSObject sender)
		{
			this.View.RemoveFromSuperview();
		}
	}
}

