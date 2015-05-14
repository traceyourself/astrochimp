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
		UIKit.UIButton CelbImage { get; set; }

		[Outlet]
		UIKit.UIImageView CelebImageView { get; set; }

		[Outlet]
		UIKit.UILabel LastName { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Action ("AddCelbButtonTapped:")]
		partial void AddCelbButtonTapped (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CelebImageView != null) {
				CelebImageView.Dispose ();
				CelebImageView = null;
			}

			if (CelbImage != null) {
				CelbImage.Dispose ();
				CelbImage = null;
			}

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
