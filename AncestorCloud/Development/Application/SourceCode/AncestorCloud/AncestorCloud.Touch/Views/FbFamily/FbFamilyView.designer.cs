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
	[Register ("FbFamilyView")]
	partial class FbFamilyView
	{
		[Outlet]
		UIKit.UITableView fbFamilyTableView { get; set; }

		[Outlet]
		UIKit.UIButton NextButton { get; set; }

		[Action ("HelpButtonTapped:")]
		partial void HelpButtonTapped (Foundation.NSObject sender);

		[Action ("NextButtonTaped:")]
		partial void NextButtonTaped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (fbFamilyTableView != null) {
				fbFamilyTableView.Dispose ();
				fbFamilyTableView = null;
			}

			if (NextButton != null) {
				NextButton.Dispose ();
				NextButton = null;
			}
		}
	}
}
