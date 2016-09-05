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
    [Register ("MyFamilyTableCell")]
    partial class MyFamilyTableCell
    {
        [Outlet]
        UIKit.UILabel BirthLabel { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UILabel RelationLabel { get; set; }


        [Action ("EditButtonTapped:")]
        partial void EditButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (BirthLabel != null) {
                BirthLabel.Dispose ();
                BirthLabel = null;
            }

            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (RelationLabel != null) {
                RelationLabel.Dispose ();
                RelationLabel = null;
            }
        }
    }
}