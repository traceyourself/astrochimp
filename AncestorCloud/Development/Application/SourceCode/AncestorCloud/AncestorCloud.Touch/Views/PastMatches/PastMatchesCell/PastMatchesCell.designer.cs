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
    [Register ("PastMatchesCell")]
    partial class PastMatchesCell
    {
        [Outlet]
        UIKit.UILabel DegreeLabel { get; set; }


        [Outlet]
        UIKit.UIImageView FirstImage { get; set; }


        [Outlet]
        UIKit.UILabel MyNameLabel { get; set; }


        [Outlet]
        UIKit.UIImageView OtherImageView { get; set; }


        [Outlet]
        UIKit.UILabel OtherNameLabel { get; set; }


        [Outlet]
        UIKit.UIImageView SecondImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DegreeLabel != null) {
                DegreeLabel.Dispose ();
                DegreeLabel = null;
            }

            if (FirstImage != null) {
                FirstImage.Dispose ();
                FirstImage = null;
            }

            if (MyNameLabel != null) {
                MyNameLabel.Dispose ();
                MyNameLabel = null;
            }

            if (OtherImageView != null) {
                OtherImageView.Dispose ();
                OtherImageView = null;
            }

            if (OtherNameLabel != null) {
                OtherNameLabel.Dispose ();
                OtherNameLabel = null;
            }

            if (SecondImage != null) {
                SecondImage.Dispose ();
                SecondImage = null;
            }
        }
    }
}