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
	[Register ("CustomCellView")]
	partial class CustomCellView
	{
		[Outlet]
		UIKit.UIImageView BorderImage { get; set; }

		[Outlet]
		UIKit.UIImageView IconImage { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (IconImage != null) {
				IconImage.Dispose ();
				IconImage = null;
			}

			if (BorderImage != null) {
				BorderImage.Dispose ();
				BorderImage = null;
			}
		}
	}
}
