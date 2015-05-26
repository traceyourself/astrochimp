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
			base.DidReceiveMemoryWarning ();
		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ActivityLoader.StartAnimating ();
			BlackView.Layer.CornerRadius=5f;
			BlackView.Layer.MasksToBounds = true;
			BlackView.Center =  new CoreGraphics.CGPoint((float) UIScreen.MainScreen.ApplicationFrame.Width/2,(float) UIScreen.MainScreen.ApplicationFrame.Height/2) ;

		}

		public override void ViewWillDisappear(bool animated)
		{
			ActivityLoader.StopAnimating ();
		}


	}
}

