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
	[Register ("PastMatchesCell")]
	partial class PastMatchesCell
	{
		[Outlet]
		UIKit.UILabel MyNameLabel { get; set; }

		[Outlet]
		UIKit.UILabel OtherNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OtherNameLabel != null) {
				OtherNameLabel.Dispose ();
				OtherNameLabel = null;
			}

			if (MyNameLabel != null) {
				MyNameLabel.Dispose ();
				MyNameLabel = null;
			}
		}
	}
}
