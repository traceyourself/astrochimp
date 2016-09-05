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
    [Register ("FacebookFriendCell")]
    partial class FacebookFriendCell
    {
        [Outlet]
        UIKit.UIButton fbFriendImage { get; set; }


        [Outlet]
        UIKit.UILabel NameLabel { get; set; }


        [Outlet]
        UIKit.UIImageView ProfilePic { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (NameLabel != null) {
                NameLabel.Dispose ();
                NameLabel = null;
            }

            if (ProfilePic != null) {
                ProfilePic.Dispose ();
                ProfilePic = null;
            }
        }
    }
}