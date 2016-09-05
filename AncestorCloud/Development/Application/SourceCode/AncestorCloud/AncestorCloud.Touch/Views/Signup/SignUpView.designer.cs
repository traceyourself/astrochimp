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
    [Register ("SignUpView")]
    partial class SignUpView
    {
        [Outlet]
        UIKit.UIView _container { get; set; }


        [Outlet]
        UIKit.UITextField EmailTextField { get; set; }


        [Outlet]
        UIKit.UIButton Facebookbutton { get; set; }


        [Outlet]
        UIKit.UITextField LastNameTextField { get; set; }


        [Outlet]
        UIKit.UITextField NameTextFeild { get; set; }


        [Outlet]
        UIKit.UITextField PasswordTextField { get; set; }


        [Outlet]
        UIKit.UIButton SignUpButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel continueWithLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageEmailField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imagePasswordField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSeparatorLeft { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSeparatorRight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSignupFirstName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imageSignupLastName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelSignupWithEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel orLabel { get; set; }


        [Action ("FacebookButtonTapped:")]
        partial void FacebookButtonTapped (Foundation.NSObject sender);


        [Action ("SignUpButttonTapped:")]
        partial void SignUpButttonTapped (Foundation.NSObject sender);


        [Action ("T:")]
        partial void T (Foundation.NSObject sender);


        [Action ("TCButtonTaped:")]
        partial void TCButtonTaped (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (_container != null) {
                _container.Dispose ();
                _container = null;
            }

            if (continueWithLabel != null) {
                continueWithLabel.Dispose ();
                continueWithLabel = null;
            }

            if (EmailTextField != null) {
                EmailTextField.Dispose ();
                EmailTextField = null;
            }

            if (Facebookbutton != null) {
                Facebookbutton.Dispose ();
                Facebookbutton = null;
            }

            if (imageEmailField != null) {
                imageEmailField.Dispose ();
                imageEmailField = null;
            }

            if (imagePasswordField != null) {
                imagePasswordField.Dispose ();
                imagePasswordField = null;
            }

            if (imageSeparatorLeft != null) {
                imageSeparatorLeft.Dispose ();
                imageSeparatorLeft = null;
            }

            if (imageSeparatorRight != null) {
                imageSeparatorRight.Dispose ();
                imageSeparatorRight = null;
            }

            if (imageSignupFirstName != null) {
                imageSignupFirstName.Dispose ();
                imageSignupFirstName = null;
            }

            if (imageSignupLastName != null) {
                imageSignupLastName.Dispose ();
                imageSignupLastName = null;
            }

            if (labelSignupWithEmail != null) {
                labelSignupWithEmail.Dispose ();
                labelSignupWithEmail = null;
            }

            if (LastNameTextField != null) {
                LastNameTextField.Dispose ();
                LastNameTextField = null;
            }

            if (NameTextFeild != null) {
                NameTextFeild.Dispose ();
                NameTextFeild = null;
            }

            if (orLabel != null) {
                orLabel.Dispose ();
                orLabel = null;
            }

            if (PasswordTextField != null) {
                PasswordTextField.Dispose ();
                PasswordTextField = null;
            }

            if (SignUpButton != null) {
                SignUpButton.Dispose ();
                SignUpButton = null;
            }
        }
    }
}