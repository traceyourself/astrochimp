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
    [Register ("CelebritiesCell")]
    partial class CelebritiesCell
    {
        [Outlet]
        UIKit.UIButton CelbImage { get; set; }


        [Outlet]
        UIKit.UIImageView CelebImageView { get; set; }


        [Outlet]
        UIKit.UILabel LastName { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Action ("AddCelbButtonTapped:")]
        partial void AddCelbButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (CelebImageView != null) {
                CelebImageView.Dispose ();
                CelebImageView = null;
            }

            if (LastName != null) {
                LastName.Dispose ();
                LastName = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }
        }
    }
}