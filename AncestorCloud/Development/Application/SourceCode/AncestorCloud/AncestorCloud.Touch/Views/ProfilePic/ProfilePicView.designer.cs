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
    [Register ("ProfilePicView")]
    partial class ProfilePicView
    {
        [Outlet]
        UIKit.UIView ContentView { get; set; }


        [Outlet]
        UIKit.UIButton ProfilePic { get; set; }


        [Outlet]
        UIKit.UIButton SkipButton { get; set; }


        [Action ("SkipButtonTapped:")]
        partial void SkipButtonTapped (Foundation.NSObject sender);


        [Action ("UploadButtonTapped:")]
        partial void UploadButtonTapped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (ContentView != null) {
                ContentView.Dispose ();
                ContentView = null;
            }

            if (ProfilePic != null) {
                ProfilePic.Dispose ();
                ProfilePic = null;
            }

            if (SkipButton != null) {
                SkipButton.Dispose ();
                SkipButton = null;
            }
        }
    }
}