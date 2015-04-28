
using System;

using Foundation;
using UIKit;

namespace AncestorCloud.Touch
{
	public partial class ResearchHelpView : BaseViewController
	{
		public ResearchHelpView () : base ("ResearchHelpView", null)
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
			this.NavigationController.NavigationBarHidden = true;
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

