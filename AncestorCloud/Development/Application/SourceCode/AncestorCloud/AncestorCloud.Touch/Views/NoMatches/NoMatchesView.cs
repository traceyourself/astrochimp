﻿
using System;

using Foundation;
using UIKit;
using System.Drawing;
using AncestorCloud.Shared;

namespace AncestorCloud.Touch
{
	public partial class NoMatchesView : UIViewController
	{
		public NoMatchesView () : base ("NoMatchesView", null)
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
//			this.Title = Utility.LocalisedBundle ().LocalizedString ("PastMatchText", "");
//			this.NavigationItem.TitleView = new MyPastMatchTitleView (this.Title,new RectangleF(0,0,150,20));
//			this.NavigationController.NavigationBar.BarTintColor = Themes.NavBarTintColor();


		}

		public override void ViewWillAppear (bool animated)
		{
			//this.NavigationController.NavigationBarHidden = false;

			App.controllerTypeRef = ControllerType.Secondary;
		}

		public override void ViewWillDisappear (bool animated)
		{
			//this.NavigationController.NavigationBarHidden = true;
		}
	}
}

