// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

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
            if (firstTabImageView != null) {
                firstTabImageView.Dispose ();
                firstTabImageView = null;
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

            if (secondTabImageView != null) {
                secondTabImageView.Dispose ();
                secondTabImageView = null;
            }

            if (segmentControlObj != null) {
                segmentControlObj.Dispose ();
                segmentControlObj = null;
            }

            if (thirdTabImageVIew != null) {
                thirdTabImageVIew.Dispose ();
                thirdTabImageVIew = null;
            }
        }
    }
}