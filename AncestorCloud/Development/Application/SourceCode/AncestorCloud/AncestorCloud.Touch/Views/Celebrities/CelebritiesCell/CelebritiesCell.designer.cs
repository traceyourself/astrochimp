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
	[Register ("CelebritiesCell")]
	partial class CelebritiesCell
	{
		[Outlet]
		UIKit.UILabel LastName { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }


		[Action ("AddCelbButtonTapped:")]
		partial void AddCelbButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (LastName != null) {
				LastName.Dispose ();
				LastName = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}
		}
	}
}
