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
    [Register ("MatchView")]
    partial class MatchView
    {
        [Outlet]
        UIKit.UIView ContentView { get; set; }


        [Outlet]
        UIKit.UIButton FirstCrossButton { get; set; }


        [Outlet]
        UIKit.UIButton FirstImageButton { get; set; }


        [Outlet]
        UIKit.UIImageView FirstImageView { get; set; }


        [Outlet]
        UIKit.UIScrollView scrollViewObj { get; set; }


        [Outlet]
        UIKit.UIButton SecondCrossButton { get; set; }


        [Outlet]
        UIKit.UIButton SecondImageButton { get; set; }


        [Outlet]
        UIKit.UIImageView SecondImageView { get; set; }


        [Action ("firstCrossImg:")]
        partial void firstCrossImg (UIKit.UIButton sender);


        [Action ("FirstImageButtonTapped:")]
        partial void FirstImageButtonTapped (Foundation.NSObject sender);


        [Action ("FirstImageTapped:")]
        partial void FirstImageTapped (UIKit.UIButton sender);


        [Action ("MatchTapped:")]
        partial void MatchTapped (Foundation.NSObject sender);


        [Action ("secCrossImg:")]
        partial void secCrossImg (UIKit.UIButton sender);


        [Action ("SecondButtonImageTapped:")]
        partial void SecondButtonImageTapped (Foundation.NSObject sender);


        [Action ("SecondImageTapped:")]
        partial void SecondImageTapped (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ContentView != null) {
                ContentView.Dispose ();
                ContentView = null;
            }

            if (FirstCrossButton != null) {
                FirstCrossButton.Dispose ();
                FirstCrossButton = null;
            }

            if (FirstImageButton != null) {
                FirstImageButton.Dispose ();
                FirstImageButton = null;
            }

            if (FirstImageView != null) {
                FirstImageView.Dispose ();
                FirstImageView = null;
            }

            if (scrollViewObj != null) {
                scrollViewObj.Dispose ();
                scrollViewObj = null;
            }

            if (SecondCrossButton != null) {
                SecondCrossButton.Dispose ();
                SecondCrossButton = null;
            }

            if (SecondImageButton != null) {
                SecondImageButton.Dispose ();
                SecondImageButton = null;
            }

            if (SecondImageView != null) {
                SecondImageView.Dispose ();
                SecondImageView = null;
            }
        }
    }
}