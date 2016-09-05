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
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        UIKit.UIView container { get; set; }


        [Outlet]
        UIKit.UITextField EmailTextFeild { get; set; }


        [Outlet]
        UIKit.UIButton Facebookbutton { get; set; }


        [Outlet]
        UIKit.UIButton LoginButton { get; set; }


        [Outlet]
        UIKit.UITextField PasswordTextFeild { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imagePassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSeparatorLeft { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSeparatorRight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelContinueWith { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelLoginWithEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelOr { get; set; }


        [Action ("FacebookButtonTapped:")]
        partial void FacebookButtonTapped (Foundation.NSObject sender);


        [Action ("LoginButtonTapped:")]
        partial void LoginButtonTapped (Foundation.NSObject sender);


        [Action ("PushButtonClicked:")]
        partial void PushButtonClicked (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (container != null) {
                container.Dispose ();
                container = null;
            }

            if (EmailTextFeild != null) {
                EmailTextFeild.Dispose ();
                EmailTextFeild = null;
            }

            if (Facebookbutton != null) {
                Facebookbutton.Dispose ();
                Facebookbutton = null;
            }

            if (imageEmail != null) {
                imageEmail.Dispose ();
                imageEmail = null;
            }

            if (imagePassword != null) {
                imagePassword.Dispose ();
                imagePassword = null;
            }

            if (imageSeparatorLeft != null) {
                imageSeparatorLeft.Dispose ();
                imageSeparatorLeft = null;
            }

            if (imageSeparatorRight != null) {
                imageSeparatorRight.Dispose ();
                imageSeparatorRight = null;
            }

            if (labelContinueWith != null) {
                labelContinueWith.Dispose ();
                labelContinueWith = null;
            }

            if (labelLoginWithEmail != null) {
                labelLoginWithEmail.Dispose ();
                labelLoginWithEmail = null;
            }

            if (labelOr != null) {
                labelOr.Dispose ();
                labelOr = null;
            }

            if (LoginButton != null) {
                LoginButton.Dispose ();
                LoginButton = null;
            }

            if (PasswordTextFeild != null) {
                PasswordTextFeild.Dispose ();
                PasswordTextFeild = null;
            }
        }
    }
}