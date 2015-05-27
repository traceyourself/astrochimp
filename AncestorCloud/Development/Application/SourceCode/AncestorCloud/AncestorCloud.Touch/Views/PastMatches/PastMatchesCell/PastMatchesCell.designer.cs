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
		UIKit.UILabel DegreeLabel { get; set; }

		[Outlet]
		UIKit.UIImageView FirstImage { get; set; }

		[Outlet]
		UIKit.UILabel MyNameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView OtherImageView { get; set; }

		[Outlet]
		UIKit.UILabel OtherNameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView SecondImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FirstImage != null) {
				FirstImage.Dispose ();
				FirstImage = null;
			}

			if (SecondImage != null) {
				SecondImage.Dispose ();
				SecondImage = null;
			}

			if (OtherImageView != null) {
				OtherImageView.Dispose ();
				OtherImageView = null;
			}

			if (DegreeLabel != null) {
				DegreeLabel.Dispose ();
				DegreeLabel = null;
			}

			if (MyNameLabel != null) {
				MyNameLabel.Dispose ();
				MyNameLabel = null;
			}

			if (OtherNameLabel != null) {
				OtherNameLabel.Dispose ();
				OtherNameLabel = null;
			}
		}
	}
}
