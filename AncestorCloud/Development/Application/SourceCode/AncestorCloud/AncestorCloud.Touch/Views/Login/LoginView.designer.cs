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
	[Register ("LoginView")]
	partial class LoginView
	{
		[Outlet]
		UIKit.UITextField EmailTextFeild { get; set; }

		[Outlet]
		UIKit.UIButton Facebookbutton { get; set; }

		[Outlet]
		UIKit.UIButton LoginButton { get; set; }

		[Outlet]
		UIKit.UITextField PasswordTextFeild { get; set; }

		[Action ("FacebookButtonTapped:")]
		partial void FacebookButtonTapped (Foundation.NSObject sender);

		[Action ("LoginButtonTapped:")]
		partial void LoginButtonTapped (Foundation.NSObject sender);

		[Action ("PushButtonClicked:")]
		partial void PushButtonClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (EmailTextFeild != null) {
				EmailTextFeild.Dispose ();
				EmailTextFeild = null;
			}

			if (PasswordTextFeild != null) {
				PasswordTextFeild.Dispose ();
				PasswordTextFeild = null;
			}

			if (Facebookbutton != null) {
				Facebookbutton.Dispose ();
				Facebookbutton = null;
			}

			if (LoginButton != null) {
				LoginButton.Dispose ();
				LoginButton = null;
			}
		}
	}
}
