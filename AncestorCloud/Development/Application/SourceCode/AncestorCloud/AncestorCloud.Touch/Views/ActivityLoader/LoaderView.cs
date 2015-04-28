
using System;

using Foundation;
using UIKit;


namespace AncestorCloud.Touch
{
	public partial class LoaderView : UIViewController
	{
		public LoaderView () : base ("LoaderView", null)
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

			ActivityLoader.StartAnimating ();
			BlackView.Layer.CornerRadius=5f;
			BlackView.Layer.MasksToBounds = true;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillDisappear(bool animated)
		{
			ActivityLoader.StopAnimating ();
		}


	}
}

