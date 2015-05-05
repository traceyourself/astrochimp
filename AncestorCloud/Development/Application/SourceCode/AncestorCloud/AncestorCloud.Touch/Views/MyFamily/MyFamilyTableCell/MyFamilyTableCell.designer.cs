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
	[Register ("MyFamilyTableCell")]
	partial class MyFamilyTableCell
	{
		[Outlet]
		UIKit.UILabel BirthLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel RelationLabel { get; set; }

		[Action ("EditButtonTapped:")]
		partial void EditButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BirthLabel != null) {
				BirthLabel.Dispose ();
				BirthLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (RelationLabel != null) {
				RelationLabel.Dispose ();
				RelationLabel = null;
			}
		}
	}
}
