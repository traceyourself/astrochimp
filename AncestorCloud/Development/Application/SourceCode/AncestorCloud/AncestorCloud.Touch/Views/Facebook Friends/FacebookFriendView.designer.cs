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
    [Register ("FacebookFriendView")]
    partial class FacebookFriendView
    {
        [Outlet]
        UIKit.UITableView FacebookFriendTableView { get; set; }


        [Outlet]
        UIKit.UIButton MeImage { get; set; }


        [Action ("AddButtonTapped:")]
        partial void AddButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (FacebookFriendTableView != null) {
                FacebookFriendTableView.Dispose ();
                FacebookFriendTableView = null;
            }

            if (MeImage != null) {
                MeImage.Dispose ();
                MeImage = null;
            }
        }
    }
}