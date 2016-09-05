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
    [Register ("HomePage")]
    partial class HomePage
    {
        [Outlet]
        UIKit.UICollectionView collectionViewObj { get; set; }


        [Outlet]
        UIKit.UIButton loginButton { get; set; }


        [Outlet]
        UIKit.UIPageControl pageObj { get; set; }


        [Outlet]
        UIKit.UIButton signUpButton { get; set; }


        [Outlet]
        UIKit.UILabel swipeToLearnLabel { get; set; }


        [Action ("loginClicked:")]
        partial void loginClicked (Foundation.NSObject sender);


        [Action ("signUpClicked:")]
        partial void signUpClicked (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (collectionViewObj != null) {
                collectionViewObj.Dispose ();
                collectionViewObj = null;
            }

            if (loginButton != null) {
                loginButton.Dispose ();
                loginButton = null;
            }

            if (pageObj != null) {
                pageObj.Dispose ();
                pageObj = null;
            }

            if (signUpButton != null) {
                signUpButton.Dispose ();
                signUpButton = null;
            }

            if (swipeToLearnLabel != null) {
                swipeToLearnLabel.Dispose ();
                swipeToLearnLabel = null;
            }
        }
    }
}