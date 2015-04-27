// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AncestorCloud.Touch
{
	[Register ("SignUpView")]
	partial class SignUpView
	{
		[Outlet]
		UIKit.UITextField EmailTextField { get; set; }

		[Outlet]
		UIKit.UIButton Facebookbutton { get; set; }

		[Outlet]
		UIKit.UITextField NameTextFeild { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextField { get; set; }

		[Outlet]
		UIKit.UIButton SignUpButton { get; set; }

		[Action ("FacebookButtonTapped:")]
		partial void FacebookButtonTapped (Foundation.NSObject sender);

		[Action ("SignUpButttonTapped:")]
		partial void SignUpButttonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Facebookbutton != null) {
				Facebookbutton.Dispose ();
				Facebookbutton = null;
			}

			if (SignUpButton != null) {
				SignUpButton.Dispose ();
				SignUpButton = null;
			}

			if (NameTextFeild != null) {
				NameTextFeild.Dispose ();
				NameTextFeild = null;
			}

			if (EmailTextField != null) {
				EmailTextField.Dispose ();
				EmailTextField = null;
			}

			if (PasswordTextField != null) {
				PasswordTextField.Dispose ();
				PasswordTextField = null;
			}
		}
	}
}
