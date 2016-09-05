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
    [Register ("ContactsView")]
    partial class ContactsView
    {
        [Outlet]
        UIKit.UITableView ContactsTableView { get; set; }


        [Outlet]
        UIKit.UIButton MeImage { get; set; }


        [Action ("AddButtonTapped:")]
        partial void AddButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (ContactsTableView != null) {
                ContactsTableView.Dispose ();
                ContactsTableView = null;
            }

            if (MeImage != null) {
                MeImage.Dispose ();
                MeImage = null;
            }
        }
    }
}