using System;
using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;

namespace AncestorCloud.Touch
{
	public partial class BaseViewController : MvxViewController
	{
		public BaseViewController (String nibName, NSBundle bundle) : base (nibName, null)
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
			try{
				base.ViewDidLoad ();
			}
			catch (Exception e) {

				System.Diagnostics.Debug.WriteLine (e.InnerException);
			}

			SetExtentedLayout();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		#region Set Extended Layout Method
		 
		private void SetExtentedLayout()
		{
			this.EdgesForExtendedLayout = UIRectEdge.None;
		}

		#endregion


	}
}

