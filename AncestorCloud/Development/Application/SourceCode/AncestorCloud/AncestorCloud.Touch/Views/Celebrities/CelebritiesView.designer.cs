// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AncestorCloud.Touch
{
	[Register ("CelebritiesView")]
	partial class CelebritiesView
	{
		[Outlet]
		UIKit.UITableView CelebritiesTableVIew { get; set; }

		[Outlet]
		UIKit.UIButton MeImage { get; set; }

		[Outlet]
		UIKit.UISearchBar SearchViewController { get; set; }

		[Action ("AddMeButtonTapped:")]
		partial void AddMeButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (MeImage != null) {
				MeImage.Dispose ();
				MeImage = null;
			}

			if (CelebritiesTableVIew != null) {
				CelebritiesTableVIew.Dispose ();
				CelebritiesTableVIew = null;
			}

			if (SearchViewController != null) {
				SearchViewController.Dispose ();
				SearchViewController = null;
			}
		}
	}
}
