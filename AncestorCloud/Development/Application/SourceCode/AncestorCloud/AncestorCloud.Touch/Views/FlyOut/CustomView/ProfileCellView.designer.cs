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
    [Register ("ProfileCellView")]
    partial class ProfileCellView
    {
        [Outlet]
        UIKit.UIImageView ProfilePic { get; set; }


        [Outlet]
        UIKit.UILabel UserName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ProfilePic != null) {
                ProfilePic.Dispose ();
                ProfilePic = null;
            }

            if (UserName != null) {
                UserName.Dispose ();
                UserName = null;
            }
        }
    }
}