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
            if (CelebritiesTableVIew != null) {
                CelebritiesTableVIew.Dispose ();
                CelebritiesTableVIew = null;
            }

            if (MeImage != null) {
                MeImage.Dispose ();
                MeImage = null;
            }

            if (SearchViewController != null) {
                SearchViewController.Dispose ();
                SearchViewController = null;
            }
        }
    }
}