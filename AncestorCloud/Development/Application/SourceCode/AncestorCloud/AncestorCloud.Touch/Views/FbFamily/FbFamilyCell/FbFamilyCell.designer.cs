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
    [Register ("FbFamilyCell")]
    partial class FbFamilyCell
    {
        [Outlet]
        UIKit.UIImageView CheckedButtonTapped { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel RelationLabel { get; set; }


        [Outlet]
        UIKit.UIImageView UncheckedButtonTapped { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CheckedButtonTapped != null) {
                CheckedButtonTapped.Dispose ();
                CheckedButtonTapped = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (RelationLabel != null) {
                RelationLabel.Dispose ();
                RelationLabel = null;
            }

            if (UncheckedButtonTapped != null) {
                UncheckedButtonTapped.Dispose ();
                UncheckedButtonTapped = null;
            }
        }
    }
}