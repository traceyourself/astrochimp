
using System;

using Foundation;
using UIKit;
using AncestorCloud.Shared.ViewModels;

namespace AncestorCloud.Touch
{
	public partial class AddFamilyView : BaseViewController

	{

		UIDatePicker picker;

		public AddFamilyView () : base ("AddFamilyView", null)
		{
		}

		public new AddFamilyViewModel ViewModel
		{
			get { return base.ViewModel as AddFamilyViewModel; }
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
			setnavigation ();
		
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public void setnavigation()
		{
			this.Title="Add Family";

		}
			

	}
}

