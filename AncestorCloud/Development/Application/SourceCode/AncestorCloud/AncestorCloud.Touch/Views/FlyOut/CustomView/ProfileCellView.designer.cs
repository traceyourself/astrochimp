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
	[Register ("ProfileCellView")]
	partial class ProfileCellView
	{
		[Outlet]
		UIKit.UIImageView ProfilePic { get; set; }

		[Outlet]
		UIKit.UILabel UserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProfilePic != null) {
				ProfilePic.Dispose ();
				ProfilePic = null;
			}

			if (UserName != null) {
				UserName.Dispose ();
				UserName = null;
			}
		}
	}
}
