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
	[Register ("MyFamilyView")]
	partial class MyFamilyView
	{
		[Outlet]
		UIKit.UIImageView firstTabImageView { get; set; }

		[Outlet]
		UIKit.UITableView grandParentTableViewObj { get; set; }

		[Outlet]
		UIKit.UITableView greatGrandParentTableVIewObj { get; set; }

		[Outlet]
		UIKit.UITableView myFamilyTable { get; set; }

		[Outlet]
		UIKit.UILabel PercentageLabel { get; set; }

		[Outlet]
		UIKit.UIImageView secondTabImageView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl segmentControlObj { get; set; }

		[Outlet]
		UIKit.UIImageView thirdTabImageVIew { get; set; }

		[Action ("CrossButtonTaped:")]
		partial void CrossButtonTaped (Foundation.NSObject sender);

		[Action ("SegmentControlTapped:")]
		partial void SegmentControlTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (segmentControlObj != null) {
				segmentControlObj.Dispose ();
				segmentControlObj = null;
			}

			if (firstTabImageView != null) {
				firstTabImageView.Dispose ();
				firstTabImageView = null;
			}

			if (secondTabImageView != null) {
				secondTabImageView.Dispose ();
				secondTabImageView = null;
			}

			if (thirdTabImageVIew != null) {
				thirdTabImageVIew.Dispose ();
				thirdTabImageVIew = null;
			}

			if (grandParentTableViewObj != null) {
				grandParentTableViewObj.Dispose ();
				grandParentTableViewObj = null;
			}

			if (greatGrandParentTableVIewObj != null) {
				greatGrandParentTableVIewObj.Dispose ();
				greatGrandParentTableVIewObj = null;
			}

			if (myFamilyTable != null) {
				myFamilyTable.Dispose ();
				myFamilyTable = null;
			}

			if (PercentageLabel != null) {
				PercentageLabel.Dispose ();
				PercentageLabel = null;
			}
		}
	}
}
