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
	[Register ("FbFamilyCell")]
	partial class FbFamilyCell
	{
		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel RelationLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
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
